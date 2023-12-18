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
    public partial class FrmTH2512B : Form
    {
        public FrmTH2512B()
        {
            InitializeComponent();
        }

        private void FrmTH2512B_Load(object sender, EventArgs e)
        {
            label3.Text = $"串口状态 : {Services.Meter.TH2512B.SerialPort.IsOpen}";
            label2.Text = $"串口名称 : {Services.Meter.TH2512B.SerialPort.PortName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.TH2512B.IDN();
            label6.Text = back;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.TH2512B.Ask();
            label4.Text = back;
            var result = Services.Meter.TH2512B.ConvertToDouble(back);
            label7.Text = result.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.KBM50MODBUS.GetTemperature();
            label8.Text = back.ToString();
        }
    }
}
