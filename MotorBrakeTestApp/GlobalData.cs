using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml.Serialization;

namespace MotorBrakeTestApp
{
    public class GlobalData
    {
        /// <summary>
        /// 用于判断测试的步骤   TestState[0]:R1 ;TestState[1]:R2;TestState[2]:耐压、绝缘;TestState[3]:额定功率测试;TestState[4]:跑合测试;TestState[5]:制动测试;TestState[6]:残留测试;TestState[7]:60%电压测试;
        /// </summary>
        public static bool[] TestState { get; set; } = new bool[10] { false, false, false, false, false, false, false, false, false, false };  //用于判断测试的步骤   TestState[0]:R1 ;TestState[1]:R2;TestState[2]:耐压、绝缘;TestState[3]:额定功率测试;TestState[4]:跑合测试;TestState[5]:制动测试;TestState[6]:残留测试;TestState[7]:60%电压测试;
        /// <summary>
        /// 用于存储测试对比结果 TestComparison[0]:R1对比结果;
        /// </summary>
        public static bool[] TestComparison { get; set; } = new bool[9] { true, true, true, true, true, true, true, true, true };//用于存储测试对比结果 TestComparison[0]:R1对比结果;
        public static Configuration config { get; } = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        /// <summary>
        /// 测试结果数据
        /// </summary>
        public static WebApi.Write.ClassTestBenchbrakeWrite ClassTestBenchbrakeWrite { get; set; }

        /// <summary>
        /// 制动器电压关系表
        /// </summary>
        public static List<Services.VoltageModels> BrakeVoltageDb { get; set; }
        public static Services.VoltageModels SelectedBrakeVoltage { get; set; }

        public struct Product
        {
            /// <summary>
            /// 产品型号
            /// </summary>
            public string Model;
            /// <summary>
            /// 产品测试时间
            /// </summary>
            public string TestTime;
            /// <summary>
            /// 产品测试时钟
            /// </summary>
            public string TestDate;
            /// <summary>
            /// 产品工单号ID
            /// </summary>
            public string ProductID;
            public string R1;
            public string R1_Correction { get; set; }
            public string R2;
            public string R2_Correction { get; set; }
            public string Temper;
            public string Rm;
            public string Rm_Voltage;
            public string LeakCurrent { get; set; }
            /// <summary>
            ///  //耐压
            /// </summary>
            public string WSVoltage;
            /// <summary>
            /// //额定电压
            /// </summary>
            public string RD_Voltage;
            /// <summary>
            /// // 额定电流
            /// </summary>
            public string RD_Current;
            /// <summary>
            ///   //修正电流
            /// </summary>
            public string RD_Current_Correction;
            /// <summary>
            ///  //额定功率
            /// </summary>
            public string RD_Power;
            /// <summary>
            ///  //修正功率
            /// </summary>
            public string RD_Power_Correction;
            /// <summary>
            /// 间隙Value
            /// </summary>
            public string IntervelLeft;
            /// <summary>
            /// 间隙Value
            /// </summary>
            public string IntervelRight;
            /// <summary>
            ///  //跑合转速
            /// </summary>
            public string Runspeed;
            /// <summary>
            ///  //跑合转矩
            /// </summary>
            public string RunTorque;
            /// <summary>
            ///  //制动转速
            /// </summary>
            public string Breakspeed;
            /// <summary>
            ///  //制动转矩
            /// </summary>
            public string BreakTorque;
            /// <summary>
            /// //残留转矩电压
            /// </summary>
            public string RemainVoltage;
            /// <summary>
            ///  //残留转矩
            /// </summary>
            public string Remainspeed;
            /// <summary>
            ///  //残留转矩
            /// </summary>
            public string RemainTorque;
            /// <summary>
            ///  //静态转矩
            /// </summary>
            public string StaticToque;
            /// <summary>
            ///  //60%电压
            /// </summary>
            public string Voltage60p;
            /// <summary>
            ///  //60%电流
            /// </summary>
            public string Current60p;
            /// <summary>
            ///  //修正电流
            /// </summary>
            public string Current60p_Correction;
            /// <summary>
            ///  //60%功率
            /// </summary>
            public string Power60p;
            /// <summary>
            ///  //修正功率
            /// </summary>
            public string Power60p_Correction;
            /// <summary>
            ///  //电阻对比结果
            /// </summary>
            public bool Comparison_R;
            /// <summary>
            ///  //绝缘电阻电阻对比结果
            /// </summary>
            public bool Comparison_Rm;
            /// <summary>
            ///  //额定功率对比结果
            /// </summary>
            public bool Comparison_RDPower;
            /// <summary>
            /// //间隙对比结果
            /// </summary>
            public bool Comparison_Intervel;
            /// <summary>
            /// //额定电流对比结果
            /// </summary>
            public bool Comparison_RDCurrent;
            /// <summary>
            /// //跑合转矩对比结果
            /// </summary>
            public bool Comparison_RunTorque;
            /// <summary>
            ///  //制动转矩对比结果
            /// </summary>
            public bool Comparison_BreakTorque;
            /// <summary>
            /// //残留转矩对比结果
            /// </summary>
            public bool Comparison_RemainTorque;
            /// <summary>
            /// //60%功率对比结果
            /// </summary>
            public bool Comparison_Power60p;
            /// <summary>
            /// //60%电流对比结果
            /// </summary>
            public bool Comparison_Current60p;
            /// <summary>
            /// //耐压对比结果
            /// </summary>
            public bool Comparison_LeakCurrent;
            /// <summary>
            /// //最后测试结果
            /// </summary>
            public bool Comparison_Total;
            /// <summary>
            /// //测试次数; 
            /// </summary>
            public string TestTimes;
            /// <summary>
            /// // 测试使用总时间 
            /// </summary>
            public string UseTime;
            /// <summary>
            ///  //序号(暂时不知道如何人工干预)
            /// </summary>
            public string sequence;
        }

