using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services.Meter
{
    internal class WT310E
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(WT310E));
        public static SerialPort SerialPort { get; private set; }

        private static string ExecuteCommand(string sendCommand)
        {
            log.Debug($"WT310E 发送：{sendCommand}");
            SerialPort.WriteLine(sendCommand);
            var back = SerialPort.ReadLine();
            log.Debug($"WT310E 接收：{back}");
            return back;
        }

        public static void Initialization()
        {
            try
            {
                if (SerialPort == null) SerialPort = new SerialPort();
                if (SerialPort.IsOpen) SerialPort.Close();
                SerialPort.PortName = GlobalData.Poewr_Tester;
                SerialPort.ReadTimeout = 1000;
                SerialPort.Open();
                log.Debug("WT310E Initialization Finish");
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

        public static string NUMericNORMal()
        {
            var back = ExecuteCommand(":NUMeric:NORMal?");
            return back;
        }

        //37.728E+00,0.1460E-03,-0.77E-03,5.51E-03,-5.46E-03,-0.1402E+00,-98.1E+00,49.815E+00,156.13E+00,NAN
        public static string NUMericVALue()
        {
            var back = ExecuteCommand(":NUMeric:VALue?");
            return back;
        }

        /// <summary>
        /// ITEM1 U,1;ITEM2 I,1;ITEM3 P,1;ITEM4 S,1;ITEM5 Q,1;ITEM6 LAMB,1;ITEM7 PHI,1;ITEM8 FU,1; ITEM9 FI,1; ITEM10 NONE
        /// </summary>
        /// <param name="nUMericVALue"></param>
        /// <returns></returns>
        public static double[] ConvertToDoubles(string nUMericVALue)
        {
            var resultTxts = nUMericVALue.Split(',');
            double[] results = new double[resultTxts.Length];
            for (int i = 0; i < resultTxts.Length; i++)
            {
                if (double.TryParse(resultTxts[i], out double back))
                {
                    results[i] = back;
                }
            }
            results[2] = Math.Abs(results[2]);
            return results;
        }

    }
}
