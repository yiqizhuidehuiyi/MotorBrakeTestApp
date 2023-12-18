using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MotorBrakeTestApp.Services
{
    internal class PLC
    {
        private static ModbusTcpNet modbusTcpClint = null;
        private static ModbusTcpNet monitorTcpClint = null;
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(PLC));
        private static int delayAction = 50;

        public static bool EmergencyStopButton { get; private set; }
        public static bool StartButton { get; private set; }
        public static bool StopButton { get; private set; }
        public static bool DoorSafety { get; private set; }
        public static bool PowerState { get; private set; }
        public static int MainCylinderButtonState { get; private set; }
        public static int MainCylinderLimitState { get; private set; }
        public static double DisplacementSensor1 { get; private set; }
        public static double DisplacementSensor2 { get; private set; }
        public static double TorqueValue { get; private set; }
        public static double SpeedValue { get; private set; }

        public static void StartPLC()
        {
            if (modbusTcpClint == null)
            {
                var ip = GlobalData.PLC_IP;
                modbusTcpClint = new ModbusTcpNet(ip, 502, 1);
                modbusTcpClint.AddressStartWithZero = true;

                try
                {
                    OperateResult connect = modbusTcpClint.ConnectServer();
                    log.Info($"PLC控制连接是否成功 {connect.IsSuccess}");
                }
                catch (Exception ex)
                {
                    log.Error($"PLC控制连接异常 {ex}");
                }
            }
            if (monitorTcpClint == null)
            {
                var ip = GlobalData.PLC_IP;
                monitorTcpClint = new ModbusTcpNet(ip, 503, 1);
                monitorTcpClint.AddressStartWithZero = true;

                try
                {
                    OperateResult connect = monitorTcpClint.ConnectServer();
                    log.Info($"PLC监控连接是否成功 {connect.IsSuccess}");
                }
                catch (Exception ex)
                {
                    log.Error($"PLC监控连接异常 {ex}");
                }

                try
                {
                    Monitor();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        public async static void Monitor()
        {
            while (true)
            {
                await Task.Delay(10);
                OperateResult<ushort[]> read = monitorTcpClint.ReadUInt16("0", 15);
                var result = read.Content;
                if (result == null) continue;
                if (result.Length > 0) EmergencyStopButton = result[0] > 0;
                if (result.Length > 1) StartButton = result[1] > 0;
                if (result.Length > 2) StopButton = result[2] > 0;
                if (result.Length > 3) DoorSafety = result[3] > 0;
                if (result.Length > 4) PowerState = result[4] > 0;
                if (result.Length > 5) MainCylinderButtonState = result[5];
                if (result.Length > 6) MainCylinderLimitState = result[6];
                if (result.Length > 7) DisplacementSensor1 = (double)result[7] / 10000;
                if (result.Length > 8) DisplacementSensor2 = (double)result[8] / 10000;
                if (result.Length > 9) TorqueValue = (double)result[9] / 100;
                if (result.Length > 10) SpeedValue = (double)result[10] / 10;
            }
        }

        public static void MustStop()
        {
            log.Debug($"PLC {nameof(MustStop)} 执行");
            modbusTcpClint.WriteOneRegister("0", 1);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void DcResi(int index)
        {
            log.Debug($"PLC {nameof(DcResi)} = {index}");
            modbusTcpClint.WriteOneRegister("1", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void DcResiProtection(int index)
        {
            log.Debug($"PLC {nameof(DcResiProtection)} = {index}");
            modbusTcpClint.WriteOneRegister("5", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void InusResi(int index)
        {
            log.Debug($"PLC {nameof(InusResi)} = {index}");
            modbusTcpClint.WriteOneRegister("2", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void Interturn(int index)
        {
            log.Debug($"PLC {nameof(Interturn)} = {index}");
            modbusTcpClint.WriteOneRegister("3", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void WithstandVoltage(int index)
        {
            log.Debug($"PLC {nameof(WithstandVoltage)} = {index}");
            modbusTcpClint.WriteOneRegister("4", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        /// <summary>
        /// 整流块
        /// </summary>
        /// <param name="index">0：断开 1：BME1.5 2：BMS1.5 3:BME3 4:BMS3 5:BMV5 6:BS24</param>
        public static void Adapter(int index)
        {
            log.Debug($"PLC {nameof(Adapter)} = {index}");
            modbusTcpClint.WriteOneRegister("6", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void VoltageSource(int index)
        {
            log.Debug($"PLC {nameof(VoltageSource)} = {index}");
            modbusTcpClint.WriteOneRegister("7", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void RorqueRange(int index)
        {
            log.Debug($"PLC {nameof(RorqueRange)} = {index}");
            modbusTcpClint.WriteOneRegister("8", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        /// <summary>
        /// 主压缸
        /// </summary>
        /// <param name="index">1上升 2下降</param>
        public static void MainCylinder(int index)
        {
            log.Debug($"PLC {nameof(MainCylinder)} = {index}");
            modbusTcpClint.WriteOneRegister("9", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void MotorRun(int index)
        {
            log.Debug($"PLC {nameof(MotorRun)} = {index}");
            modbusTcpClint.WriteOneRegister("10", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void MotorSpeedSet(double speedValue)
        {
            log.Debug($"PLC {nameof(MotorSpeedSet)} = {speedValue}");
            modbusTcpClint.WriteOneRegister("11", (ushort)(speedValue * 10));
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void Ground(int index)
        {
            log.Debug($"PLC {nameof(Ground)} = {index}");
            modbusTcpClint.WriteOneRegister("0x0d", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void LightGreen(int index)
        {
            log.Debug($"PLC {nameof(LightGreen)} = {index}");
            modbusTcpClint.WriteOneRegister("0x0e", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void LightRed(int index)
        {
            log.Debug($"PLC {nameof(LightRed)} = {index}");
            modbusTcpClint.WriteOneRegister("0x0f", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void LightYellow(int index)
        {
            log.Debug($"PLC {nameof(LightYellow)} = {index}");
            modbusTcpClint.WriteOneRegister("0x10", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void Buzzer(int index)
        {
            log.Debug($"PLC {nameof(Buzzer)} = {index}");
            modbusTcpClint.WriteOneRegister("0x11", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

        public static void BtnStartLight(int index)
        {
            log.Debug($"PLC {nameof(BtnStartLight)} = {index}");
            modbusTcpClint.WriteOneRegister("0x12", (ushort)index);
            System.Threading.Thread.Sleep(delayAction);
        }

    }
}
