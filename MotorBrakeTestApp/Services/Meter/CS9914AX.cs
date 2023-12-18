using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace MotorBrakeTestApp.Services.Meter
{
    internal class CS9914AX
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(CS9914AX));
        public static SerialPort SerialPort { get; set; }

        private static string ExecuteCommand(string sendCommand)
        {
            log.Debug($"CS9914AX 发送：{sendCommand}");
            SerialPort.Write(sendCommand);
            var back = SerialPort.ReadTo("#");
            log.Debug($"CS9914AX 接收：{back}");
            return back;
        }

        public static void Initialization()
        {
            try
            {
                if (SerialPort == null) SerialPort = new SerialPort();
                if (SerialPort.IsOpen) SerialPort.Close();
                SerialPort.PortName = GlobalData.WithstandPortName;
                SerialPort.ReadTimeout = 5000;
                SerialPort.Open();
                SAddress(1);
                Connect();
                log.Debug("CS9914AX Initialization Finish");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public static void SAddress(int address)
        {
            ExecuteCommand($"COMMunication:SADDress {address}#");
        }

        public static void Connect()
        {
            ExecuteCommand("COMMunication:REMote#");
        }

        /// <summary>
        /// 设置电压 单位 V
        /// </summary>
        /// <param name="voltage"></param>
        public static void SetVoltage(double voltage)
        {
            string voltageText = (voltage / 1000).ToString("0.000");
            ExecuteCommand($"STEP:ACW:VOLTage {voltageText}#");
        }

        /// <summary>
        /// 泄露上限 单位mA
        /// </summary>
        /// <param name="c"></param>
        public static void SetCurrent(int current)
        {
            if (current == 0) current = 100;
            if (current > 20) current = 20;
            current *= 100;
            string cuttentText = current.ToString("0000");
            ExecuteCommand($"STEP:ACW:HIGH {cuttentText}#");
        }

        /// <summary>
        /// 耐压时长 单位s
        /// </summary>
        /// <param name="time"></param>
        public static void SetTime(double time)
        {
            if (time < 1) time = 1;
            string timeText = time.ToString("000.0");
            ExecuteCommand($"STEP:ACW:TTIMe {timeText}#");
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Start()
        {
            ExecuteCommand("SOURce:TEST:STARt#");
        }

        public static void Stop()
        {
            ExecuteCommand("SOURce:TEST:STOP#");
        }

        /// <summary>
        /// SOURce:TEST:FETCh?#  "01,0,0.000,2,05.01,0,-----,000.2,07#"  不合格
        /// </summary>
        /// <returns></returns>
        public static double[] GetResult()
        {
            double[] results = new double[3];
            var backText = ExecuteCommand("SOURce:TEST:FETCh?#");
            string[] data = backText.Split(',');
            if (data.Length > 3) results[0] = Convert.ToDouble(data[2]);
            if (data.Length > 5) results[1] = Convert.ToDouble(data[4]);
            if (data.Length > 8) results[2] = Convert.ToDouble(data[7]);
            if (data.Length > 8)
            {
                int state = Convert.ToInt32(data[8].Replace("#", ""));
                if (state == 9)
                {
                    results[1] = 200;
                }
            }
            return results;
        }

        public static void SetRange(string rang)
        {
            ExecuteCommand($"STEP:ACW:RANG {rang}#");
        }
    }
}
