using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HslCommunication.Profinet.Siemens;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
//using ICUValidationService.Log;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Xml.Serialization;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using HslCommunication.Robot.FANUC;
using MotorBrakeTestApp.WebApi.BrakeInfos;
using MotorBrakeTestApp.Services;
using MotorBrakeTestApp.WebApi.SuZhouMotor;
using Microsoft.EntityFrameworkCore.Internal;
//using ClassTestBench_brake_Input;
//using MotorBrakeTestApp.Sample;

namespace MotorBrakeTestApp
{
    /* ********************************************************
     * 工单数据流程
     * 1、扫码 txProductID => 输入条码
     * 2、btn_GetApi_Click => 查询工单信息及标准
     * 3、comboBox3_SelectedIndexChanged => 处理额定电压；确定使用的整流块；
     * 4、RunMainTestProcessAsync() => 执行测试
     * ********************************************************/

    public partial class Main : Form
    {
        #region 00 局部变量
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(Main));
        BrakeOrder brakeOrders;//制动器工单
        //public static event UPdataHander updata;
        /// <summary>
        ///  更新SQL SETTING 事件
        /// </summary>
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        BrakeWorkOrderService brakeWorkOrderService;
        /// <summary>
        /// 用于测量电阻、耐压 .
        /// </summary>
        static public bool IsStartTest01ThreadRun = false;   //一号工位，用于测量电阻、耐压;
        private int i = 0;
        private int j = 0;
        private float SumReatedCurrent;
        private float SumReatedPower;
        private int SumReadtedTestTimes;
        private float Sum60pCurrent;
        private float Sum60pPower;
        private int Sum60pTestTimes;
        private DateTime testStartTime;
        private int Adjust;    //用于校准
        private bool ChangeVoltage = false; //切换电压
        static System.Threading.Mutex _mutex;
        LoadForm frmLoad = new LoadForm();
        private Services.TestServices.BrakeTestService BrakeTestService;
        private Socket Ainuo_socketCore = null;
        private Socket TH2516_socketCore = null;
        private Socket WT310E_socketCore = null;
        private byte[] buffer = new byte[2048];
        GlobalData.Product[] Body = new GlobalData.Product[3];
        GlobalData.Model BrakeModel = new GlobalData.Model();
        #endregion

        #region 01 公共属性
        public delegate void PCsendByteHandler(string action);   //创建委托
        /// <summary>
        /// 
        /// </summary>
        public static event PCsendByteHandler ControlTester;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        public delegate void ComparisonTestResultHandler(float Result);
        /// <summary>
        /// 
        /// </summary>
        public static event ComparisonTestResultHandler Comparison;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        public delegate void UPdataHander(int k);
        #endregion

