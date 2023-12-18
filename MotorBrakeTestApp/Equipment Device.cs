using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HslCommunication.Profinet.Siemens;
using HslCommunication.Profinet;
using HslCommunication;
using HslCommunication.ModBus;
using System.Net.Sockets;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
namespace MotorBrakeTestApp
{
    class Equipment_Device
    {
        public static  SiemensS7Net siemensS7Net = new SiemensS7Net(SiemensPLCS.S1200);
        public static ModbusTcpNet busTcpClint = new ModbusTcpNet();
        #region 电压表读取
        //public static bool ModbusTCPInitialize()
        //{
        //    busTcpClint?.ConnectClose();
        //    busTcpClint = new ModbusTcpNet(Class1GlobalData.Power_digital_IP, Convert.ToInt16( Class1GlobalData.Power_digital_Port), 1);
        //    try
        //    {
        //        OperateResult connect = busTcpClint.ConnectServer();
        //        if(connect .IsSuccess )
        //        {
        //            Class1GlobalData.Digital_Power_Connect_State = true;
        //        }
        //        else
        //        {
        //            Class1GlobalData.Digital_Power_Connect_State = false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //       MessageBox.Show(e.Message);
        //    }
        //    return Class1GlobalData.Digital_Power_Connect_State;
        //}
        //public static int  ReadDigitalPower()
        //{
        //    int Voltage=0;
        //    Voltage = ReadResultRender(busTcpClint.ReadInt32("16"), "16", Voltage);
        //    return Voltage;
        //}
        //private static  int  ReadResultRender<T>(OperateResult<T> result, String address, int Result)
        //{
        //    if (result.IsSuccess)
        //    {
        //        Result = Convert.ToInt32 ( result.Content);
        //    }
        //    else
        //    {
        //        //MessageBox.Show($"读取{address }错误！！");
        //        Result = -100;
        //    }
        //    return Result;
        //}
        #endregion


        //PLC 不能同时读写  或者同时 写多个 读 多个数据
        ///s7_1200PLCDriver.ReadPLCShort("DB3002.0");
        ///s7_1200PLCDriver.ReadPLCShort("DB3001.486.5");
        ///s7_1200PLCDriver.WritePLCShort("DB3002.0",12);
        ///s7_1200PLCDriver.WritePLCShort("DB3001.486.5",1);
        ///SHORT  对应 PLC  INT 16位
        ///SHORT  对应 PLC  WORD 16位
        ///INT    对应 PLC  DINT 32位
        ///INT    对应 PLC  DWORD 32位
        ///DOUBLE 对应 PLC   LongReal  
        ///Float  对应 PLC  Real;
        #region PLC 操作
        public static bool PLCInitialize()
        {
            //siemensS7Net = null;
            siemensS7Net.IpAddress = GlobalData.PLC_IP;
            OperateResult connect = siemensS7Net.ConnectServer();
            if(connect .IsSuccess )
            {
                GlobalData.PLC_Connect_State = true;
            }
            else
            {
                //MessageBox.Show("PLC连接失败，请检查PLC IP地址:"+Class1GlobalData.PLC_IP+"是否正确");
                GlobalData.PLC_Connect_State = false;
            }
            return GlobalData.PLC_Connect_State;
        }
        #region PLC读取
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PLCAdd"></param>
        /// <returns></returns>
        public static bool ReadPLCbool(string PLCAdd)
        {
            string Result = "";
            try
            {
                Result = ReadResultRender(siemensS7Net.ReadBool(PLCAdd), PLCAdd, Result);
                return bool.Parse(Result);
            }
            catch (Exception E)
            {
                Result = "False";
                return false ;  
            }
        }
        /// <summary>
        /// 读取PLC Real型
        /// </summary>
        /// <param name="PLCAdd"></param>
        /// <returns></returns>
        public static string  ReadPLCfloat(string PLCAdd)
        {
            string Result = "";
            Result=ReadResultRender(siemensS7Net.ReadFloat(PLCAdd), PLCAdd, Result);
            return Result;
        }
        /// <summary>
        ///  读取时间为秒S   除以100000000    plc单位是  ms
        /// </summary>
        /// <param name="PLCAdd"></param>
        /// <returns></returns>
        public static string ReadPLCLong(string PLCAdd)
        {
            string Result = "";
            Result = ReadResultRender(siemensS7Net.ReadInt64(PLCAdd), PLCAdd, Result);
            return Result;
        }
        /// <summary>
        /// 读取PLC  DINT  DWORD
        /// </summary>
        /// <param name="PLCAdd"></param>
        /// <returns></returns>
        public static string ReadPLCInt(string PLCAdd)
        {
            string Result = "";
            Result = ReadResultRender(siemensS7Net.ReadInt32(PLCAdd), PLCAdd, Result);
            return Result;
        }
        /// <summary>
        /// 读取PLC INT  WORD
        /// </summary>
        /// <param name="PLCAdd"></param>
        /// <returns></returns>
        public static string ReadPLCShort(string PLCAdd)
        {
            string Result = "";
            Result = ReadResultRender(siemensS7Net.ReadInt16(PLCAdd), PLCAdd, Result);
            return Result;
        }
        /// <summary>
        /// 读取 PLC  STRING
        /// </summary>
        /// <param name="PLCAdd"></param>
        /// <returns></returns>
        public static string ReadPLCString(string PLCAdd)
        {
            string Result = "";
            Result= ReadResultRender(siemensS7Net.ReadString(PLCAdd,10), PLCAdd, Result);
            return Result;
        }
        public static string ReadResultRender<T>(OperateResult<T> result, string address, string GetResult)
        {
            if (result.IsSuccess)
            {
                GetResult += result.Content;
                GlobalData.ReadNetCFailTimes[0] =0 ;
            }
            else
            {
                GlobalData.ReadNetCFailTimes[0] += 1;
                //MessageBox.Show("读取错误");
            }
            return GetResult;
        }
        #endregion
        #region PLC写
        public static void WritePLCBool(string PLCAdd,bool WriteValue )
        {
            WriteResultRender(siemensS7Net.Write(PLCAdd,WriteValue),PLCAdd);
        }
        public static void WritePLCShort(string PLCAdd, short WriteValue)
        {
            WriteResultRender(siemensS7Net.Write(PLCAdd, WriteValue), PLCAdd);
        }
        public static void WritePLCInt(string PLCAdd, Int32 WriteValue)
        {
            WriteResultRender(siemensS7Net.Write(PLCAdd, WriteValue), PLCAdd);
        }
        public static void WritePLCFloat(string PLCAdd,float wrtieValue)
        {
            WriteResultRender(siemensS7Net.Write(PLCAdd, wrtieValue), PLCAdd);
        }
        public static void WritePLCString(string PLCAdd, string writeValue)
        {
            WriteResultRender(siemensS7Net .Write(PLCAdd, writeValue), PLCAdd);
        }
        public static void WriteResultRender(OperateResult result, string address)
        {
            if (result.IsSuccess)
            {
                //MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入成功");
            }
            else
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }
        #endregion
        #endregion
    }
}
