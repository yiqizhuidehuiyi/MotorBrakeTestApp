using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotorBrakeTestApp.Views
{
    public partial class FrmDialogboxSave : Form
    {
        public FrmDialogboxSave(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
            AutoClose(5);
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void AutoClose(int sec)
        {

            for (int i = 0; i < sec; i++)
            {
                await Task.Delay(1000);
                label1.Text = $"{sec - i} 秒后自动关闭";
            }
            Close();
        }
    }
}
