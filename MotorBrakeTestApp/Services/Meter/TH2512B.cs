using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services.Meter
{
    internal class TH2512B
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(TH2512B));
        public static SerialPort SerialPort { get; private set; }

        private static string ExecuteCommand(string sendCommand)
        {
            log.Debug($"TH2512B 发送：{sendCommand}");
            SerialPort.WriteLine(sendCommand);
            var back = SerialPort.ReadLine();
            log.Debug($"TH2512B 接收：{back}");
            return back;
        }

        private static void ExecuteVoid(string sendCommand)
        {
            SerialPort.DiscardInBuffer();
            log.Debug($"TH2683B 发送无返回：{sendCommand}");
            SerialPort.WriteLine(sendCommand);
        }

        public static void Initialization()
        {
            try
            {
                if (SerialPort == null) SerialPort = new SerialPort();
                if (SerialPort.IsOpen) SerialPort.Close();
                SerialPort.PortName = GlobalData.R_DC;
                SerialPort.ReadTimeout = 1000;
                SerialPort.Open();
                log.Debug("TH2512B Initialization Finish");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public static string IDN()
        {
            var back = ExecuteCommand("*IDN?");
            return back;
        }

        public static void G()
        {
            ExecuteVoid("G");
        }

        public static string Ask()
        {
            var back = ExecuteCommand("?");
            return back;
        }

        /// <summary>
        /// 转换成数据
        /// </summary>
        /// <param name="resultText"></param>
        /// <returns></returns>
        public static double ConvertToDouble(string resultText)
        {
            double range = 1;
            string[] txts = resultText.Split('=');
            double r = 0;
            if (txts.Length > 1)
            {
                switch (txts[0].Trim())
                {
                    case "R1":
                    case "R2":
                        range = 0.001;
                        break;
                    case "R3":
                    case "R4":
                    case "R5":
                        range = 1;
                        break;
                    case "R6":
                    case "R7":
                    case "R8":
                        range = 1000;
                        break;
                }
                string[] sTxts = txts[1].Split(' ');
                r = Convert.ToDouble(sTxts[0]);
                return r * range;
            }
            return 99999;
        }

    }
}
