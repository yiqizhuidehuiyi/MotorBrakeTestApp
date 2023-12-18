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
    public partial class FrmWT310E : Form
    {
        public FrmWT310E()
        {
            InitializeComponent();
        }

        private void FrmWT310E_Load(object sender, EventArgs e)
        {
            label3.Text = $"串口状态 : {Services.Meter.WT310E.SerialPort.IsOpen}";
            label2.Text = $"串口名称 : {Services.Meter.WT310E.SerialPort.PortName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.WT310E.IDN();
            label6.Text = back;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.WT310E.NUMericVALue();
            label4.Text = back;
            double[] results = Services.Meter.WT310E.ConvertToDoubles(back);
            label14.Text = results[0].ToString();
            label13.Text = results[1].ToString();
            label12.Text = results[2].ToString();
            label11.Text = results[7].ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var back = Services.Meter.WT310E.NUMericNORMal();
            label4.Text = back;
        }
    }
}
