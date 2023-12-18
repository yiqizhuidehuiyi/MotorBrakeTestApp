using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using ICUValidationService.Log;
using Excel = Microsoft.Office.Interop.Excel;
namespace SEW苏州电机制动器测试台
{
    class Class1GlobalData
    {
        public struct Product
        {
            public string Model;
            public string TestTime;
            public string TestData;
            public string ProductID;
            public string R1;
            public string R1_Correction;
            public string R2;
            public string R2_Correction;
            public string Temper;
            public string Rm;
            public string Rm_Voltage;
            public string LeakCurrent;
            public string WSVoltage;          //耐压
            public string RD_Voltage;        //额定电压
            public string RD_Current;        // 额定电流
            public string RD_Current_Correction;
            public string RD_Power;         //额定功率
            public string RD_Power_Correction;
            public string Runspeed;         //跑合转速
            public string RunTorque;       //跑合转矩
            public string Breakspeed;       //制动转速
            public string BreakTorque;     //制动转矩
            public string RemainVoltage;   //残留转矩电压
            public string Remainspeed;      //残留转矩
            public string RemainTorque;     //残留转矩
            public string StaticToque;     //静态转矩
            public string Voltage60p;       //60%电压
            public string Current60p;       //60%电流
            public string Current60p_Correction;
            public string Power60p;         //60%功率
            public string Power60p_Correction;
            public bool Comparison_R;      //电阻对比结果
            public bool Comparison_Rm;      //绝缘电阻电阻对比结果
            public bool Comparison_RDPower;  //额定功率对比结果
            public bool Comparison_RDCurrent;//额定电流对比结果
            public bool Comparison_RunTorque;//跑合转矩对比结果
            public bool Comparison_BreakTorque;//制动转矩对比结果
            public bool Comparison_RemainTorque;//残留转矩对比结果
            public bool Comparison_Power60p;//60%功率对比结果
            public bool Comparison_Current60p;//60%电流对比结果
            public bool Comparison_LeakCurrent;//耐压对比结果
            public bool Comparison_Total;//最后测试结果
            public string TestTimes;     //测试次数;
            public string UseTime;          //  测试使用总时间
            public string sequence;        //序号(暂时不知道如何人工干预)
        }

