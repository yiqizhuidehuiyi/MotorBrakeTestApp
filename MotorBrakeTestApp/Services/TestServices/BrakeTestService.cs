using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services.TestServices
{
    internal class BrakeTestService
    {
        Main frmMain;
        bool stopFlag = false;
        public bool SoftStartFlag { get; set; } = false;
        public int brakeSourceEnum;

        public BrakeTestService(Main frmMain)
        {
            this.frmMain = frmMain;
        }

        public bool HasStop()
        {
            if (stopFlag) return true;
            stopFlag = true;
            if (!PLC.EmergencyStopButton) { return true; }
            if (PLC.StopButton) { return true; }
            if (PLC.DoorSafety) { return true; }
            stopFlag = false;
            return false;
        }

        public void ResetStop()
        {
            stopFlag = false;
        }

        public void Ready()
        {

        }

        public async Task WaitStart()
        {
            while (true)
            {
                await Task.Delay(200);
                if (SoftStartFlag || Services.PLC.StartButton)
                {
                    SoftStartFlag = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 延时指定的时间，循环间隔100ms执行action方法。
        /// </summary>
        /// <param name="millisecond"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task Delay(int millisecond, Action action = null)
        {
            var endDateTime = DateTime.Now + TimeSpan.FromMilliseconds(millisecond);
            while (DateTime.Now < endDateTime)
            {
                await Task.Delay(100);
                if (HasStop()) { break; }
                if (action != null) action();
                if (HasStop()) { break; }
            }
        }

        public bool HasStopFlag()
        {
            return false;
        }

        public async Task Reset()
        {
            PLC.MustStop();
            await Task.Delay(200);
        }

        public async Task<double> MeasDcResiBs()
        {
            await Task.Delay(200);
            PLC.DcResi(1);
            await Task.Delay(2000);
            var back = Meter.TH2512B.Ask();
            PLC.DcResi(0);
            var result = Meter.TH2512B.ConvertToDouble(back);
            return result;
        }

        public async Task<double> MeasDcResiTs()
        {
            await Task.Delay(200);
            PLC.DcResi(3);
            await Task.Delay(2000);
            var back = Meter.TH2512B.Ask();
            await Task.Delay(50);
            PLC.DcResi(0);
            var result = Meter.TH2512B.ConvertToDouble(back);
            return result;
        }

        public async Task DownMainCylinder()
        {
            PLC.MainCylinder(2);
            await Task.Delay(5000);
        }

        public async Task UpMainCylinder()
        {
            PLC.MainCylinder(1);
            DateTime endTime = DateTime.Now + TimeSpan.FromSeconds(5);
            while (PLC.MainCylinderLimitState != 1
                && endTime > DateTime.Now)
            {
                await Task.Delay(100);
            }
            PLC.MainCylinder(0);
        }

        public async Task<double[]> MeasInusResi()
        {
            await Task.Delay(200);
            PLC.InusResi(1);
            await Task.Delay(200);
            var back = Meter.TH2683B.TRG();
            PLC.InusResi(0);
            await Task.Delay(200);
            var result = Meter.TH2683B.ConvertToDoubles(back);
            return result;
        }

        public async Task<double[]> MeasWithstand(double voltage, double delay, double leakage = 20)
        {
            double testTime = 1;
            await Task.Delay(200);
            Meter.CS9914AX.SetCurrent((int)leakage);
            await Task.Delay(50);
            Meter.CS9914AX.SetVoltage(voltage);
            await Task.Delay(50);
            Meter.CS9914AX.SetTime(testTime);
            await Task.Delay(50);
            PLC.WithstandVoltage(1);
            await Task.Delay(200);
            Meter.CS9914AX.Start();
            await Task.Delay((int)(testTime * 1000) + 500);
            PLC.WithstandVoltage(0);
            await Task.Delay(100);
            var results = Meter.CS9914AX.GetResult();
            await Task.Delay(200);
            Meter.CS9914AX.Stop();
            return results;
        }

        public async Task OpenBrakeSource(float voltage)
        {
            if (voltage > 25)
            {
                brakeSourceEnum = 1;
                Meter.BS601102C.SetFrequency(50);
                await Task.Delay(100);
                Meter.BS601102C.SetVoltage(voltage);
                await Task.Delay(100);
                Meter.BS601102C.Start();
                await Task.Delay(100);
                PLC.VoltageSource(1);
                await Task.Delay(2000);
            }
            else if (voltage <= 30 && voltage > 17)
            {
                brakeSourceEnum = 2;
                PLC.VoltageSource(2);
            }
            else if (voltage <= 17)
            {
                brakeSourceEnum = 3;
                PLC.VoltageSource(3);
            }

            PLC.Adapter(GlobalData.SelectedBrakeVoltage.Address);
        }

        public async Task SetBrakeSourceVoltage(float voltage)
        {
            Meter.BS601102C.SetVoltage(voltage);
            await Task.Delay(100);
        }

        public async Task CloseBrakeSource()
        {
            if (brakeSourceEnum == 1)
            {
                Meter.BS601102C.Stop();
                await Task.Delay(100);
            }
            PLC.VoltageSource(0);
            await Task.Delay(100);
            PLC.Adapter(0);
            brakeSourceEnum = 0;
        }

        public void SetBrakeSpeed(double speed)
        {
            PLC.MotorSpeedSet(speed * 9.59);
        }
    }
}
