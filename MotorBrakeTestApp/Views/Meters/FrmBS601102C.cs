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
    public partial class FrmBS601102C : Form
    {
        public FrmBS601102C()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double voltage))
            {
                Services.Meter.BS601102C.SetVoltage(voltage);
                return;
            }
            textBox1.Text = string.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double voltage))
            {
                Services.Meter.BS601102C.SetFrequency(voltage);
                return;
            }
            textBox2.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Services.Meter.BS601102C.Start();
            label6.Text = "启动";
            label6.BackColor= Color.Green; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Services.Meter.BS601102C.Stop();
            label6.Text = "停止";
            label6.BackColor = Color.Transparent;
        }

        private void FrmBS601102C_Load(object sender, EventArgs e)
        {
            label1.Text = $"串口号：{Services.Meter.BS601102C.SerialPort.PortName}";
            label2.Text = $"串口打开：{Services.Meter.BS601102C.SerialPort.IsOpen}";
        }
    }
}