        public struct Model
        {
            public float R1_L;                //bs 电阻min
            public float R1_H;                //bs 电阻max
            public float R2_L;                //ts 电阻min
            public float R2_H;                //ts 电阻max
            public float R_TestTime;          //电阻测试时间
            public float StandardTemper;      //电阻测试基准温度
            public float Tempercoefficient;   //电阻温度系数
            public float Rated_V;             //额定电压
            public float Rated_IL;            //满电压电流min
            public float Rated_IH;            //满电压电流max
            public float Rated_PL;            //满电压功率min
            public float Rated_PH;            //满电压功率max
            public float Rated_PowerTime;      //满电压测试时间
            public float Run_Ver;             //跑合速度
            public float Run_torL;            //跑合转矩min
            public float Run_torH;           //跑合转矩max
            public float Run_TimeL;           //跑合时间min
            public float Run_TimeH;          //跑合时间max
            public float Brake_Ver;          //制动转速
            public float Brake_torL;         //制动转矩min
            public float Brake_torH;         //制动转矩max
            public float Remain_torL;        //残留转矩min
            public float Remain_torH;        //残留转矩max
            public float Rated_60pV;        //60%电压
            public float Rated_60pIL;       //60%电压电流min
            public float Rated_60pIH;       //60%电压电流max
            public float Rated_60pPL;       //60%电压功率min
            public float Rated_60pPH;       //60%电压功率max
            public float Rated_60pTimer;    //60电压测试时间
            public float IR_L;              //绝缘电阻min
            public float IR_H;             //绝缘电阻max
            public int IR_TestTime;        //绝缘电阻测试时间
            public int IR_TestVoltage;    //绝缘电阻测试电压
            public int WithStand_V;       //耐压电压
            public float Leak_AL;           //泄露电流min
            public float Leak_AH;            //泄露电流max
            public int WithStandV_Time;     //耐压测试时间
            public float Spare_temperL;      //备用温度min
            public float Spare_temperH;     //备用温度max
            public float Spare_temper;      //备用温度
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["PLC IP"].Value = PLC_IP;
            config.AppSettings.Settings["数据库地址"].Value = sql_add;
            config.AppSettings.Settings["数据库账号"].Value = sql_ID;
            config.AppSettings.Settings["数据库密码"].Value = sql_pw;
            config.AppSettings.Settings["直流电阻COM"].Value = R_DC;
            config.AppSettings.Settings["直流电阻IP"].Value = R_DC_IP;
            config.AppSettings.Settings["直流电阻端口"].Value = R_DC_Port;
            config.AppSettings.Settings["电能测试仪COM"].Value = Poewr_Tester;
            config.AppSettings.Settings["电能测试仪IP"].Value = Poewr_Tester_IP;
            config.AppSettings.Settings["电能测试仪端口"].Value = Poewr_Tester_Port;
            config.AppSettings.Settings["安规测试仪COM"].Value = Angui_Tester;
            config.AppSettings.Settings["安规测试仪IP"].Value = Angui_Tester_IP;
            config.AppSettings.Settings["安规测试仪端口"].Value = Angui_Tester_Port;
            config.AppSettings.Settings["数字表COM"].Value = Power_digital;
            config.AppSettings.Settings["数字表IP"].Value = Power_digital_IP;
            config.AppSettings.Settings["数字表端口"].Value = Power_digital_Port;
            config.AppSettings.Settings["Excel保存路径"].Value = Class1GlobalData.ExcelFilePath;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #region 声明公共变量
        public static int Log_Level=-1;
        public static bool Log_State;
        public static string Log_Name;
        public static string PLC_IP = ConfigurationManager.AppSettings["PLC IP"];
        public static string sql_add = ConfigurationManager.AppSettings["数据库地址"];
        public static string sql_ID = ConfigurationManager.AppSettings["数据库账号"];
        public static string sql_pw = ConfigurationManager.AppSettings["数据库密码"];
        public static string R_DC = ConfigurationManager.AppSettings["直流电阻COM"];
        public static string R_DC_IP = ConfigurationManager.AppSettings["直流电阻IP"];
        public static string R_DC_Port = ConfigurationManager.AppSettings["直流电阻端口"];
        public static string Poewr_Tester = ConfigurationManager.AppSettings["电能测试仪COM"];
        public static string Poewr_Tester_IP = ConfigurationManager.AppSettings["电能测试仪IP"];
        public static string Poewr_Tester_Port= ConfigurationManager.AppSettings["电能测试仪端口"];
        public static string Angui_Tester= ConfigurationManager.AppSettings["安规测试仪COM"];
        public static string Angui_Tester_IP = ConfigurationManager.AppSettings["安规测试仪IP"];
        public static string Angui_Tester_Port = ConfigurationManager.AppSettings["安规测试仪端口"];
        public static string Power_digital = ConfigurationManager.AppSettings["数字表COM"];
        public static string Power_digital_IP = ConfigurationManager.AppSettings["数字表IP"];
        public static string Power_digital_Port = ConfigurationManager.AppSettings["数字表端口"];
        public static string ExcelFilePath = ConfigurationManager.AppSettings["Excel保存路径"];                //Excel文件保存路径
        public static string RemoteSetAinuo =ConfigurationManager.AppSettings["远程设置安规"]; //远程设置安规测试仪;
        public static string sqlconstr= "Server=" + Class1GlobalData.sql_add + ";user=" + Class1GlobalData.sql_ID + ";pwd=" + Class1GlobalData.sql_pw + ";database=SEW Product AutoTest Station;";
        public static SqlConnection mysql = new SqlConnection(sqlconstr);
        public static string Log_UserID;
        public static bool PLC_Connect_State=false ;
        public static bool WT310E_Connect_State = false;
        public static bool TH2516_Connect_State = false;
        public static bool Ainuo_Connect_State = false;
        public static bool Digital_Power_Connect_State = false;
        public static bool DCR_needTest = false;
        public static bool IR_needTest = false;
        public static bool RetedV_needTest = false;
        public static bool Reted60pV_needTest = false;
        public static bool Run_needTest = false;
        public static bool Runtor_needTest = false;
        public static bool Remain_needTest = false;
        public static bool WithstandV_needTest = false;
        public static string[] PLCIO = new string[200] ;
        public static bool[] TestState = new bool[8]{false, false, false, false, false, false, false, false };  //用于判断测试的步骤   TestState[0]:R1 ;TestState[1]:R2;TestState[2]:耐压、绝缘;TestState[3]:额定功率测试;TestState[4]:跑合测试;TestState[5]:制动测试;TestState[6]:残留测试;TestState[7]:60%电压测试;
        public static bool[] TestComparison = new bool[9]{true,true,true,true,true,true,true,true,true};//用于存储测试对比结果 TestComparison[0]:R1对比结果;
        public static bool TestStart = false;  //测试开始
        public static bool TestOver = false;   //测试结束
        public static int TotalMakeQty= Int16 .Parse ( ConfigurationManager.AppSettings["当日产量"].Trim ());        //当日总产量
        public static int TotalBadQty= Int16.Parse(ConfigurationManager.AppSettings["当日不良品数量"].Trim());          //当日不良品数量
        public static int TotalGoodQty= Int16.Parse(ConfigurationManager.AppSettings["当日良品数量"].Trim());        //当日良品数量
        public static int GetAinuoSendTimes = 0;
        public static int TH2516TestTimes = 0;                                 //用于测试的电阻是R1 还是R2
        public static float [] EveryHBadQty = new float [24];//每小时的不良品数量
        public static bool SystemitialFlag;//初始化标志位
        public static int[] ReadNetCFailTimes = new int[4] { 0,0,0,0};                            //四个变量用于记录读取失败错误次数，用于检测连接状态 1.PLC 2.安规测试仪 3.电阻测试仪   4.电压表             
        public static bool AppRestart; //软件重启
        #endregion
        public static void ReadPLCIO()
        {
            string[]  keyName= ConfigurationManager.AppSettings.AllKeys;
            for(int i=0;i<keyName.Length;i++)
            {
                PLCIO[i] = ConfigurationManager.AppSettings[keyName[i]];
            }
            for (int j = 0; j < 24; j++)
            {
                EveryHBadQty[j] = float.Parse(PLCIO[102 + j].Trim());
            }
        }
        public static  Decimal ChangeToDecimal(string strData)    //将科学计数法的返回字符转换成浮点
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
            return Math.Round(dData, 2);
        }
        public static void EveryDayToExcel(string Today)
        {
            try
            {
                string sql = "select  *from brake_routine_test_detail  where test_time>='" + Today .Trim()+" 00:00:00" + "' and 测试日期<='" + Today .Trim() +" 23:59:59"+ "'";
                SqlDataAdapter Da = new SqlDataAdapter(sql, Class1GlobalData.mysql);
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
                        worksheet .Cells[1, i + 1] = Ds.Tables["brake_routine_test_detail"].Columns[i].ColumnName;
                        range.EntireColumn.AutoFit();//自动调整列宽
                        range.EntireRow.AutoFit();//自动调整行高
                }
                for(int i=0;i<Ds.Tables ["brake_routine_test_detail"].Rows.Count;i++)
                {
                    for(int j=0;j<Ds.Tables ["brake_routine_test_detail"].Columns .Count;j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = Ds.Tables["brake_routine_test_detail"].Rows[i][j];
                    }
                    System.Windows.Forms.Application.DoEvents();
                    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。
                }
                workbook.Saved = true;
                workbook.SaveCopyAs(Class1GlobalData .ExcelFilePath+@"\"+ DateTime.Now.ToString("yyyy-MM-dd")+"-TestResult.xlsx");
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
                Logger.Instance.WriteException(E);
            }
        }
        public static void ReStartAPP()
        {
            Class1GlobalData.AppRestart = true;
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