        #region 02 构造函数 + public Main()
        /// <summary>
        /// 构造函数
        /// </summary>
        public Main()
        {
            //BrakeWorkOrderService brakeWorkOrderService = new BrakeWorkOrderService();
            #region 02 01确保只能打开一个
            //是否可以打开新进程
            bool createNew;
            //获取程序集Guid作为唯一标识
            Attribute guid_attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute));
            string guid = ((GuidAttribute)guid_attr).Value;
            _mutex = new System.Threading.Mutex(true, guid, out createNew);
            if (false == createNew)
            {
                MessageBox.Show("程序已经在运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            _mutex.ReleaseMutex();
            #endregion

            InitializeComponent();

            #region 软件初始化
            frmLoad.Show();
            frmLoad.Refresh();

            //Logger.Instance.WriteLog("软件初始化,软件启动");
            CheckForIllegalCrossThreadCalls = false;
            //ISResistace.Checked = true;
            //ISWithStand.Checked = true;
            //ISFPower.Checked = true;
            //ISHPower.Checked = true;
            //ISTorque.Checked = true;
            // Class1GlobalData.ReadPLCIO();
            try
            {
                //if (DateTime.Now.ToString("yyyy-MM-dd") != GlobalData.config.AppSettings.Settings["产量更新日期"].Value)
                //{
                //    for (int i = 0; i < 24; i++)
                //    {
                //        GlobalData.EveryHBadQty[i] = 0;
                //        config.AppSettings.Settings[i.ToString("00")].Value = GlobalData.EveryHBadQty[i].ToString();
                //        config.Save(ConfigurationSaveMode.Modified);
                //        ConfigurationManager.RefreshSection("appSettings");
                //    }
                //    GlobalData.TotalMakeQty = 0;
                //    GlobalData.TotalBadQty = 0;
                //    GlobalData.TotalGoodQty = 0;
                //    config.AppSettings.Settings["当日产量"].Value = GlobalData.TotalMakeQty.ToString();
                //    config.Save(ConfigurationSaveMode.Modified);
                //    ConfigurationManager.RefreshSection("appSettings");
                //    config.AppSettings.Settings["当日良品数量"].Value = GlobalData.TotalGoodQty.ToString();
                //    config.Save(ConfigurationSaveMode.Modified);
                //    ConfigurationManager.RefreshSection("appSettings");
                //    config.AppSettings.Settings["当日不良品数量"].Value = GlobalData.TotalBadQty.ToString();
                //    config.Save(ConfigurationSaveMode.Modified);
                //    ConfigurationManager.RefreshSection("appSettings");
                //    config.AppSettings.Settings["产量更新日期"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                //    config.Save(ConfigurationSaveMode.Modified);
                //    ConfigurationManager.RefreshSection("appSettings");
                //}

                #region //PLC TCPIP 连接
                //Link_SerialZebra();

                #endregion
            }
            catch (Exception e)
            {
                log.Error(e);
                MessageBox.Show(e.Message);
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 关闭加载窗体
        /// </summary>
        private async void CloseLoadFromAsync()
        {
            await Task.Delay(1000);
            if (!frmLoad.IsDisposed) frmLoad.Close();
        }

        /// <summary>
        /// 初始化测试数据
        /// </summary>
        public void InitialBrakeTestData()
        {
            GlobalData.ClassTestBenchbrakeWrite = new WebApi.Write.ClassTestBenchbrakeWrite();
        }

        private bool CheckSetBrakeData()
        {
            if (!float.TryParse(comboBox3.Text, out float brakeVoltage)) return false;
            BrakeModel.Rated_V = brakeVoltage;
            if (GlobalData.SelectedBrakeVoltage == null) { return false; }
            if (BrakeModel.Run_Ver == 0) { return false; }

            return true;
        }

        #region 001 直流电阻测试
        /* ***********************************************
         * 目前直流电阻测试第一项测试值不稳定，通过日志看有量程
         * 不准确的情况；延时时间长短与测试值不准确之间没发现关联
         * 
         * 
         * ***********************************************/

        /// <summary>
        /// 直流电阻测试
        /// </summary>
        /// <returns></returns>
        private async Task RunDcResiTsBsTestItemAsync()
        {
            PLC.DcResiProtection(1);

            #region 0012 AB电阻测试
            try
            {
                GlobalData.TH2516TestTimes = 1;
                toolStripStatusLabel4.Text = "电阻Bs测试中...";
                bt电阻测试结果.Text = "电阻Bs测试中...";
                toolStripStatusLabel4.Text = "系统当前状态:电阻Bs测试中...";
                SetToolStripProgressBar(25);
                bt电阻测试结果.BackColor = Color.Yellow;
                userLantern1.LanternBackground = Color.LimeGreen;
                //直流电阻测试
                var dcResiTs = await BrakeTestService.MeasDcResiBs();
                dcResiTs = MathHelper.CorrectResistance(
                    dcResiTs,
                    GlobalData.ClassTestBenchbrakeWrite.temperature);
                tx测试温度值.Text
                    = GlobalData.ClassTestBenchbrakeWrite.temperature.ToString("0.00");
                txR1测试值.Text = dcResiTs.ToString();
                Body[i].R1 = dcResiTs.ToString("0.000");
                Body[i].R1_Correction = txR1测试值.Text;
                GlobalData.ClassTestBenchbrakeWrite.resistancePassed
                    = Comparison_R1((float)dcResiTs);
                if (BrakeTestService.HasStop()) return;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message + "Bs电阻测试");
            }
            #endregion

            #region 0013 BC电阻测试
            try
            {
                toolStripStatusLabel4.Text = "电阻Ts测试中...";
                bt电阻测试结果.Text = "电阻Ts测试中...";
                bt电阻测试结果.BackColor = Color.Yellow;
                toolStripStatusLabel4.Text = "系统当前状态:电阻Ts测试中...";
                toolStripProgressBar3.Value += 25 * 1;
                userLantern2.LanternBackground = Color.LimeGreen;
                var dcResiBs = await BrakeTestService.MeasDcResiTs();
                dcResiBs = MathHelper.CorrectResistance(
                    dcResiBs,
                    GlobalData.ClassTestBenchbrakeWrite.temperature);
                txR2测试值.Text = dcResiBs.ToString();
                Body[i].R2 = dcResiBs.ToString("0.000");
                Body[i].R2_Correction = txR2测试值.Text;
                GlobalData.ClassTestBenchbrakeWrite.resistancePassed
                    = Comparison_R2((float)dcResiBs);
                toolStripProgressBar3.Value += 25 * 1;

            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message + " Ts电阻测试");
            }

            #endregion

            PLC.DcResiProtection(0);
        }
        #endregion

        #region 002 绝缘电阻测试
        private async Task RunInsuResiTsBsTestItemAsync()
        {
            try
            {
                bt绝缘测试结果.Text = "绝缘电阻测量中...";
                bt绝缘测试结果.BackColor = Color.Yellow;
                toolStripStatusLabel4.Text = "系统当前状态:绝缘电阻测量...";
                toolStripProgressBar3.Value = 0;
                toolStripProgressBar3.Value += 25 * 1;
                var inusResi = await BrakeTestService.MeasInusResi();
                Body[i].Rm = inusResi[0].ToString();
                Body[i].Rm_Voltage = BrakeModel.IR_TestVoltage.ToString();
                tx绝缘值.Text = inusResi[0].ToString();
                Comparison_Rm((float)inusResi[0]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        #endregion

        private async Task RunWithstandVoltageTsBsTestItemAsync()
        {
            try
            {
                toolStripStatusLabel4.Text = "系统当前状态:耐压测试中...";
                bt耐压测试结果.Text = "耐压测试中。。。";
                bt耐压测试结果.BackColor = Color.Yellow;
                userLantern3.LanternBackground = Color.LimeGreen;
                toolStripProgressBar3.Value = 0;
                toolStripProgressBar3.Value += 25 * 1;
                var results = await BrakeTestService.MeasWithstand(
                    BrakeModel.WithStand_V,
                    BrakeModel.WithStandV_Time);
                tx耐压电压.Text = (results[0] * 1000).ToString();
                tx泄露电流.Text = results[1].ToString();
                Body[i].WSVoltage = tx耐压电压.Text;
                Body[i].LeakCurrent = tx泄露电流.Text;
                Comparison_LeakCurrent((float)results[1]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.ToString());
            }
        }

        private void FullVoltageTestDelayAction()
        {
            var wt310text = Services.Meter.WT310E.NUMericVALue();
            double[] wt310results = Services.Meter.WT310E.ConvertToDoubles(wt310text);
            tx额定电压值.Text = wt310results[0].ToString("000.0");
            tx实际电流值.Text = wt310results[1].ToString("0.0000");
            tx实际功率.Text = wt310results[2].ToString("0.0000");
        }

        /// <summary>
        /// 额定电压测试
        /// </summary>
        /// <returns></returns>
        private async Task RunFullVoltageTestItemAsync()
        {
            toolStripStatusLabel4.Text = "额定电压功率测试中...";
            bt额定电参数测试结果.Text = "额定电压功率测试中...";
            bt额定电参数测试结果.BackColor = Color.Yellow;
            toolStripStatusLabel7.Text = "系统当前状态:额定电压功率测试中...";

            userLantern4.LanternBackground = Color.LimeGreen;
            await BrakeTestService.OpenBrakeSource(BrakeModel.Rated_V);

            int delayTime = (int)(BrakeModel.Rated_PowerTime * 1000) - 200;
            await BrakeTestService.Delay(delayTime, FullVoltageTestDelayAction);
            await Task.Delay(200);
            var wt310text = Services.Meter.WT310E.NUMericVALue();
            double[] wt310results = Services.Meter.WT310E.ConvertToDoubles(wt310text);

            await BrakeTestService.CloseBrakeSource();
            tx额定电压值.Text = wt310results[0].ToString("000.0");
            tx实际电流值.Text = wt310results[1].ToString("0.0000");
            tx实际功率.Text = wt310results[2].ToString("0.0000");

            GlobalData.TestState[3] = false;
            userLantern4.LanternBackground = Color.Red;
            if (tx实际电流值.Text == "")
            {
                tx实际电流值.Text = "0";
            }
            if (tx实际功率.Text == "")
            {
                tx实际功率.Text = "0";
            }
            tx修正电流值.Text = ((float.Parse(comboBox3.Text) / float.Parse(tx额定电压值.Text)) * float.Parse(tx实际电流值.Text)).ToString("0.0000");
            tx修正功率.Text = ((float.Parse(comboBox3.Text) / float.Parse(tx额定电压值.Text)) * float.Parse(tx实际功率.Text)).ToString("0.0000");
            SumReatedCurrent = 0;
            SumReatedPower = 0;
            SumReadtedTestTimes = 0;
            float Current = float.Parse(tx修正电流值.Text);
            Body[j].RD_Current = tx实际电流值.Text;
            float Power = float.Parse(tx修正功率.Text);
            Body[j].RD_Power = tx实际功率.Text;
            Body[j].RD_Current_Correction = tx修正电流值.Text;
            Body[j].RD_Power_Correction = tx修正功率.Text;
            Body[j].RD_Voltage = tx额定电压值.Text;
            Comparison_100Power(Current, Power);
            toolStripProgressBar2.Value = 0;
            toolStripProgressBar2.Value += 20 * 1;
        }

        #region 005 跑合相关
        double grindingKeepMax = 0;
        DateTime grindingFinishTime = DateTime.Now;
        /// <summary>
        /// 延时期间执行的方法
        /// </summary>
        private void GrindingBrakeTestDelayAction()
        {
            if (grindingKeepMax < PLC.TorqueValue)
            {
                grindingKeepMax = PLC.TorqueValue;
            }
            tx制动器跑合转矩.Text = PLC.TorqueValue.ToString("0.00");
            tx制动器跑合速度.Text = PLC.SpeedValue.ToString("0.0");
            tx制动器跑合剩余时间.Text = (grindingFinishTime - DateTime.Now).TotalSeconds.ToString();
        }

        /// <summary>
        /// 跑合测试
        /// </summary>
        /// <returns></returns>
        private async Task RunGrindingBrakeTestItemAsync()
        {
            GlobalData.TestState[4] = true;
            bt制动器跑合结果.Text = "跑合测试中...";
            bt制动器跑合结果.BackColor = Color.Yellow;
            toolStripStatusLabel7.Text = "系统当前状态:跑合测试中...";
            int delayTime = (int)(BrakeModel.Run_TimeL * 1000);
            grindingFinishTime = DateTime.Now + TimeSpan.FromMilliseconds(delayTime);
            Task delayTask = BrakeTestService.Delay(delayTime, GrindingBrakeTestDelayAction);
            await Task.Delay(delayTime - 3000);
            grindingKeepMax = 0;
            await delayTask;
            await Task.Delay(100);
            try
            {
                Comparison_MotorRunTorque(float.Parse(tx制动器跑合转矩.Text));
                Body[j].Runspeed = tx制动器跑合速度.Text;
                Body[j].RunTorque = tx制动器跑合转矩.Text;
                toolStripProgressBar2.Value = 0;
                toolStripProgressBar2.Value += 20 * 1;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            GlobalData.TestState[4] = false;
        }
        #endregion

        #region 006 制动转矩测试

        double brakeTorqueKeepMax = 0;
        DateTime brakeFinishTime = DateTime.Now;
        private void BrakeTorqueDelayAction()
        {
            if (brakeTorqueKeepMax < PLC.TorqueValue)
            {
                brakeTorqueKeepMax = PLC.TorqueValue;
            }
            //tx制动器转矩.Text = PLC.TorqueValue.ToString("0.00");
            tx制动器转矩.Text = brakeTorqueKeepMax.ToString("0.00");
            tx制动器转矩转速.Text = PLC.SpeedValue.ToString("0.0");
            tx制动剩余时间.Text = (brakeFinishTime - DateTime.Now).TotalSeconds.ToString();
        }

        /// <summary>
        /// 制动力矩测试
        /// </summary>
        /// <returns></returns>
        private async Task RunBrakeTorqueTestItemAsync()
        {
            GlobalData.TestState[5] = true;
            bt制动器转矩测试结果.Text = "制动转矩测试中...";
            bt制动器转矩测试结果.BackColor = Color.Yellow;
            toolStripStatusLabel7.Text = "系统当前状态:制动转矩测试中...";

            brakeTorqueKeepMax = 0;
            int delayTime = (int)(BrakeModel.BrakeTorqueTime * 1000);
            brakeFinishTime = DateTime.Now + TimeSpan.FromMilliseconds(delayTime);
            await BrakeTestService.Delay(delayTime, BrakeTorqueDelayAction);
            await Task.Delay(100);
            try
            {
                Body[j].Breakspeed = tx制动器转矩转速.Text;
                Body[j].BreakTorque = tx制动器转矩.Text;
                Comparison_MotorBrakeTorque(float.Parse(tx制动器转矩.Text));
                toolStripProgressBar2.Value = 0;
                toolStripProgressBar2.Value += 20 * 1;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            GlobalData.TestState[5] = false;
        }
        #endregion

        #region 007 间隙测试
        private async Task RunReadyDisplacementTestItemAsync()
        {
            BrakeModel.DisplacementBackgroundValue1 = PLC.DisplacementSensor1;
            BrakeModel.DisplacementBackgroundValue2 = PLC.DisplacementSensor2;
            log.Debug($"清零位置 A:{PLC.DisplacementSensor1} B:{PLC.DisplacementSensor2}");
            await Task.Delay(10);
            btnDisplacement.Text = "测试中...";
            btnDisplacement.BackColor = Color.Yellow;
        }

        /// <summary>
        /// 间隙测试
        /// </summary>
        /// <returns></returns>
        private async Task RunDisplacementTestItemAsync()
        {
            await Task.Delay(100);
            BrakeModel.DisplacementValue1 = PLC.DisplacementSensor1 - BrakeModel.DisplacementBackgroundValue1;
            BrakeModel.DisplacementValue2 = PLC.DisplacementSensor2 - BrakeModel.DisplacementBackgroundValue2;
            log.Debug($"制动位置 A:{PLC.DisplacementSensor1} B:{PLC.DisplacementSensor2}");
            //DB130.204
            tx实测左间隙.Text = BrakeModel.DisplacementValue1.ToString("0.000");
            //DB130.216
            tx实测右间隙.Text = BrakeModel.DisplacementValue2.ToString("0.000");
            Body[j].IntervelLeft = tx实测左间隙.Text;
            Body[j].IntervelRight = tx实测右间隙.Text;
            Body[i].Comparison_Intervel = true;
            btnDisplacement.BackColor = Color.LimeGreen;
            btnDisplacement.Text = "合格";

        }
        #endregion

        #region 008 残留转矩
        /* ************* 残留转矩修改记录 *****************
         * 2023-04-23 残余转矩改为均值；
         * residualTorqueKeepMax的功能改为当前检测次数的平均值；
         * 
         * ********************************************/
        double residualTorqueKeepMax = 0;
        DateTime residualTorqueFinishTime = DateTime.Now;
        double residualTorqueSum = 0;   //总数
        int residualTorqueCount = 0;    //采集次数

        private void ResidualTorqueDelayAction()
        {
            residualTorqueSum += PLC.TorqueValue;
            residualTorqueCount++;
            residualTorqueKeepMax = residualTorqueSum / residualTorqueCount;
            tx残留转矩.Text = residualTorqueKeepMax.ToString("0.00");
            tx残留转矩转速.Text = PLC.SpeedValue.ToString("0.0");
            tx残留剩余时间.Text = (residualTorqueFinishTime - DateTime.Now).TotalSeconds.ToString();
        }

        /// <summary>
        /// 残余力矩
        /// </summary>
        /// <returns></returns>
        private async Task RunResidualTorqueTestItemAsync()
        {
            GlobalData.TestState[6] = true;
            bt残留转矩测试结果.Text = "残留转矩测试中...";
            bt残留转矩测试结果.BackColor = Color.Yellow;
            toolStripStatusLabel7.Text = "系统当前状态:残留转矩测试中...";

            int delayTime = (int)(BrakeModel.ResidualTorqueTime * 1000);
            residualTorqueFinishTime = DateTime.Now + TimeSpan.FromMilliseconds(delayTime);
            var delayTask = BrakeTestService.Delay(delayTime, ResidualTorqueDelayAction);
            await Task.Delay(delayTime / 2);
            residualTorqueKeepMax = 0;
            residualTorqueSum = 0;
            residualTorqueCount = 0;
            await delayTask;
            await Task.Delay(100);
            var wt310text = Services.Meter.WT310E.NUMericVALue();
            double[] wt310results = Services.Meter.WT310E.ConvertToDoubles(wt310text);
            tx残留转矩电压.Text = wt310results[0].ToString("000.0");

            try
            {
                Body[j].RemainVoltage = tx残留转矩电压.Text;
                Body[j].Remainspeed = tx残留转矩转速.Text;
                Comparison_MotorRemainTorque(float.Parse(tx残留转矩.Text));
                Body[j].RemainTorque = tx残留转矩.Text;
                toolStripProgressBar2.Value = 0;
                toolStripProgressBar2.Value += 20 * 1;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            GlobalData.TestState[6] = false;
        }
        #endregion

        #region 009 60%电压下功率测试

        private void Voltage06TestDelayAction()
        {
            var wt310text = Services.Meter.WT310E.NUMericVALue();
            double[] wt310results = Services.Meter.WT310E.ConvertToDoubles(wt310text);
            tx60p电压.Text = wt310results[0].ToString("000.0");
            tx60p电压实际电流.Text = wt310results[1].ToString("0.0000");
            tx60p电压实际功率.Text = wt310results[2].ToString("0.0000");
        }

        /// <summary>
        /// 60%电压测试
        /// </summary>
        /// <returns></returns>
        private async Task Run06VoltageTestItemAsync()
        {
            toolStripStatusLabel7.Text = "系统当前状态:60%电压功率测试中...";
            bt60p电压测试结果.Text = "60%电压功率测试中...";
            bt60p电压测试结果.BackColor = Color.Yellow;
            userLantern5.LanternBackground = Color.LimeGreen;

            await BrakeTestService.OpenBrakeSource(BrakeModel.Rated_V * 0.6f);

            int delayTime = (int)(BrakeModel.Rated_60pTimer * 1000) - 200;
            await BrakeTestService.Delay(delayTime, Voltage06TestDelayAction);
            await Task.Delay(200);
            var wt310text = Services.Meter.WT310E.NUMericVALue();
            double[] wt310results = Services.Meter.WT310E.ConvertToDoubles(wt310text);
            tx60p电压.Text = wt310results[0].ToString("000.0");
            tx60p电压实际电流.Text = wt310results[1].ToString("0.0000");
            tx60p电压实际功率.Text = wt310results[2].ToString("0.0000");
            await Task.Delay(100);

            await BrakeTestService.CloseBrakeSource();

            userLantern5.LanternBackground = Color.Red;
            toolStripProgressBar2.Value = 0;
            toolStripProgressBar2.Value += 20 * 1;
            GlobalData.TestState[7] = false;
            if (tx60p电压实际电流.Text == "")
            {
                tx60p电压实际电流.Text = "0";
            }
            if (tx60p电压实际功率.Text == "")
            {
                tx60p电压实际功率.Text = "0";
            }
            tx60p电压修正电流.Text = ((float.Parse(comboBox3.Text) * 0.6 / float.Parse(tx60p电压.Text)) * float.Parse(tx60p电压实际电流.Text)).ToString("0.0000");
            tx60p电压修正功率.Text = ((float.Parse(comboBox3.Text) * 0.6 / float.Parse(tx60p电压.Text)) * float.Parse(tx60p电压实际功率.Text)).ToString("0.0000");
            Sum60pCurrent = 0;
            Sum60pPower = 0;
            Sum60pTestTimes = 0;
            float Current = float.Parse(tx60p电压修正电流.Text);
            float Power = float.Parse(tx60p电压修正功率.Text);
            Body[j].Voltage60p = tx60p电压.Text;
            Body[j].Current60p = tx60p电压实际电流.Text;
            Body[j].Power60p = tx60p电压实际功率.Text;
            Body[j].Current60p_Correction = tx60p电压修正电流.Text;
            Body[j].Power60p_Correction = tx60p电压修正功率.Text;
            TimeSpan ts;
            DateTime testEndTime = Convert.ToDateTime(DateTime.Now.ToString());
            ts = testEndTime - testStartTime;
            Body[j].UseTime = ts.Seconds.ToString();   //使用时间
            Comparison_60Power(Current, Power);
        }
        #endregion

        private void UpdateSquence()
        {
            int brakeIndex = Convert.ToInt32(tx_testSquence.Text);
            brakeWorkOrderService.SetOrderIndex(txProductID.Text, brakeIndex);
            brakeIndex++;
            tx_testSquence.Text = brakeIndex.ToString();
        }

        /// <summary>
        /// 测试完成后数据处理
        /// </summary>
        private void TestFinishProcess()
        {
            SetToolStripTestStateText("测试完成");
            SetToolStripProgressBar(100);

            //测试结束更新SQL
            if (ISResistace.Checked && ISWithStand.Checked && ISFPower.Checked && ISFPower.Checked)
            {
                if (Body[j].Comparison_R == true && Body[j].Comparison_Rm == true && Body[j].Comparison_LeakCurrent == true && Body[j].Comparison_RDCurrent == true && Body[j].Comparison_RDPower == true && Body[j].Comparison_RunTorque == true && Body[j].Comparison_BreakTorque == true && Body[j].Comparison_RemainTorque == true && Body[j].Comparison_Power60p == true && Body[j].Comparison_Current60p == true)
                {
                    LoginService.AutoLogin();//登陆账号
                    //此处保存数据
                    var retMessage = BrakeSave();
                    if (!retMessage.StartsWith("600"))
                    {
                        ShowDialogMessage($"保存异常 {retMessage}");
                    }
                    else
                    {
                        ShowDialogMessage("保存完成");
                        UpdateSquence();
                        AutoPrintLabel();
                    }
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("有不合格产品!!!确定是否保存数据么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            LoginService.AutoLogin();//登陆账号
                            //此处保存数据
                            var retMessage = BrakeSave();
                            if (!retMessage.StartsWith("600"))
                            {
                                ShowDialogMessage($"保存异常 {retMessage}");
                            }
                            else
                            {
                                ShowDialogMessage("保存完成");
                                UpdateSquence();
                                AutoPrintLabel();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

        /// <summary>
        /// 测试主程序
        /// </summary>
        public async void RunMainTestProcessAsync()
        {
            await Task.Delay(1000);
            while (true)
            {
                //复位设备
                await BrakeTestService.Reset();
                await BrakeTestService.UpMainCylinder();
                await BrakeTestService.CloseBrakeSource();
                CloseLoadFromAsync();
                if (BrakeTestService.HasStop())
                {
                    SetToolStripTestStateText("设备停止运行");
                    BrakeTestService.ResetStop();
                }
                //等待启动信号
                await BrakeTestService.WaitStart();

                BrakeTestService.ResetStop();
                //开始新的测试
                //初始化数据
                InitialForm_Station1();
                InitialForm_Station2();
                InitialBrakeTestData();
                SetToolStripProgressBar(0);
                if (!CheckSetBrakeData())
                {
                    MessageBox.Show("制动器数据异常，请检查数据");
                    continue;
                }

                if (BrakeTestService.HasStop()) continue;
                //气缸下行
                Task downMainCylinderTask = BrakeTestService.DownMainCylinder();
                //测试温度
                GlobalData.ClassTestBenchbrakeWrite.temperature =
                    Services.Meter.KBM50MODBUS.GetTemperature();

                if (BrakeTestService.HasStop()) continue;
                //直流电阻
                if (ISResistace.Checked)
                {
                    if (BrakeTestService.HasStop()) continue;
                    await RunDcResiTsBsTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                }

                if (ISWithStand.Checked)
                {
                    if (BrakeTestService.HasStop()) continue;
                    //绝缘电阻
                    await RunInsuResiTsBsTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                    //耐压测试
                    await RunWithstandVoltageTsBsTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                }

                if (ISFPower.Checked)
                {
                    if (BrakeTestService.HasStop()) continue;
                    await RunFullVoltageTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                }

                //气缸下压完成后在进行动态测试
                if (ISTorque.Checked || ISHPower.Checked)
                {
                    await downMainCylinderTask;
                }

                //转矩测试
                if (ISTorque.Checked)
                {
                    //设置测试转速
                    if (BrakeTestService.HasStop()) continue;
                    BrakeTestService.SetBrakeSpeed(BrakeModel.Run_Ver);
                    await Task.Delay(100);
                    //运行主轴
                    PLC.MotorRun(1);
                    if (BrakeTestService.HasStop()) continue;
                    //跑合测试
                    await RunGrindingBrakeTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                    //制动力矩测试
                    await RunBrakeTorqueTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                    //准备间隙检测、间隙参数初始化
                    await RunReadyDisplacementTestItemAsync();
                    if (BrakeTestService.HasStop()) continue;
                    //制动器供电
                    await BrakeTestService.OpenBrakeSource(BrakeModel.Rated_V);
                    if (BrakeTestService.HasStop()) continue;
                    //残余力矩检测
                    await RunResidualTorqueTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                    //间隙测试
                    await RunDisplacementTestItemAsync();
                    await Task.Delay(200);
                    if (BrakeTestService.HasStop()) continue;
                    //停止主轴运行
                    PLC.MotorRun(0);
                    await Task.Delay(500);
                    //关闭制动器电源
                    await BrakeTestService.CloseBrakeSource();
                }

                if (ISHPower.Checked)
                {
                    await Task.Delay(1000);
                    await Run06VoltageTestItemAsync();
                    await Task.Delay(200);
                }

                TestFinishProcess();
            }
        }

        private void SetToolStripTestStateText(string txt)
        {
            toolStripStatusLabel4.Text = $"系统当前状态: {txt}";
            toolStripStatusLabel7.Text = $"系统当前状态: {txt}";
        }

        private void SetToolStripProgressBar(int value)
        {
            toolStripProgressBar2.Value = value;
            toolStripProgressBar3.Value = value;
        }

        private void ShowDialogMessage(string msg)
        {
            Views.FrmDialogboxSave frmDialogboxSave = new Views.FrmDialogboxSave(msg);
            frmDialogboxSave.Show();
        }

        private async void AutoPrintLabel()
        {
            if (chkAutoPrintLabel.Checked)
            {
                for (int i = 0; i < 2; i++)
                {
                    PrintLabel();
                    await Task.Delay(500);
                }
            }
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        private void PrintLabel()
        {
            WebApiGlobalClass.notes = brakeOrders.notes;
            WebApiGlobalClass.brakeIdentifier = comboBox1.Text;
            string torque = comboBox4.Text.Replace("m", " ");
            torque = torque.Replace("N", "");
            WebApiGlobalClass.brakeTorque = Convert.ToDouble(torque);
            WebApiGlobalClass.brakeQuality = 1;
            WebApiGlobalClass.brakeVoltage = Convert.ToInt16(comboBox3.Text);

            //WebApiGlobalClass.notes = "BZ05D/ 240 45";
            //WebApiGlobalClass.brakeProductId = 0;
            //WebApiGlobalClass.brakeId = 0;
            //WebApiGlobalClass.brakeIdentifier = "BZ05";
            //WebApiGlobalClass.brakeWorkOrderId = 0;
            //WebApiGlobalClass.brakeTorque = 23;
            //WebApiGlobalClass.brakeQuality = 1;
            //WebApiGlobalClass.brakeVoltage = 220;
            Send_ZebraSerial(tx_testSquence.Text.Trim());
        }

        private void SwitchBrakeBE()
        {
            if (!double.TryParse(comboBox3.Text, out double brakeVoltage)) return;
            if (!double.TryParse(cmbBrakeType.Text, out double brakeSize)) return;
            if (double.TryParse(cmbBrakeType.Text.Substring(0, 1), out double sizeStart))
            {
                if (sizeStart == 0)
                {
                    brakeSize /= 10;
                }
            }
            if (!cmbSerice.Text.StartsWith("BE")) return;

            var brakeAp = GlobalData.BrakeVoltageDb.FirstOrDefault(
                em => em.BrakeModel == cmbSerice.Text
                && em.VoltageUp > brakeVoltage && em.VoltageLow <= brakeVoltage
                && em.BrakeSizeUp >= brakeSize && em.BrakeSizeLow <= brakeSize);
            GlobalData.SelectedBrakeVoltage = brakeAp;
        }

        /// <summary>
        /// 窗体加载 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {
            Location = Screen.PrimaryScreen.WorkingArea.Location;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            groupBox1.Visible = true;
            comboBox1.Enabled = true;
            InitialForm_Station1();
            InitialForm_Station2();

            GlobalData.Digital_Power_Connect_State = true;
        }

        /// <summary>
        /// 主窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定退出系统么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    // System.Environment.Exit(0);
                    if (GlobalData.PLC_Connect_State == true)
                    {
                        //Equipment_Device.WritePLCBool(Class1GlobalData.PLCIO[17], false);
                    }
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquipMentInitalTool_Click(object sender, EventArgs e)
        {
            InitialForm_Station1();
            InitialForm_Station2();
        }

        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_StartButton_Click(object sender, EventArgs e)
        {
            //WebApiGlobalClass.notes = brakeOrders.notes;
            //WebApiGlobalClass.brakeIdentifier = comboBox1.Text;
            //string torque = comboBox4.Text.Replace("m", " ");
            //torque = torque.Replace("N", "");
            //WebApiGlobalClass.brakeTorque = Convert.ToDouble(torque);
            //WebApiGlobalClass.brakeQuality = 1;
            //WebApiGlobalClass.brakeVoltage = Convert.ToInt16(comboBox3.Text);

            ////WebApiGlobalClass.notes = "BZ05D/ 240 45";
            ////WebApiGlobalClass.brakeProductId = 0;
            ////WebApiGlobalClass.brakeId = 0;
            ////WebApiGlobalClass.brakeIdentifier = "BZ05";
            ////WebApiGlobalClass.brakeWorkOrderId = 0;
            ////WebApiGlobalClass.brakeTorque = 23;
            ////WebApiGlobalClass.brakeQuality = 1;
            ////WebApiGlobalClass.brakeVoltage = 220;
            //Send_ZebraSerial(tx_testSquence.Text.Trim());
            PrintLabel();
        }

        #region 窗体工具栏 TOOLTITLE   COMBOX

        /// <summary>
        /// 主页面的等陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logo_Click(object sender, EventArgs e)
        {
            using (NewLogin LogForm = new NewLogin())
            {

                LogForm.ShowDialog();
            }
        }

        /// <summary>
        /// 型号选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)      //型号选择
        {

        }
        /// <summary>
        /// 标准编号选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)      //测试标准
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)      //额定电压
        {
            GlobalData.SelectedBrakeVoltage = null;
            label42.Text = string.Empty;
            if (string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(cmbSerice.Text)) return;

            if (!double.TryParse(comboBox3.Text, out double brakeVoltage)) return;

            if (cmbSerice.Text == "BZ" || cmbSerice.Text == "BZ.D")
            {
                var brakeAp = GlobalData.BrakeVoltageDb.FirstOrDefault(em => em.BrakeModel == cmbSerice.Text
                && em.VoltageUp > brakeVoltage && em.VoltageLow <= brakeVoltage);
                GlobalData.SelectedBrakeVoltage = brakeAp;
            }

            SwitchBrakeBE();

            if (GlobalData.SelectedBrakeVoltage == null) return;
            label42.Text = GlobalData.SelectedBrakeVoltage.Adapter;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)      //额定扭矩
        {

        }


        private void 本底校准ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打印当前标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region 发送打印机数据
                Send_ZebraSerial(tx_testSquence.Text.Trim());
                #endregion
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        /// <summary>
        /// 通讯参数设置ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 通讯参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalData.Log_Level > 0)
            {
                using (设备通讯参数 SetData = new 设备通讯参数())
                {
                    SetData.ShowDialog();
                }
            }
            else
            {
                using (登录 LogForm = new 登录())
                {
                    LogForm.myRefresh += new 登录.Refresh(ReRefreshControl);
                    LogForm.ShowDialog();
                }
            }
            Show();
        }

        /// <summary>
        ///  产品测试参数设置ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 产品测试参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalData.Log_Level > 0)
            {
                using (合格参数设定 ProductData = new 合格参数设定())
                {
                    ProductData.myRefresh += new 合格参数设定.Refresh(Background);
                    ProductData.ShowDialog();
                }
            }
            else
            {
                using (登录 LogForm = new 登录())
                {
                    LogForm.myRefresh += new 登录.Refresh(ReRefreshControl);
                    LogForm.ShowDialog();
                }
            }
            Show();
        }

        /// <summary>
        ///  数据库显示界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Hide();
            using (数据库 sqlfrom = new 数据库())
            {
                sqlfrom.ShowDialog();
            }
            Show();
        }

        /// <summary>
        /// 登录人员权限等级判断
        /// </summary>
        private void ReRefreshControl()
        {
            if (GlobalData.Log_Level <= 0)
            {
                //Logo.Text = "登录";
                //调试ToolStripMenuItem.Visible = false;
                //设置ToolStripMenuItem.Enabled = false;
            }
            if (GlobalData.Log_Level >= 1)
            {
                //设置
                //ToolStripMenuItem.Enabled = true;
            }
            if (GlobalData.Log_State == false)
            {
                //Logo.Text = "登录";
                //调试ToolStripMenuItem.Visible = false;
                //退出ToolStripMenuItem.Enabled = false;
                toolStripStatusLabel2.Text = "当前登录账户:无";
                toolStripStatusLabel3.Text = "当前权限等级:无";
                toolStripStatusLabel6.Text = "人员信息:无";
            }
            else
            {
                //Logo.Text = "注销";
                toolStripStatusLabel2.Text = "当前登录账户:" + GlobalData.Log_UserID;
                //退出ToolStripMenuItem.Enabled = true;
                toolStripStatusLabel6.Text = "人员信息:" + GlobalData.Log_Name;
            }
            if (GlobalData.Log_Level >= 5)
            {
                //调试ToolStripMenuItem.Visible = true;
            }
            else
            {
                //调试ToolStripMenuItem.Visible = false;
            }
            if (GlobalData.Log_Level == 0)
            {
                toolStripStatusLabel3.Text = "当前权限等级:" + "普通用户";
            }
            else if (GlobalData.Log_Level == 2)
            {
                toolStripStatusLabel3.Text = "当前权限等级:" + "管理员";
            }
            else if (GlobalData.Log_Level >= 5)
            {
                toolStripStatusLabel3.Text = "当前权限等级:" + "开发人员";
            }
        }

        /// <summary>
        /// 添加用户ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalData.Log_Level >= 2)
            {
                using (添加用户 AddUser = new 添加用户())
                {
                    AddUser.ShowDialog();
                }
            }
            else if (GlobalData.Log_Level == 1)
            {
                MessageBox.Show("权限不够，请联系管理人员!!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (登录 LogForm = new 登录())
                {
                    LogForm.myRefresh += new 登录.Refresh(ReRefreshControl);
                    LogForm.ShowDialog();
                }
            }
            Show();
        }

        /// <summary>
        ///  删除用户ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalData.Log_Level >= 2)
            {
                using (删除用户 delUser = new 删除用户())
                {
                    delUser.ShowDialog();
                }
            }
            else if (GlobalData.Log_Level == 1)
            {
                MessageBox.Show("权限不够，请联系管理人员!!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (登录 LogForm = new 登录())
                {
                    LogForm.myRefresh += new 登录.Refresh(ReRefreshControl);
                    LogForm.ShowDialog();
                }
            }
            Show();
        }
        #endregion

        #region TCP/IP通讯  直流电阻、功率分析仪、安规测试仪、扫码枪、Zebra、 Link ReceiveCallBack

        /// <summary>
        ///  连接扫打印机声明
        /// </summary>
        private SerialPort serialZebra = new SerialPort();

        /// <summary>
        /// 连接扫打印机(通讯）
        /// </summary>
        private void Link_SerialZebra()
        {
            try
            {
                serialZebra.PortName = "COM5";//GlobalData.Poewr_Tester;
                serialZebra.BaudRate = 9600;
                serialZebra.DataBits = 8;
                serialZebra.StopBits = StopBits.One;
                serialZebra.Parity = Parity.None;
                serialZebra.DataReceived += ZebraSerial_ReadData_DataReceived;
                serialZebra.Open();
                log.Debug("连接打印机成功！");
            }
            catch (Exception e)
            {
                log.Debug("连接打印机失败:" + e.Message);
                log.Error(e);
            }
        }

        public ClassTestBench_brake_Search_Output.ClassTestBenchbrakeRead classTestBenchbrakeRead { get; set; }

        /// <summary>
        /// 确认返修工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetApi_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel4.Text = "正在查询请稍后";
            if (string.IsNullOrEmpty(txProductID.Text))
            {
                toolStripStatusLabel4.Text = "您输入的工单有误!!!";
                MessageBox.Show("您输入的工单有误，请重新输入!");
                return;
            }
            Services.LoginService.AutoLogin();//登陆账号
            brakeWorkOrderService = new BrakeWorkOrderService();
            brakeOrders = brakeWorkOrderService.QuertBrakeOrder(txProductID.Text.Trim());
            if (brakeOrders == null)
            {
                toolStripStatusLabel4.Text = $"没有此工单数据！！！{txProductID.Text}";
                return;
            }
            InitCheckBoxValue();
            //加载标准内容
            RefreshApiReadInformationAndControl(brakeWorkOrderService.BrakeOrderNature);
            LoadStandrdInfos(brakeWorkOrderService.BrakeOrderNature);
            SetWebGlobalData();
            toolStripStatusLabel4.Text = $"工单 {txProductID.Text} 查询完成！";
        }

        private void SetWebGlobalData()
        {
            WebApiGlobalClass.brakeWorkOrderId = Convert.ToInt32(brakeWorkOrderService.BrakeOrderNature.orderNo);
            WebApiGlobalClass.notes = brakeWorkOrderService.BrakeOrderNature.notes;
            if (brakeWorkOrderService.BrakeOrderNature.brake != null)
                WebApiGlobalClass.brakeProductId = Convert.ToInt32(brakeWorkOrderService.BrakeOrderNature.brake.partNo);
        }

        private void InitCheckBoxValue()
        {
            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            comboBox1.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox1.Text = string.Empty;

            cmbSerice.SelectedIndex = 2;
        }

        private void LoadStandrdInfos(BrakeOrder brakeOrder)
        {
            int brakeIndex = brakeWorkOrderService.GetOrderLastIndex(txProductID.Text);
            brakeIndex++;
            if (brakeIndex < 1) brakeIndex = 1;
            tx_testSquence.Text = brakeIndex.ToString();
            string notes = brakeOrder.notes;
            if (string.IsNullOrEmpty(brakeOrder.brake.identifier))
            {
                //型号
                int index = notes.LastIndexOf(" ");
                int index_s = notes.IndexOf("/");
                if (index_s > index)
                {
                    string mode = notes.Substring(index + 1, index_s - index - 1);
                    comboBox1.Text = mode;
                }
                else
                {
                    comboBox1.Text = "";
                }
            }
            else
            {
                comboBox1.Text = brakeOrder.brake.identifier;//制动器型号
                comboBox1.Items.Add(brakeOrder.brake.identifier);
            }

            if (brakeOrder.brake.ratedVoltage != null)
            {
                comboBox3.Text = brakeOrder.brake.ratedVoltage.ToString();//额定电压
                comboBox3.Items.Add(brakeOrder.brake.ratedVoltage);
            }

            if (brakeOrder.brake.ratedTorque != null)
            {
                comboBox4.Text = brakeOrder.brake.ratedTorque.ToString();//额定转矩
                comboBox4.Items.Add(brakeOrder.brake.ratedTorque);
            }

            if (brakeOrder.notes != null)
            {
                string[] str3 = notes.Split('/');
                if (str3.Length > 2)
                {
                    comboBox3.Text = Convert.ToInt32(str3[2].Substring(0, str3[2].IndexOf("A"))).ToString();//电压
                    comboBox4.Text = Convert.ToDouble(str3[1].Replace(",", ".").Substring(0, str3[1].IndexOf("N"))).ToString();//转矩
                }
                else
                {
                    comboBox3.Text = "0";
                    comboBox4.Text = "0";
                }
            }

            if (comboBox3.Items != null && comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }

            if (comboBox4.Items != null && comboBox4.Items.Count > 0)
            {
                comboBox4.SelectedIndex = 0;
            }

            //当型号电压为空时，从 notes 分析BZ、BZD 制动器的型号和电压
            if (string.IsNullOrEmpty(brakeOrder.brake.identifier)
                && !string.IsNullOrEmpty(brakeOrder.brake.notes))
            {
                string[] noteTexts = brakeOrder.brake.notes.Split(' ');
                if (noteTexts.Length > 2) comboBox1.Text = noteTexts[1];
                if (noteTexts.Length > 3) comboBox4.Text = noteTexts[2];
                if (noteTexts.Length > 4) comboBox3.Text = noteTexts[3].TrimEnd('V');
            }

            //从型号信息中分析是属于BZ 还是 BZD
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                string brakeName = comboBox1.Text;
                string brakeType = brakeName;
                if (brakeName.StartsWith("BZ"))
                {
                    if (brakeName.Contains("D"))
                    {
                        cmbSerice.SelectedIndex = 1;
                    }
                    else
                    {
                        cmbSerice.SelectedIndex = 0;
                    }
                    brakeType = brakeName.Substring(2);
                    brakeType = brakeType.TrimEnd('D');
                    log.Debug($"制动器 {brakeType}");
                    string[] types = new string[cmbBrakeType.Items.Count];
                    for (int i = 0; i < cmbBrakeType.Items.Count; i++)
                    {
                        types[i] = cmbBrakeType.Items[i].ToString();
                    }
                    cmbBrakeType.SelectedIndex = types.IndexOf(brakeType);
                }

                //从型号信息中分析是属于BE
                if (brakeName.StartsWith("BE"))
                {
                    brakeType = brakeName.Substring(2);
                    if (double.TryParse(brakeType, out double size))
                    {
                        if (!cmbBrakeType.Items.Contains(brakeType))
                        {
                            cmbBrakeType.Items.Add(brakeType);
                        }
                        cmbBrakeType.Text = brakeType;
                    }
                    else
                    {
                        brakeType = brakeType.Substring(0, brakeType.Length - 1);
                        if (double.TryParse(brakeType, out size))
                        {
                            if (!cmbBrakeType.Items.Contains(brakeType))
                            {
                                cmbBrakeType.Items.Add(brakeType);
                            }
                            cmbBrakeType.Text = brakeType;
                        }
                    }

                    if (!cmbSerice.Items.Contains("BE"))
                    {
                        cmbSerice.Items.Add("BE");
                    }
                    cmbSerice.Text = "BE";
                }

            }
            lblRatedSpeed.Text = BrakeModel.Run_Ver.ToString();
        }

        private void RefreshApiReadInformationAndControl(BrakeOrder brakeOrder)
        {
            //详情表
            if (brakeOrder.brake.brakeRoutineStandardDetail == null)
            {
                brakeOrder.brake.brakeRoutineStandardDetail = new ClassTestBench_brake_Search_Output.BrakeRoutineStandardDetail();
            }
            else
            {
                BrakeModel.R1_L = (float)brakeOrder.brake.brakeRoutineStandardDetail.resistanceBsMin;
                BrakeModel.R1_H = (float)brakeOrder.brake.brakeRoutineStandardDetail.resistanceBsMax;
                BrakeModel.R2_L = (float)brakeOrder.brake.brakeRoutineStandardDetail.resistanceTsMin;
                BrakeModel.R2_H = (float)brakeOrder.brake.brakeRoutineStandardDetail.resistanceTsMax;

                textBoxR1MinValue.Text = brakeOrder.brake.brakeRoutineStandardDetail.resistanceBsMin.ToString().Trim();//电阻测量【R1 resistanceBsMin】
                textBoxR1MaxValue.Text = brakeOrder.brake.brakeRoutineStandardDetail.resistanceBsMax.ToString().Trim();//电阻测量【R1 resistanceBsMax】 
                textBoxR2MinValue.Text = brakeOrder.brake.brakeRoutineStandardDetail.resistanceTsMin.ToString().Trim();//电阻测量【R2 resistanceTsMin】
                textBoxR2MaxValue.Text = brakeOrder.brake.brakeRoutineStandardDetail.resistanceTsMax.ToString().Trim();//电阻测量【R2 resistanceTsMax】      
                BrakeModel.IR_L = (float)brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard.insulationResistanceMin;                //绝缘电阻min
                BrakeModel.Leak_AH = (float)brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard.leakageCurrentMax;                //泄露电流max

                textBox绝缘值Min.Text = brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard.insulationResistanceMin.ToString().Trim();//绝缘电阻最小值
                textBox泄露电流Max.Text = brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard.leakageCurrentMax.ToString().Trim();//泄露电流上限

                BrakeModel.Rated_IL = (float)brakeOrder.brake.brakeRoutineStandardDetail.fullVoltageCurrentMin;
                BrakeModel.Rated_IH = (float)brakeOrder.brake.brakeRoutineStandardDetail.fullVoltageCurrentMax;
                BrakeModel.Rated_PL = (float)brakeOrder.brake.brakeRoutineStandardDetail.fullVoltagePowerMin;
                BrakeModel.Rated_PH = (float)brakeOrder.brake.brakeRoutineStandardDetail.fullVoltagePowerMax;

                textBox额定电流Min.Text = brakeOrder.brake.brakeRoutineStandardDetail.fullVoltageCurrentMin.ToString().Trim();
                textBox额定电流Max.Text = brakeOrder.brake.brakeRoutineStandardDetail.fullVoltageCurrentMax.ToString().Trim();
                textBox额定功率Min.Text = brakeOrder.brake.brakeRoutineStandardDetail.fullVoltagePowerMin.ToString().Trim();
                textBox额定功率Max.Text = brakeOrder.brake.brakeRoutineStandardDetail.fullVoltagePowerMax.ToString().Trim();

                BrakeModel.Rated_60pIL = (float)brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltageCurrentMin;
                BrakeModel.Rated_60pIH = (float)brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltageCurrentMax;
                BrakeModel.Rated_60pPL = (float)brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltagePowerMin;
                BrakeModel.Rated_60pPH = (float)brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltagePowerMax;

                textBox60电流Min.Text = brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltageCurrentMin.ToString().Trim();
                textBox60电流Max.Text = brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltageCurrentMax.ToString().Trim();
                textBox60功率Min.Text = brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltagePowerMin.ToString().Trim();
                textBox60功率Max.Text = brakeOrder.brake.brakeRoutineStandardDetail.decreasedVoltagePowerMax.ToString().Trim();

                BrakeModel.Remain_torH = (float)brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard.residualTorqueMax;                //残留转矩max
                textBox残留转矩Max.Text = brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard.residualTorqueMax.ToString().Trim();
            }

            //标准表
            if (brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard != null)
            {
                var standard = brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard;

                //耐压测试
                BrakeModel.WithStand_V = standard.withStandVoltage;
                BrakeModel.WithStandV_Time = standard.withStandDuration;

                //额定电压测试相关
                BrakeModel.Rated_PowerTime = standard.fullVoltageTime;

                //跑合相关
                BrakeModel.Run_TimeL = standard.grindingTimeMin;
                BrakeModel.Run_TimeH = standard.grindingTimeMax;
                tx制动器跑合时间.Text = standard.grindingTimeMin.ToString();
                if (standard.grindingSpeed == 0)
                    standard.grindingSpeed = (int)BrakeModel.Run_Ver;
                BrakeModel.Run_Ver = standard.grindingSpeed;
                tx制动器跑合速度.Text = standard.grindingSpeed.ToString();

                //制动转矩、残余转矩
                BrakeModel.Brake_Ver = standard.grindingSpeed;
                tx制动器转矩转速.Text = standard.grindingSpeed.ToString();
                tx残留转矩转速.Text = standard.grindingSpeed.ToString();
                BrakeModel.BrakeTorqueTime = 5;
                BrakeModel.ResidualTorqueTime = 4;

                tx残留运行时间.Text = BrakeModel.ResidualTorqueTime.ToString();
                tx制动运行时间.Text = BrakeModel.BrakeTorqueTime.ToString();

                //60%电压测试相关
                BrakeModel.Rated_60pTimer = standard.decreasedVoltageTime;
            }

            //力矩表
            if (brakeOrder.brake.brakeRoutineStandardTorque == null)
            {
                brakeOrder.brake.brakeRoutineStandardTorque = new ClassTestBench_brake_Search_Output.BrakeRoutineStandardTorque();
            }
            else
            {
                BrakeModel.Run_torL = (float)brakeOrder.brake.brakeRoutineStandardTorque.grindingTorqueMin;       //磨合转矩 Min
                BrakeModel.Run_torH = (float)brakeOrder.brake.brakeRoutineStandardTorque.grindingTorqueMax;          //磨合转矩 Max
                BrakeModel.Brake_torL = (float)brakeOrder.brake.brakeRoutineStandardTorque.brakeTorqueMin;          //制动转矩 Min
                BrakeModel.Brake_torH = (float)brakeOrder.brake.brakeRoutineStandardTorque.brakeTorqueMax;             //制动转矩 Max

                textBox跑合转矩Min.Text = brakeOrder.brake.brakeRoutineStandardTorque.grindingTorqueMin.ToString().Trim();
                textBox跑合转矩Max.Text = brakeOrder.brake.brakeRoutineStandardTorque.grindingTorqueMax.ToString().Trim();
                textBox制动转矩Min.Text = brakeOrder.brake.brakeRoutineStandardTorque.brakeTorqueMin.ToString().Trim();
                textBox制动转矩Max.Text = brakeOrder.brake.brakeRoutineStandardTorque.brakeTorqueMax.ToString().Trim();

            }
        }

        /// <summary>
        /// 打印机CALLBACK返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZebraSerial_ReadData_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 接收数据
            byte[] buffer = null;
            byte[] data = new byte[2048];
            int receiveCount = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(20);
                if (serialZebra.BytesToRead < 1)
                {
                    buffer = new byte[receiveCount];
                    Array.Copy(data, 0, buffer, 0, receiveCount);
                    break;
                }
                receiveCount += serialZebra.Read(data, receiveCount, serialZebra.BytesToRead);
            }
            if (receiveCount == 0) return;
            Invoke(new Action(() =>
            {
                string msg = string.Empty;
                msg = Encoding.ASCII.GetString(buffer);
                log.Debug(msg);
            }));
        }
        #endregion

        #region 发送字符到仪器Function
        /// <summary>
        /// 安规测试仪测试开始参数
        /// </summary>
        /// <param name="Function"></param>
        private void Send_Ainuo(string Function)           //安规测试仪
        {
            try
            {
                byte[] PC_Send;
                switch (Function)
                {
                    case "进入测试界面":
                        PC_Send = new byte[8] { 0x7B, 0x00, 0x08, 0x01, 0x0F, 0x06, 0x1E, 0x7D };
                        Ainuo_socketCore?.Send(PC_Send, 0, PC_Send.Length, SocketFlags.None);
                        break;
                    case "启动测试":
                        PC_Send = new byte[8] { 0x7B, 0x00, 0x08, 0x01, 0x0F, 0xFF, 0x17, 0x7D };
                        Ainuo_socketCore?.Send(PC_Send, 0, PC_Send.Length, SocketFlags.None);
                        break;
                    case "绝缘电阻测量结果":
                        PC_Send = new byte[9] { 0x7B, 0x00, 0x09, 0x01, 0xF1, 0x01, 0x00, 0xFC, 0x7D };
                        Ainuo_socketCore?.Send(PC_Send, PC_Send.Length, SocketFlags.None);
                        break;
                    case "交流耐压测量结果":
                        PC_Send = new byte[9] { 0x7B, 0x00, 0x09, 0x01, 0xF1, 0x01, 0x01, 0xFD, 0x7D };
                        Ainuo_socketCore?.Send(PC_Send, PC_Send.Length, SocketFlags.None);
                        break;
                    case "直流耐压测量结果":
                        PC_Send = new byte[9] { 0x7B, 0x00, 0x09, 0x01, 0xF1, 0x01, 0x02, 0xFE, 0x7D };
                        Ainuo_socketCore?.Send(PC_Send, PC_Send.Length, SocketFlags.None);
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        /// <summary>
        /// 发送打印机字符串
        /// </summary>
        private void Send_ZebraSerial(string productSquence)
        {
            try
            {
                string str;
                string[] notes = WebApiGlobalClass.notes.Split('/', ' ');
                string getNoteDistinguishEX = WebApiGlobalClass.notes;
                if (notes.Length > 0)
                {
                    getNoteDistinguishEX = notes[0];
                }

                if (getNoteDistinguishEX.Substring(getNoteDistinguishEX.Length - 1, 1) != "E")
                {
                    str = Zebra.SendZebraFunction(productSquence);
                }
                else
                {
                    str = Zebra.SendZebraFunctionEX(productSquence);
                }
                byte[] PC_SendString = null;
                PC_SendString = Encoding.ASCII.GetBytes(str);
                serialZebra?.Write(PC_SendString, 0, PC_SendString.Length);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 对比测试结果Function
        /// <summary>
        /// 对比AB电阻测试结果
        /// </summary>
        /// <param name="R"></param>
        private bool Comparison_R1(float R)
        {
            if (R > BrakeModel.R1_H || R < BrakeModel.R1_L)
            {
                txR1测试值.BackColor = Color.Red;
                bt电阻测试结果.BackColor = Color.Red;
                bt电阻测试结果.Text = "AB电阻不合格";
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[37], true);
                GlobalData.TestComparison[0] = false;
                toolStripProgressBar2.BackColor = Color.Red;
            }
            else if (R <= BrakeModel.R1_H && R >= BrakeModel.R1_L)
            {
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[36], true);
                GlobalData.TestComparison[0] = true;
                bt电阻测试结果.BackColor = Color.Green;
                bt电阻测试结果.Text = "AB电阻测试合格";
            }

            //Equipment_Device.WritePLCBool(GlobalData.PLCIO[35], true);
            return GlobalData.TestComparison[0];
        }

        /// <summary>
        /// 对比BC电阻测试结果
        /// </summary>
        /// <param name="R"></param>
        private bool Comparison_R2(float R)
        {
            if (R > BrakeModel.R2_H || R < BrakeModel.R2_L)
            {
                txR2测试值.BackColor = Color.Red;
                bt电阻测试结果.BackColor = Color.Red;
                bt电阻测试结果.Text = "BC电阻不合格";
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[70], true);
                //GlobalData.TestComparison[1] = false;
                Body[i].Comparison_R = false;
                toolStripProgressBar3.BackColor = Color.Red;
            }
            else
            {
                //GlobalData.TestComparison[1] = true;
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[69], true);
                if (GlobalData.TestComparison[0] == true)
                {
                    bt电阻测试结果.BackColor = Color.LimeGreen;
                    bt电阻测试结果.Text = "BC电阻测试合格";
                    Body[i].Comparison_R = true;
                }
                else
                {
                    bt电阻测试结果.BackColor = Color.Red;
                    bt电阻测试结果.Text = "电阻测试不合格";
                    Body[i].Comparison_R = false;
                }
            }
            //Equipment_Device.WritePLCBool(GlobalData.PLCIO[68], true);
            return Body[i].Comparison_R;
        }

        /// <summary>
        /// 对比安规绝缘测试结果
        /// </summary>
        /// <param name="Rm"></param>
        private void Comparison_Rm(float Rm)
        {
            if (Rm < BrakeModel.IR_L)
            {
                bt绝缘测试结果.BackColor = Color.Red;
                bt绝缘测试结果.Text = "不合格";
                Body[i].Comparison_Rm = false;
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[141], true);
            }
            else
            {
                bt绝缘测试结果.BackColor = Color.LimeGreen;
                bt绝缘测试结果.Text = "合格";
                Body[i].Comparison_Rm = true;
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[140], true);
            }
            //Equipment_Device.WritePLCBool(GlobalData.PLCIO[39], true);
        }

        /// <summary>
        ///  对比安规耐压测试结果,泄露电流
        /// </summary>
        /// <param name="LeakCurrent"></param>
        private void Comparison_LeakCurrent(float LeakCurrent)
        {
            if (LeakCurrent <= BrakeModel.Leak_AH)
            {
                bt耐压测试结果.BackColor = Color.LimeGreen;
                bt耐压测试结果.Text = "合格";
                GlobalData.TestComparison[7] = true;
                Body[i].Comparison_LeakCurrent = true;
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[40], true);
            }
            else
            {
                bt耐压测试结果.BackColor = Color.Red;
                bt耐压测试结果.Text = "不合格";
                GlobalData.TestComparison[7] = false;
                Body[i].Comparison_LeakCurrent = false;
                //Equipment_Device.WritePLCBool(GlobalData.PLCIO[41], true);
            }
            //Equipment_Device.WritePLCBool(GlobalData.PLCIO[39], true);
            GlobalData.GetAinuoSendTimes = 0;
        }

        /// <summary>
        /// 对比额定电压功率测试结果
        /// </summary>
        /// <param name="Current"></param>
        /// <param name="Power"></param>
        private void Comparison_100Power(float Current, float Power)
        {
            if (Current <= BrakeModel.Rated_IH && Current >= BrakeModel.Rated_IL)
            {
                Body[j].Comparison_RDCurrent = true;
            }
            else
            {
                tx修正电流值.BackColor = Color.Red;
                Body[j].Comparison_RDCurrent = false;
            }

            if (Power <= BrakeModel.Rated_PH && Power >= BrakeModel.Rated_PL)
            {
                Body[j].Comparison_RDPower = true;
            }
            else
            {
                tx修正功率.BackColor = Color.Red;
                Body[j].Comparison_RDPower = false;
            }

            if (Body[j].Comparison_RDPower && Body[j].Comparison_RDCurrent)
            {
                bt额定电参数测试结果.Text = "合格";
                bt额定电参数测试结果.BackColor = Color.LimeGreen;
                GlobalData.TestComparison[2] = true;
            }
            else
            {
                bt额定电参数测试结果.Text = "不合格";
                bt额定电参数测试结果.BackColor = Color.Red;
                GlobalData.TestComparison[2] = false;
            }
        }

        /// <summary>
        /// 对比跑合转矩结果
        /// </summary>
        /// <param name="Torque"></param>
        private void Comparison_MotorRunTorque(float Torque)
        {
            if (Torque >= BrakeModel.Run_torL && Torque <= BrakeModel.Run_torH)
            {
                bt制动器跑合结果.BackColor = Color.LimeGreen;
                bt制动器跑合结果.Text = "合格";
                GlobalData.TestComparison[3] = true;
                Body[j].Comparison_RunTorque = true;
            }
            else
            {
                tx制动器跑合转矩.BackColor = Color.Red;
                bt制动器跑合结果.BackColor = Color.Red;
                bt制动器跑合结果.Text = "不合格";
                Body[j].Comparison_RunTorque = false;
                GlobalData.TestComparison[3] = false;
            }
        }

        /// <summary>
        ///  对比刹车转矩结果
        /// </summary>
        /// <param name="Torque"></param>
        private void Comparison_MotorBrakeTorque(float Torque)
        {
            if (Torque >= BrakeModel.Brake_torL && Torque <= BrakeModel.Brake_torH)
            {
                bt制动器转矩测试结果.BackColor = Color.LimeGreen;
                bt制动器转矩测试结果.Text = "合格";
                GlobalData.TestComparison[4] = true;
                Body[j].Comparison_BreakTorque = true;
            }
            else
            {
                tx制动器转矩.BackColor = Color.Red;
                bt制动器转矩测试结果.BackColor = Color.Red;
                bt制动器转矩测试结果.Text = "不合格";
                GlobalData.TestComparison[4] = false;
                Body[j].Comparison_BreakTorque = false;
            }
        }

        /// <summary>
        ///  对比残留转矩结果
        /// </summary>
        /// <param name="Torque"></param>
        private void Comparison_MotorRemainTorque(float Torque)
        {
            if (Torque <= BrakeModel.Remain_torH)
            {
                bt残留转矩测试结果.BackColor = Color.LimeGreen;
                bt残留转矩测试结果.Text = "合格";
                GlobalData.TestComparison[5] = true;
                Body[j].Comparison_RemainTorque = true;
            }
            else
            {
                tx残留转矩.BackColor = Color.Red;
                bt残留转矩测试结果.BackColor = Color.Red;
                bt残留转矩测试结果.Text = "不合格";
                GlobalData.TestComparison[5] = true;
                Body[j].Comparison_RemainTorque = false;
            }
        }
        /// <summary>
        ///  对比60%电压测试结果
        /// </summary>
        /// <param name="Current60"></param>
        /// <param name="Power60"></param>
        private void Comparison_60Power(float Current60, float Power60)
        {
            if (Current60 <= BrakeModel.Rated_60pIH && Current60 >= BrakeModel.Rated_60pIL)
            {
                Body[j].Comparison_Current60p = true;
            }
            else
            {
                tx60p电压修正电流.BackColor = Color.Red;
                Body[j].Comparison_Current60p = false;
            }

            if (Power60 <= BrakeModel.Rated_60pPH && Power60 >= BrakeModel.Rated_60pPL)
            {
                Body[j].Comparison_Power60p = true;
            }
            else
            {
                tx60p电压修正功率.BackColor = Color.Red;
                Body[j].Comparison_Power60p = false;
            }
            if (Body[j].Comparison_Power60p && Body[j].Comparison_Current60p)
            {
                bt60p电压测试结果.Text = "合格";
                bt60p电压测试结果.BackColor = Color.LimeGreen;
                GlobalData.TestComparison[6] = true;
            }
            else
            {
                bt60p电压测试结果.Text = "不合格";
                bt60p电压测试结果.BackColor = Color.Red;
                GlobalData.TestComparison[6] = false;

            }
            GlobalData.TestOver = true;
        }
        #endregion

        /// <summary>
        ///   窗体初始化
        /// </summary>
        private void InitialForm_Station1()         //Station1 窗体初始化
        {
            txR1测试值.Text = "";
            txR1测试值.BackColor = Color.WhiteSmoke;
            txR2测试值.Text = "";
            txR2测试值.BackColor = Color.WhiteSmoke;
            tx测试温度值.Text = "";
            bt电阻测试结果.Text = "等待测试...";
            bt电阻测试结果.BackColor = Color.Gray;
            tx绝缘值.Text = "";
            bt绝缘测试结果.Text = "等待测试...";
            bt绝缘测试结果.BackColor = Color.Gray;
            tx耐压电压.Text = "";
            tx泄露电流.Text = "";
            bt耐压测试结果.Text = "等待测试...";
            bt耐压测试结果.BackColor = Color.Gray;
            Sum60pTestTimes = 0;
            SumReadtedTestTimes = 0;
            Sum60pCurrent = 0;
            Sum60pPower = 0;
            SumReatedCurrent = 0;
            SumReatedPower = 0;
            for (int i = 0; i < 9; i++)
            {
                GlobalData.TestComparison[i] = true;
            }
            for (int i = 0; i < 8; i++)
            {
                GlobalData.TestState[i] = false;
            }
            toolStripStatusLabel4.Text = "系统当前状态:等待测试...";
            toolStripProgressBar3.Value = 0;
        }
        /// <summary>
        ///  窗体初始化
        /// </summary>
        private void InitialForm_Station2()         //Station2 窗体初始化
        {
            tx额定电压值.Text = "";
            tx实际电流值.Text = "";
            tx实际电流值.BackColor = Color.WhiteSmoke;
            tx修正电流值.BackColor = Color.WhiteSmoke;
            tx修正电流值.Text = "";
            tx实际功率.Text = "";
            tx实际功率.BackColor = Color.WhiteSmoke;
            tx修正功率.BackColor = Color.WhiteSmoke;
            tx修正功率.Text = "";
            bt额定电参数测试结果.Text = "等待测试...";
            bt额定电参数测试结果.BackColor = Color.Gray;

            tx制动器跑合速度.Text = "";
            tx制动器跑合转矩.Text = "";
            tx制动器跑合转矩.BackColor = Color.WhiteSmoke;
            bt制动器跑合结果.Text = "等待测试...";
            bt制动器跑合结果.BackColor = Color.Gray;

            tx制动器转矩.Text = "";
            tx制动器转矩.BackColor = Color.WhiteSmoke;
            tx制动器转矩转速.Text = "";
            bt制动器转矩测试结果.Text = "等待测试...";
            bt制动器转矩测试结果.BackColor = Color.Gray;
            tx残留转矩电压.Text = "";
            tx残留转矩转速.Text = "";
            tx残留转矩.Text = "";
            tx残留转矩.BackColor = Color.WhiteSmoke;
            bt残留转矩测试结果.Text = "等待测试...";
            bt残留转矩测试结果.BackColor = Color.Gray;

            tx60p电压.Text = "";
            tx60p电压实际电流.Text = "";
            tx60p电压实际电流.BackColor = Color.WhiteSmoke;
            tx60p电压修正电流.BackColor = Color.WhiteSmoke;
            tx60p电压修正电流.Text = "";
            tx60p电压实际功率.Text = "";
            tx60p电压实际功率.BackColor = Color.WhiteSmoke;
            tx60p电压修正功率.BackColor = Color.WhiteSmoke;
            tx60p电压修正功率.Text = "";
            bt60p电压测试结果.Text = "等待测试...";
            bt60p电压测试结果.BackColor = Color.Gray;

            btn_Colusion.Text = "";
            btn_Colusion.BackColor = Color.Gray;

            tx实测左间隙.Text = "";
            tx实测右间隙.Text = "";
            btnDisplacement.Text = "等待测试...";
            btnDisplacement.BackColor = Color.Gray;

            for (int i = 0; i < 9; i++)
            {
                GlobalData.TestComparison[i] = true;
            }
            for (int i = 0; i < 8; i++)
            {
                GlobalData.TestState[i] = false;
            }
            toolStripStatusLabel7.Text = "系统当前状态:等待测试...";
            toolStripProgressBar2.Value = 0;
        }

        private void InitialFormOrderInfo()
        {
            Services.BrakeDbContext context = new Services.BrakeDbContext();
            var VoltageModels = context.VoltageModel.ToList();
            GlobalData.BrakeVoltageDb = VoltageModels;
        }

        /// <summary>
        /// 显示设备状态
        /// </summary>
        private async void ShowDriveState()
        {
            await Task.Delay(3000);
            while (true)
            {
                await Task.Delay(100);

                notificationBox1.NotificationType = PLC.EmergencyStopButton
                    ? ReaLTaiizor.Controls.NotificationBox.Type.Notice
                    : ReaLTaiizor.Controls.NotificationBox.Type.Error;

                notificationBox2.NotificationType = !PLC.DoorSafety
                    ? ReaLTaiizor.Controls.NotificationBox.Type.Notice
                    : ReaLTaiizor.Controls.NotificationBox.Type.Error;

                notificationBox3.NotificationType = PLC.MainCylinderLimitState == 1
                    ? ReaLTaiizor.Controls.NotificationBox.Type.Notice
                    : ReaLTaiizor.Controls.NotificationBox.Type.Warning;

                BrakeTestService.HasStop();
            }
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="Information"></param>
        /// <param name="Time"></param>
        private void PopupWindowsShow(string Information, int Time)
        {
            HslCommunication.BasicFramework.FormPopup popup = new HslCommunication.BasicFramework.FormPopup(Information, Color.Blue, Time);
            popup.Show();
        }

        /// <summary>
        ///  本底校验
        /// </summary>
        private void Background()
        {
            string sql = "select *from 本底值 where ID='1'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, GlobalData.mysql);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tx绝缘电阻本底值.Text = ds.Tables[0].Rows[0]["绝缘电阻"].ToString();
                tx本底泄露电流.Text = ds.Tables[0].Rows[0]["泄露电流"].ToString();
                tx制动器转矩本底.Text = ds.Tables[0].Rows[0]["转矩"].ToString();
            }
            ds.Dispose();
        }

        #region 读写XML 文件 方法

        #region 读取补偿参数方法
        /// <summary>
        ///  定义XML 读取出的 类 补偿用
        /// </summary>
        SaveDataClass saveDataClass;
        /// <summary>
        /// 将补偿参数从XML中读取 补偿用
        /// </summary>
        /// <param name="filepath"></param>
        private bool ReadParameterFromXml()
        {
            try
            {

                string strAppPath = Application.StartupPath; //获得可执bai行文件的du路径。zhi
                string strConfigPath = strAppPath + "\\MyXml\\" + "230" + ".xml"; //自己调dao整一下相对zhuan路shu径。

                //判断是否存在这个文件
                if (File.Exists(strConfigPath))
                {
                    //再做操作
                    //从test.xml文件中反序列化出来
                    saveDataClass = new SaveDataClass();
                    using (FileStream fsReader = new FileStream(strConfigPath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(SaveDataClass));
                        saveDataClass = xs.Deserialize(fsReader) as SaveDataClass;
                        if (saveDataClass.list.Count > 0)
                        {
                            int listCounts = 0;

                        }

                    }
                    return true;
                }
                else
                {
                    //如果判断不存在这个文件 则读取PLC 内容
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("读取参数文件XML错误");
                return false;
            }


        }
        #endregion

        #region 保存 读取 工单号XML
        /// <summary>
        /// 将参数保存到XML 每一个工单号 对应的 数量
        /// </summary>
        /// <param name="filepath"></param>
        private void WriteProductToXml()
        {
            try
            {

                string strAppPath = Application.StartupPath; //获得可执bai行文件的du路径。zhi
                string strConfigPath = strAppPath + "\\MyProductXml\\" + txProductID.Text.Trim() + ".xml"; //自己调dao整一下相对zhuan路shu径。
                //xml序列化到test.xml文件中
                SaveDataProductIdClass saveDataProductIdClass = new SaveDataProductIdClass();
                saveDataProductIdClass.ProductId = txProductID.Text.Trim();
                saveDataProductIdClass.ProductSequqence = (Convert.ToInt16(tx_testSquence.Text.Trim()) + 1).ToString();

                SaveDataClassIndex.ProductId = txProductID.Text.Trim();

                using (FileStream fsWriter = new FileStream(strConfigPath, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(SaveDataProductIdClass));
                    xs.Serialize(fsWriter, saveDataProductIdClass);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("保存参数文件XML错误");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        SaveDataProductIdClass saveDataProductIdClass;
        /// <summary>
        /// 将参数从XML中读取 每一个工单号 对应的 数量
        /// </summary>
        /// <param name="filepath"></param>
        private bool ReadProductFromXml()
        {
            try
            {
                tx_testSquence.Text = "0";
                string strAppPath = Application.StartupPath; //获得可执bai行文件的du路径。zhi
                string strConfigPath = strAppPath + "\\MyProductXml\\" + txProductID.Text.Trim() + ".xml"; //自己调dao整一下相对zhuan路shu径。

                //判断是否存在这个文件
                if (File.Exists(strConfigPath))
                {
                    //再做操作
                    //从test.xml文件中反序列化出来
                    using (FileStream fsReader = new FileStream(strConfigPath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(SaveDataProductIdClass));
                        saveDataProductIdClass = xs.Deserialize(fsReader) as SaveDataProductIdClass;

                        txProductID.Text = saveDataProductIdClass.ProductId;
                        tx_testSquence.Text = saveDataProductIdClass.ProductSequqence;
                    }
                    return true;
                }
                else
                {
                    //如果判断不存在这个文件 则读取PLC 内容
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("读取参数文件XML错误");
                return false;
            }


        }
        #endregion

        #region 保存 读取 工单号XML 配置文件
        /// <summary>
        /// 将参数保存到XML 此处作用为 关机保持 
        /// </summary>
        /// <param name="filepath"></param>
        private void WriteProductTxtToXml()
        {
            try
            {
                string strAppPath = Application.StartupPath; //获得可执bai行文件的du路径。zhi
                string strConfigPath = strAppPath + "\\MyProductTxtXml\\txProductID.xml"; //自己调dao整一下相对zhuan路shu径。
                //xml序列化到test.xml文件中
                SaveProductTxt saveProductTxt = new SaveProductTxt();
                saveProductTxt.ProductIdTxt = txProductID.Text.Trim();

                using (FileStream fsWriter = new FileStream(strConfigPath, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(SaveProductTxt));
                    xs.Serialize(fsWriter, saveProductTxt);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("保存参数文件XML错误");
            }

        }
        #endregion

        #endregion
        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAppExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 调试测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebugTest_Click(object sender, EventArgs e)
        {
            log.DebugFormat("打开调试窗口");
            debugTest debugTests = new debugTest();
            debugTests.ShowDialog();
        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetConfigs_Click(object sender, EventArgs e)
        {
            log.DebugFormat("打开设置窗口");
            SetData setData = new SetData();
            setData.ShowDialog();
        }

        /// <summary>
        /// 启动测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //InitialForm_Station1();
            //InitialForm_Station2();
            //IsStartTest01ThreadRun = true;
            //StartSimulation();
            BrakeTestService.SoftStartFlag = true;
        }

        private async void StartSimulation()
        {
            Random ran = new Random();
            bool insulaStatus = false;
            bool withStand = false;
            if (IsStartTest01ThreadRun)
            {
                //电阻测试
                if (ISResistace.Checked)
                {
                    toolStripStatusLabel4.Text = "电阻AB测试中";
                    bt电阻测试结果.Text = "电阻AB测试中...";
                    await Task.Delay(1000);
                    toolStripStatusLabel4.Text += "系统当前状态：电阻AB测试中...";
                    toolStripProgressBar3.Value = 0;
                    toolStripProgressBar3.Value += 25 * 1;
                    bt电阻测试结果.BackColor = Color.Yellow;
                    await Task.Delay(1000);
                    tx测试温度值.Text = "20";
                    await Task.Delay(1000);
                    double randNum = NextFloat(ran, BrakeModel.R1_H, BrakeModel.R1_L, 2);
                    txR1测试值.Text = randNum.ToString();
                    await Task.Delay(1000);
                    float R1 = float.Parse(txR1测试值.Text);
                    await Task.Delay(1500);
                    ComparsionSimulate_R1(R1);
                    await Task.Delay(2000);
                    toolStripStatusLabel4.Text = "电阻BC测试中";
                    bt电阻测试结果.Text = "电阻BC测试中...";
                    bt电阻测试结果.BackColor = Color.Yellow;
                    await Task.Delay(1000);
                    toolStripStatusLabel4.Text = "系统当前状态:电阻BC测试中";
                    toolStripProgressBar3.Value = 0;
                    toolStripProgressBar3.Value = 25 * 1;

                    double randNumR2 = NextFloat(ran, BrakeModel.R2_H, BrakeModel.R2_L, 2);
                    txR2测试值.Text = randNum.ToString();

                    await Task.Delay(3000);
                    float R2 = float.Parse(txR2测试值.Text);
                    ComparsionSimulate_R2(R2);
                    //比对合格
                    await Task.Delay(2000);
                    insulaStatus = true;
                    await Task.Delay(1000);
                    withStand = true;

                }
                await Task.Delay(3000);
                if (ISWithStand.Checked)
                {
                    //绝缘测试
                    if (insulaStatus == true)
                    {
                        await Task.Delay(3000);
                        GlobalData.GetAinuoSendTimes = 1;
                        bt绝缘测试结果.Text = "绝缘电阻测量中...";
                        bt绝缘测试结果.BackColor = Color.Yellow;
                        toolStripStatusLabel4.Text = "系统当前状态:绝缘电阻测量...";
                        toolStripProgressBar3.Value = 0;
                        toolStripProgressBar3.Value = 25 * 1;
                        int irl = Convert.ToInt32(BrakeModel.IR_L);
                        int ranIrl = ran.Next(irl);
                        tx绝缘值.Text = ranIrl.ToString();
                        await Task.Delay(3000);
                        //转换
                        float Rm = float.Parse(tx绝缘值.Text);
                        //比对结果
                        ComparsionSimulate_insulate(Rm);
                        await Task.Delay(1000);
                    }
                    //耐压测试
                    if (withStand == true)
                    {
                        await Task.Delay(3000);
                        GlobalData.GetAinuoSendTimes = 2;
                        bt耐压测试结果.Text = "耐压测试中...";
                        bt耐压测试结果.BackColor = Color.Yellow;
                        toolStripProgressBar3.Value = 0;
                        toolStripProgressBar3.Value = 25 * 1;
                        int leakCurrent = ran.Next(1, 10);
                        tx泄露电流.Text = leakCurrent.ToString();
                        await Task.Delay(3000);
                        float leak = float.Parse(tx泄露电流.Text);
                        await Task.Delay(1000);
                        //比对结果
                        ComparsionSimulate_WithStand(leak);
                    }
                }
            }
        }

        private void ComparsionSimulate_R1(float r)
        {
            if (r > BrakeModel.R1_H || r < BrakeModel.R1_L)
            {
                txR1测试值.BackColor = Color.Blue;
                bt电阻测试结果.BackColor = Color.Blue;
                bt电阻测试结果.Text = "AB电阻测试不合格";
                GlobalData.TestComparison[0] = false;
                toolStripProgressBar2.BackColor = Color.Blue;
            }
            else if (r <= BrakeModel.R1_H && r >= BrakeModel.R1_L)
            {
                GlobalData.TestComparison[0] = true;
                bt电阻测试结果.BackColor = Color.Green;
                bt电阻测试结果.Text = "AB电阻测试合格";
            }
        }

        private void ComparsionSimulate_R2(float r)
        {
            if (r > BrakeModel.R2_H || r < BrakeModel.R2_L)
            {
                txR2测试值.BackColor = Color.Red;
                bt电阻测试结果.BackColor = Color.Red;
                bt电阻测试结果.Text = "BC电阻测试不合格";
                GlobalData.TestComparison[1] = false;
                toolStripProgressBar3.BackColor = Color.Red;
            }
            else
            {
                GlobalData.TestComparison[1] = true;
                if (GlobalData.TestComparison[0] == true)
                {
                    bt电阻测试结果.BackColor = Color.LimeGreen;
                    bt电阻测试结果.Text = "BC电阻测试合格";
                }
            }
        }

        private void ComparsionSimulate_insulate(float Rm)
        {
            if (Rm < BrakeModel.IR_L)
            {
                bt绝缘测试结果.BackColor = Color.Red;
                bt绝缘测试结果.Text = "不合格";
            }
            else
            {
                bt绝缘测试结果.BackColor = Color.LimeGreen;
                bt绝缘测试结果.Text = "合格";
            }
        }

        private void ComparsionSimulate_WithStand(float leakCurrent)
        {
            if (leakCurrent <= BrakeModel.Leak_AH)
            {
                bt耐压测试结果.BackColor = Color.LimeGreen;
                bt耐压测试结果.Text = "合格";
            }
            else
            {
                bt耐压测试结果.BackColor = Color.Red;
                bt耐压测试结果.Text = "不合格";
            }
        }

        public double NextFloat(Random ran, float minValue, float maxValue, int floatPlcat)
        {
            double randNum = ran.NextDouble() * (maxValue - minValue) + minValue;
            return Convert.ToDouble(randNum.ToString("f") + floatPlcat);
        }

        /// <summary>
        /// 制动器测试结果上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrakeResult_Click(object sender, EventArgs e)
        {
            BrakeSave();
            //WebApiFuction webApiFuction;
            //webApiFuction = new WebApiFuction(TestMotorSample[i]);
            //ClassTestBenchbrakeRequest requestData = WebApiFuction.
            ////工单带出制动器标准

            //var testBenchbrakeWriteClass = WebApiFuction.WriteSceneReport(classTestBenchbrakeRead);
            //int statusWriteProductId = WebApiFuction.WriteProductId(testBenchbrakeWriteClass);
        }

        private string BrakeSave()
        {
            try
            {
                //ClassTestBenchbrakeRequest requestData = WebApiFuction.WriteSceneReport();
                log.Debug($"制动器数据保存");
                var uploadData = WebApiFuction.WriteProductIdReport(Body);
                uploadData.brakeRoutineTestId = brakeWorkOrderService.BrakeOrderNature.id;
                uploadData.testTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (string.IsNullOrEmpty(tx_testSquence.Text)) tx_testSquence.Text = "1";
                uploadData.sequence = Convert.ToInt32(tx_testSquence.Text);
                WebApiFuction apiFuction = new WebApiFuction();
                return apiFuction.UploadBrakeReport(uploadData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return "上传数据异常!" + ex;
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            BrakeTestService = new Services.TestServices.BrakeTestService(this);
            PLC.StartPLC();
            Services.Meter.CS9914AX.Initialization();
            Services.Meter.BS601102C.Initialization();
            Services.Meter.TH2512B.Initialization();
            Services.Meter.TH2683B.Initialization();
            Services.Meter.WT310E.Initialization();
            Services.Meter.KBM50MODBUS.Initialization();
            Link_SerialZebra();
            RunMainTestProcessAsync();
            InitialFormOrderInfo();
            ShowDriveState();
            //CloseLoadFromAsync();
        }

        private void txProductID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_GetApi_Click(txProductID, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            数据库 frmFind = new 数据库();
            frmFind.Show();
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void txProductID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
