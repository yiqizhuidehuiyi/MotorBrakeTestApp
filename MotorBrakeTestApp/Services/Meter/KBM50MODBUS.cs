using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services.Meter
{
    internal class KBM50MODBUS
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(KBM50MODBUS));
        public static SerialPort SerialPort { get; private set; }
        private struct TempInfo
        {
            public double Temp { get; set; }
            public DateTime DateTime { get; set; }
        }
        private static List<TempInfo> tempList;

        private static double GetRealTemp(double temp)
        {
            TempInfo tempInfo = new TempInfo();
            tempInfo.Temp = temp;
            tempInfo.DateTime = DateTime.Now;
            if (temp > 120)
            {
                temp = 20;
            }
            tempList.Add(tempInfo);

            if (tempList.Count > 5)
            {
                tempList.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));
                tempList.RemoveAt(0);
            }

            tempList.Sort((x, y) => x.Temp.CompareTo(y.Temp));
            if (tempList.Count < 5)
            {
                return tempList[tempList.Count - 1].Temp;
            }
            else
            {
                return tempList[tempList.Count - 3].Temp;
            }
        }

        public static void Initialization()
        {
            try
            {
                if (SerialPort == null) SerialPort = new SerialPort();
                if (SerialPort.IsOpen) SerialPort.Close();
                SerialPort.PortName = GlobalData.TempModulePortName;
                SerialPort.ReadTimeout = 1000;
                SerialPort.Open();
                tempList = new List<TempInfo>();
                log.Debug($"KBM50MODBUS Initialization Finish 串口号：{SerialPort.PortName}");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public static double GetTemperature()
        {
            int index = 0;
            double dBack;
            byte[] readData;
            do
            {
                try
                {
                    var command = new byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x01, 0x84, 0x0A };
                    SerialPort.Write(command, 0, command.Length);
                    System.Threading.Thread.Sleep(100);
                    readData = new byte[SerialPort.BytesToRead];
                    var dl = SerialPort.Read(readData, 0, SerialPort.BytesToRead);
                    log.Debug($"温度接收字节数{dl}");
                } 
                catch (Exception)
                {
                    return 20;
                }
                try
                {
                    byte[] rBack = new byte[2];
                    Array.Copy(readData, 3, rBack, 0, 2);
                    dBack = ((double)rBack[0] * 256 + (double)rBack[1]) / 10 - 50;
                    log.Debug($"温度值{dBack} 反馈次数{index}");
                    index++;
                    if (index > 1 && index < 3)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {

                    return 0;
                }

            }
            while (index < 3 && dBack > 120);

            return GetRealTemp(dBack);
        }
    }
}
