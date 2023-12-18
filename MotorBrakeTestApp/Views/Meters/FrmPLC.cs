using MotorBrakeTestApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotorBrakeTestApp.Views.Meters
{
    public partial class FrmPLC : Form
    {
        private bool hasRunning = true;
        public FrmPLC()
        {
            InitializeComponent();
            CheckStateAsync();
        }

        private async void CheckStateAsync()
        {
            while (hasRunning)
            {
                await Task.Delay(10);
                chkMustStop.Checked = PLC.EmergencyStopButton;
                chkStart.Checked = PLC.StartButton;
                chkStop.Checked = PLC.StopButton;
                chkSafty.Checked = PLC.DoorSafety;
                chkPower.Checked = PLC.PowerState;
                lblMainBtn.Text = $"主缸按钮状态：{PLC.MainCylinderButtonState}";
                lblMainLimit.Text = $"主缸限位状态：{PLC.MainCylinderLimitState}";
                lblPos1.Text = $"位移1：{PLC.DisplacementSensor1}";
                lblPos2.Text = $"位移2：{PLC.DisplacementSensor2}";
                lblTroque.Text = $"转矩：{PLC.TorqueValue:0.00}";
                lblSpeed.Text = $"转速：{PLC.SpeedValue:0.0}";
            }
        }

        private void rdoDcResi_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton1):
                        PLC.DcResi(0);
                        break;
                    case nameof(radioButton25):
                        PLC.DcResi(1);
                        break;
                    case nameof(radioButton24):
                        PLC.DcResi(2);
                        break;
                    case nameof(radioButton2):
                        PLC.DcResi(3);
                        break;
                }
            }
        }

        private void rdoInusResi_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton7):
                        PLC.InusResi(0);
                        break;
                    case nameof(radioButton6):
                        PLC.InusResi(1);
                        break;
                    case nameof(radioButton5):
                        PLC.InusResi(2);
                        break;
                    case nameof(radioButton4):
                        PLC.InusResi(3);
                        break;
                }
            }
        }

        private void rdoInterturn_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton3):
                        PLC.Interturn(0);
                        break;
                    case nameof(radioButton10):
                        PLC.Interturn(1);
                        break;
                    case nameof(radioButton9):
                        PLC.Interturn(2);
                        break;
                    case nameof(radioButton8):
                        PLC.Interturn(3);
                        break;
                }
            }
        }

        private void FrmPLC_Load(object sender, EventArgs e)
        {

        }

        private void rdoWithstandVoltage_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton11):
                        PLC.WithstandVoltage(0);
                        break;
                    case nameof(radioButton14):
                        PLC.WithstandVoltage(1);
                        break;
                    case nameof(radioButton13):
                        PLC.WithstandVoltage(2);
                        break;
                    case nameof(radioButton12):
                        PLC.WithstandVoltage(3);
                        break;
                }
            }
        }

        private void rdoAdapter_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton15):
                        PLC.Adapter(0);
                        break;
                    case nameof(radioButton18):
                        PLC.Adapter(1);
                        break;
                    case nameof(radioButton17):
                        PLC.Adapter(2);
                        break;
                    case nameof(radioButton16):
                        PLC.Adapter(3);
                        break;
                    case nameof(radioButton19):
                        PLC.Adapter(4);
                        break;
                    case nameof(radioButton22):
                        PLC.Adapter(5);
                        break;
                    case nameof(radioButton21):
                        PLC.Adapter(6);
                        break;
                }
            }
        }

        private void rdoStop_CheckedChanged(object sender, EventArgs e)
        {
            PLC.MustStop();
        }

        private void rdoVoltageSource_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton51):
                        PLC.VoltageSource(0);
                        break;
                    case nameof(radioButton54):
                        PLC.VoltageSource(1);
                        break;
                    case nameof(radioButton53):
                        PLC.VoltageSource(2);
                        break;
                    case nameof(radioButton52):
                        PLC.VoltageSource(3);
                        break;
                }
            }
        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtSetSpeed.Text, out var speed))
            {
                PLC.MotorSpeedSet(speed);
            }
        }

        private void rdoMainCylinder_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton59):
                        PLC.MainCylinder(0);
                        break;
                    case nameof(radioButton62):
                        PLC.MainCylinder(1);
                        break;
                    case nameof(radioButton61):
                        PLC.MainCylinder(2);
                        break;
                }
            }
        }

        private void rdoMotorRun_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton67):
                        PLC.MotorRun(0);
                        break;
                    case nameof(radioButton70):
                        PLC.MotorRun(1);
                        break;
                    case nameof(radioButton69):
                        PLC.MotorRun(2);
                        break;
                }
            }
        }

        private void FrmPLC_FormClosed(object sender, FormClosedEventArgs e)
        {
            hasRunning = false;
        }

        private void rdoDcResiProtection_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radio)
            {
                switch (radio.Name)
                {
                    case nameof(radioButton20):
                        PLC.DcResiProtection(0);
                        break;
                    case nameof(radioButton28):
                        PLC.DcResiProtection(1);
                        break;
                }
            }
        }
    }
}
