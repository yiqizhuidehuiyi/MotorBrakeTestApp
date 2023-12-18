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
    public partial class FrmCS9914AX : Form
    {
        public FrmCS9914AX()
        {
            InitializeComponent();
        }

        private void FrmCS9914AX_Load(object sender, EventArgs e)
        {
            label1.Text = $"串口状态 : {Services.Meter.CS9914AX.SerialPort.IsOpen}";
            label2.Text = $"串口名称 : {Services.Meter.CS9914AX.SerialPort.PortName}";
        }

        //发送
        private void button1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox3.Text, out double time))
            {
                textBox3.Text = string.Empty;
                return;
            }
            Services.Meter.CS9914AX.SetTime(time);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double voltage))
            {
                textBox1.Text = string.Empty;
                return;
            }
            Services.Meter.CS9914AX.SetVoltage(voltage);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int current))
            {
                textBox2.Text = string.Empty;
                return;
            }
            Services.Meter.CS9914AX.SetCurrent(current);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Services.Meter.CS9914AX.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Services.Meter.CS9914AX.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var results = Services.Meter.CS9914AX.GetResult();
            label6.Text = $"电压 ：{results[0]}";
            label7.Text = $"电流 ：{results[1]}";
            label8.Text = $"时长 ：{results[2]}";
        }
    }
}
