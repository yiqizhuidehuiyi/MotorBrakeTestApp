using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services.Meter
{
    internal class BS601102C
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(BS601102C));
        public static SerialPort SerialPort { get; private set; }

        public static void Initialization()
        {
            try
            {
                if (SerialPort == null) SerialPort = new SerialPort();
                if (SerialPort.IsOpen) SerialPort.Close();
                SerialPort.PortName = GlobalData.SinglePowerSource;
                SerialPort.ReadTimeout = 1000;
                SerialPort.Open();
                log.Debug("BS601102C Initialization Finish");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public static void Start()
        {
            SerialPort.Write(new byte[] { 0x01, 0x11, 0x00, 0xff, 0x11 }, 0, 5);
            log.Debug($"单相电源 发送：启动");
        }

        public static void Stop()
        {
            SerialPort.Write(new byte[] { 0x01, 0x11, 0x00, 0x00, 0x12 }, 0, 5);
            log.Debug($"单相电源 发送：停止");
        }

        /// <summary>
        /// 设置电压：01 01 B8 0B C5=300V
        /// </summary>
        /// <param name="fengJiModel"></param>
        /// <returns></returns>
        public static void SetVoltage(double voltage)
        {
            int setVoltage = (int)(voltage * 10);
            byte[] commands = new byte[5];
            commands[0] = 0x01;
            commands[1] = 0x01;
            commands[2] = (byte)(setVoltage % 256);
            commands[3] = (byte)((setVoltage - setVoltage % 256) / 256);
            commands[4] = GerCheck(commands[2], commands[3], commands[1]);
            SerialPort.Write(commands, 0, commands.Length);
            log.Debug($"单相电源 发送：电压值{voltage}V");
        }

        public static void SetFrequency(double frequency)
        {
            int setFrequency = (int)(frequency * 10);
            byte[] commands = new byte[5];
            commands[0] = 0x01;
            commands[1] = 0x02;
            commands[2] = (byte)(setFrequency % 256);
            commands[3] = (byte)((setFrequency - setFrequency % 256) / 256);
            commands[4] = GerCheck(commands[2], commands[3], commands[1]);
            SerialPort.Write(commands, 0, commands.Length);
            log.Debug($"单相电源 发送：频率值{frequency}Hz");
        }

        public static byte GerCheck(byte vs, byte vs1, byte vs2)
        {
            return (byte)((0x01 + vs2 + vs + vs1) & (0xff));

        }
    }
}
