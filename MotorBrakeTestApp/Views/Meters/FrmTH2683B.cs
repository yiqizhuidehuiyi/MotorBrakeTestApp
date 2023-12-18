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
    public partial class FrmTH2683B : Form
    {
        public FrmTH2683B()
        {
            InitializeComponent();
        }

        private void FrmTH2683B_Load(object sender, EventArgs e)
        {
            label3.Text = $"串口状态 : {Services.Meter.TH2683B.SerialPort.IsOpen}";
            label2.Text = $"串口名称 : {Services.Meter.TH2683B.SerialPort.PortName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.TH2683B.IDN();
            label6.Text = back;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.TH2683B.TRG();
            label4.Text = back;
            var results = Services.Meter.TH2683B.ConvertToDoubles(back);
            label8.Text = results[0].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Services.Meter.TH2683B.OVOL();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Services.Meter.TH2683B.DISCharge();
        }
    }
}