        public struct Model
        {
            /// <summary>
            /// bs 电阻min
            /// </summary>
            public float R1_L;
            /// <summary>
            /// bs 电阻max
            /// </summary>
            public float R1_H;
            /// <summary>
            /// ts 电阻min
            /// </summary>
            public float R2_L;
            /// <summary>
            /// ts 电阻max
            /// </summary>
            public float R2_H;
            /// <summary>
            /// 电阻测试时间
            /// </summary>
            public float R_TestTime;
            /// <summary>
            /// 电阻测试基准温度
            /// </summary>
            public float StandardTemper;
            /// <summary>
            ///  电阻温度系数
            /// </summary>
            public float Tempercoefficient;
            /// <summary>
            /// 额定电压
            /// </summary>
            public float Rated_V { get; set; }
            /// <summary>
            /// 满电压电流min
            /// </summary>
            public float Rated_IL;
            /// <summary>
            /// 满电压电流max
            /// </summary>
            public float Rated_IH;
            /// <summary>
            ///  满电压功率min
            /// </summary>
            public float Rated_PL;
            /// <summary>
            /// 满电压功率max
            /// </summary>
            public float Rated_PH;
            /// <summary>
            /// 满电压测试时间
            /// </summary>
            public float Rated_PowerTime { get; set; }
            /// <summary>
            ///  跑合速度
            /// </summary>
            public float Run_Ver { get; set; }
            /// <summary>
            /// 跑合转矩min
            /// </summary>
            public float Run_torL;
            /// <summary>
            /// 跑合转矩max
            /// </summary>
            public float Run_torH;
            /// <summary>
            /// 跑合时间min
            /// </summary>
            public float Run_TimeL;
            /// <summary>
            /// 跑合时间max
            /// </summary>
            public float Run_TimeH;
            /// <summary>
            /// 制动转速
            /// </summary>
            public float Brake_Ver { get; set; }
            /// <summary>
            ///  制动转矩min
            /// </summary>
            public float Brake_torL;
            /// <summary>
            /// 制动转矩max
            /// </summary>
            public float Brake_torH;
            /// <summary>
            /// 制动力持续矩时间（s）
            /// </summary>
            public float BrakeTorqueTime { get; set; }
            /// <summary>
            /// 残留转矩min
            /// </summary>
            public float Remain_torL;
            /// <summary>
            /// 残留转矩max
            /// </summary>
            public float Remain_torH;
            /// <summary>
            /// 残余转矩测试时间（s）
            /// </summary>
            public float ResidualTorqueTime { get; set; }
            /// <summary>
            /// 60%电压
            /// </summary>
            public float Rated_60pV;
            /// <summary>
            /// 60%电压电流min 
            /// </summary>
            public float Rated_60pIL;
            /// <summary>
            /// 60%电压电流max
            /// </summary>
            public float Rated_60pIH;
            /// <summary>
            /// 60%电压功率min
            /// </summary>
            public float Rated_60pPL;
            /// <summary>
            /// 60%电压功率max
            /// </summary>
            public float Rated_60pPH;
            /// <summary>
            /// 60电压测试时间
            /// </summary>
            public float Rated_60pTimer { get; set; }
            /// <summary>
            /// 绝缘电阻min
            /// </summary>
            public float IR_L;
            /// <summary>
            /// 绝缘电阻max
            /// </summary>
            public float IR_H;
            /// <summary>
            /// 绝缘电阻测试时间
            /// </summary>
            public int IR_TestTime;
            /// <summary>
            /// 绝缘电阻测试电压
            /// </summary>
            public int IR_TestVoltage;
            /// <summary>
            ///  耐压电压
            /// </summary>
            public int WithStand_V { get; set; }
            /// <summary>
            /// 泄露电流min
            /// </summary>
            public float Leak_AL { get; set; }
            /// <summary>
            /// 泄露电流max
            /// </summary>
            public float Leak_AH { get; set; }
            /// <summary>
            /// 耐压测试时间 s
            /// </summary>
            public int WithStandV_Time { get; set; }
            /// <summary>
            /// 间隙本底值1
            /// </summary>
            public double DisplacementBackgroundValue1 { get; set; }
            /// <summary>
            /// 间隙本底值2
            /// </summary>
            public double DisplacementBackgroundValue2 { get; set; }
            /// <summary>
            /// 间隙值1
            /// </summary>
            public double DisplacementValue1 { get; set; }
            /// <summary>
            /// 间隙值2
            /// </summary>
            public double DisplacementValue2 { get; set; }
            /// <summary>
            /// 间隙值
            /// </summary>
            public float Intervel_Value;
            /// <summary>
            /// 间隙最大值
            /// </summary>
            public float Intervel_Max;
            /// <summary>
            /// 间隙最小值
            /// </summary>
            public float Intervel_Min;
            /// <summary>
            /// 备用温度min
            /// </summary>
            public float Spare_temperL;
            /// <summary>
            /// 备用温度max
            /// </summary>
            public float Spare_temperH;
            /// <summary>
            /// 备用温度 
            /// </summary>
            public float Spare_temper;
            //public float 
        }

        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                Application.DoEvents();//可执行某无聊的操作
            }
        }

        public static void UpdataParameter()
        {
            config.AppSettings.Settings["PLC IP"].Value = PLC_IP;
            config.AppSettings.Settings["数据库地址"].Value = Sql_add;
            config.AppSettings.Settings["数据库账号"].Value = Sql_ID;
            config.AppSettings.Settings["数据库密码"].Value = Sql_pw;
            config.AppSettings.Settings["绝缘本底值"].Value = Insulation_BValue;
            config.AppSettings.Settings["直流电阻COM"].Value = R_DC;
            config.AppSettings.Settings["直流电阻IP"].Value = R_DC_IP;
            config.AppSettings.Settings["直流电阻端口"].Value = R_DC_Port;
            config.AppSettings.Settings["电能测试仪COM"].Value = Poewr_Tester;
            config.AppSettings.Settings["电能测试仪IP"].Value = Poewr_Tester_IP;
            config.AppSettings.Settings["电能测试仪端口"].Value = Poewr_Tester_Port;
            config.AppSettings.Settings["耐压测试仪COM"].Value = WithstandPortName;
            config.AppSettings.Settings["耐压测试仪IP"].Value = Angui_Tester_IP;
            config.AppSettings.Settings["耐压测试仪端口"].Value = Angui_Tester_Port;
            config.AppSettings.Settings["绝缘表COM"].Value = Power_digital;
            config.AppSettings.Settings["绝缘表IP"].Value = Power_digital_IP;
            config.AppSettings.Settings["绝缘表端口"].Value = Power_digital_Port;
            config.AppSettings.Settings["Excel保存路径"].Value = GlobalData.ExcelFilePath;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        #region 声明公共变量
        public static int Log_Level { get; set; } = -1;
        public static bool Log_State { get; set; }
        public static string Log_Name { get; set; }
        public static string PLC_IP { get; set; } = ConfigurationManager.AppSettings["PLC IP"];
        public static string Sql_add { get; set; } = ConfigurationManager.AppSettings["数据库地址"];
        public static string Sql_ID { get; set; } = ConfigurationManager.AppSettings["数据库账号"];
        public static string Sql_pw { get; set; } = ConfigurationManager.AppSettings["数据库密码"];
        public static string Insulation_BValue { get; set; } = ConfigurationManager.AppSettings["绝缘本底值"];
        /// <summary>
        /// 此COM口作为 扫码枪的COM口
        /// </summary>
        public static string R_DC { get; set; } = ConfigurationManager.AppSettings["直流电阻COM"];
        public static string R_DC_IP { get; set; } = ConfigurationManager.AppSettings["直流电阻IP"];
        public static string R_DC_Port { get; set; } = ConfigurationManager.AppSettings["直流电阻端口"];
        public static string Poewr_Tester { get; set; } = ConfigurationManager.AppSettings["电能测试仪COM"];
        public static string Poewr_Tester_IP { get; set; } = ConfigurationManager.AppSettings["电能测试仪IP"];
        public static string Poewr_Tester_Port { get; set; } = ConfigurationManager.AppSettings["电能测试仪端口"];
        public static string WithstandPortName { get; set; } = ConfigurationManager.AppSettings["耐压测试仪COM"];
        public static string Angui_Tester_IP { get; set; } = ConfigurationManager.AppSettings["耐压测试仪IP"];
        public static string Angui_Tester_Port { get; set; } = ConfigurationManager.AppSettings["耐压测试仪端口"];
        public static string Power_digital { get; set; } = ConfigurationManager.AppSettings["绝缘表COM"];
        public static string Power_digital_IP { get; set; } = ConfigurationManager.AppSettings["绝缘表IP"];
        public static string Power_digital_Port { get; set; } = ConfigurationManager.AppSettings["绝缘表端口"];
        public static string ExcelFilePath { get; set; } = ConfigurationManager.AppSettings["Excel保存路径"];                //Excel文件保存路径
        public static string RemoteSetAinuo { get; set; } = ConfigurationManager.AppSettings["远程设置耐压"]; //远程设置安规测试仪;
        public static string SinglePowerSource { get; private set; } = ConfigurationManager.AppSettings["单相电源串口号"];
        public static bool HardwareDebug { get => GetBool(ConfigurationManager.AppSettings["硬件调试状态"]); }
        public static string TempModulePortName { get => ConfigurationManager.AppSettings["温度模块COM"]; }

        //public static string sqlconstr = "Data Source=8.130.166.6;Initial Catalog=MotorResultData;User ID=sa;Password=Sasa1234";
        //  public static string sqlconstr = "Server=" + Class1GlobalData.sql_add + ";user=" + Class1GlobalData.sql_ID + ";pwd=" + Class1GlobalData.sql_pw + ";database=motorresultdata;port=3306";

        //public static string connString = "server=8.130.166.6;user=root;password=abc123456;database=motorresultdata";
        //public static string connString = "server=8.130.166.6;port=3306;user=root;password=abc123456;database=motorresultdata";
        //string connstring= ($"data source={ Class1GlobalData.sql_add};initial catalog={databaseName};user id={databaseUser};password={databasePassword}", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.21-mysql"));

        public static string data = "motorresultdata";
        public static string connStr = $"data source = {GlobalData.Sql_add};initial catalog = {data};user id = {GlobalData.Sql_ID};password = {GlobalData.Sql_pw}";

        public static SqlConnection mysql = new SqlConnection(connStr);
        public static string Log_UserID;
        public static bool PLC_Connect_State = false;
        public static bool WT310E_Connect_State = false;
        public static bool TH2516_Connect_State = false;
        public static bool Ainuo_Connect_State = false;
        public static bool Digital_Power_Connect_State = false;
        public static bool DCR_needTest = false;
        public static bool IR_needTest = false;
        public static bool RetedV_needTest = false;
        public static bool Reted60pV_needTest = false;
        public static bool Intervel_needTest = false;
        public static bool Run_needTest = false;
        public static bool Runtor_needTest = false;
        public static bool Remain_needTest = false;
        public static bool WithstandV_needTest = false;
        public static string[] PLCIO = new string[200];
        public static bool TestStart = false;  //测试开始
        public static bool TestOver = false;   //测试结束
        public static int TotalMakeQty = Int16.Parse(ConfigurationManager.AppSettings["当日产量"].Trim());        //当日总产量
        public static int TotalBadQty = Int16.Parse(ConfigurationManager.AppSettings["当日不良品数量"].Trim());          //当日不良品数量
        public static int TotalGoodQty = Int16.Parse(ConfigurationManager.AppSettings["当日良品数量"].Trim());        //当日良品数量
        /// <summary>
        /// 安规测试步骤 1代表绝缘测试  2代表耐压测试
        /// </summary>
        public static int GetAinuoSendTimes = 0;
        /// <summary>
        ///  用于测试的电阻是R1 AB 还是R2 BC
        /// </summary>
        public static int TH2516TestTimes = 0;
        /// <summary>
        /// 每小时的不良品数量
        /// </summary>
        public static float[] EveryHBadQty = new float[24];//每小时的不良品数量
        public static bool SystemitialFlag;//初始化标志位
        public static int[] ReadNetCFailTimes = new int[4] { 0, 0, 0, 0 };                            //四个变量用于记录读取失败错误次数，用于检测连接状态 1.PLC 2.安规测试仪 3.电阻测试仪   4.电压表             
        public static bool AppRestart; //软件重启
        #endregion

        public static bool GetBool(string str)
        {
            if (bool.TryParse(str, out bool b))
            {
                return b;
            }
            return false;
        }

        public static void ReadPLCIO()
        {
            string[] keyName = ConfigurationManager.AppSettings.AllKeys;
            for (int i = 0; i < keyName.Length; i++)
            {
                PLCIO[i] = ConfigurationManager.AppSettings[keyName[i]];
            }
            for (int j = 0; j < 24; j++)
            {
                EveryHBadQty[j] = float.Parse(PLCIO[102 + j].Trim());
            }
        }
        public static Decimal ChangeToDecimal(string strData)    //将科学计数法的返回字符转换成浮点
        {
            Decimal dData = 0.0M;
            try
            {
                if (strData.Contains("E"))
                {
                    dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
                }
                else
                {
                    dData = Convert.ToDecimal(strData);
                }
            }
            catch (Exception e)
            {

            }
            return Math.Round(dData, 4);
        }
        public static void EveryDayToExcel(string Today)
        {
            try
            {
                string sql = "select  *from brake_routine_test_detail  where test_time>='" + Today.Trim() + " 00:00:00" + "' and 测试日期<='" + Today.Trim() + " 23:59:59" + "'";
                SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData.mysql);
                DataSet Ds = new DataSet();
                Da.Fill(Ds, "brake_routine_test_detail");
                if (Ds == null) return;
                Excel.Application excel = new Excel.Application();
                if (excel == null) return;
                Excel.Workbooks workbooks = excel.Workbooks;
                excel.Visible = false;
                Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                Excel.Range range = null;
                for (int i = 0; i < Ds.Tables["brake_routine_test_detail"].Columns.Count; i++)
                {
                    range = (Excel.Range)worksheet.Cells[1, i + 1];
                    worksheet.Cells[1, i + 1] = Ds.Tables["brake_routine_test_detail"].Columns[i].ColumnName;
                    range.EntireColumn.AutoFit();//自动调整列宽
                    range.EntireRow.AutoFit();//自动调整行高
                }
                for (int i = 0; i < Ds.Tables["brake_routine_test_detail"].Rows.Count; i++)
                {
                    for (int j = 0; j < Ds.Tables["brake_routine_test_detail"].Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = Ds.Tables["brake_routine_test_detail"].Rows[i][j];
                    }
                    System.Windows.Forms.Application.DoEvents();
                    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。
                }
                workbook.Saved = true;
                workbook.SaveCopyAs(GlobalData.ExcelFilePath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + "-TestResult.xlsx");
                workbook.Close(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                //MessageBox.Show("数据导出成功o(￣▽￣)d");
            }
            catch (Exception E)
            {
                //Logger.Instance.WriteException(E);
            }
        }
        public static void ReStartAPP()
        {
            GlobalData.AppRestart = true;
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
