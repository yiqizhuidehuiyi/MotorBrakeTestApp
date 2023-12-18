using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services.Meter
{
    /// <summary>
    /// 绝缘电阻表
    /// </summary>
    internal class TH2683B
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(TH2683B));
        public static SerialPort SerialPort { get; private set; }

        private static string ExecuteCommand(string sendCommand)
        {
            SerialPort.DiscardInBuffer();
            log.Debug($"TH2683B 发送：{sendCommand}");
            SerialPort.WriteLine(sendCommand);
            var back = SerialPort.ReadLine();
            log.Debug($"TH2683B 接收：{back}");
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
                SerialPort.PortName = GlobalData.Power_digital;
                SerialPort.ReadTimeout = 5000;
                SerialPort.Open();
                InitializeTestDataAsync();
                log.Debug("TH2683B Initialization Finish");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public async static void InitializeTestDataAsync()
        {
            SINGle();
            await Task.Delay(200);
            OVOL();
            await Task.Delay(200);
            MTIMe();
            await Task.Delay(200);
            TRGVoid();
        }

        public static string IDN()
        {
            var back = ExecuteCommand("*IDN?");
            return back;
        }

        public static string TRG()
        {
            var back = ExecuteCommand("*TRG");
            return back;
        }

        public static void TRGVoid()
        {
            ExecuteVoid("*TRG");
        }

        public static void OVOL()
        {
            ExecuteVoid("FUNCtion:OVOL 500");
        }

        public static void DISCharge()
        {
            ExecuteVoid("DISCharge");
        }

        public static void MTIMe()
        {
            ExecuteVoid("FUNCtion:MTIMe 2");
        }

        public static void SINGle()
        {
            ExecuteVoid("FUNCtion:MMOD SINGle");
        }

        /// <summary>
        /// 转换成数据
        /// </summary>
        /// <param name="resultText"></param>
        /// <returns></returns>
        public static double[] ConvertToDoubles(string resultText)
        {
            //  +1.131E+13,  +4.428E-11,+1
            double[] results = new double[3];
            string[] txts = resultText.Split(',');
            for (int i = 0; i < 3; i++)
            {
                txts[i] = txts[i].Trim();
                results[i] = Convert.ToDouble(txts[i]);
            }
            results[0] = results[0] / 1000000;
            return results;
        }
    }
}
