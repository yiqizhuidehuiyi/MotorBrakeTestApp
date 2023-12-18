using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace MotorBrakeTestApp
{
    public partial class AlarmWindows : Form
    {
        public AlarmWindows()
        {
            InitializeComponent();
        }

        private void AlarmWindows_Load(object sender, EventArgs e)
        {
            Thread ThreadConnectAganin = new Thread(new ThreadStart(Connect));
            ThreadConnectAganin.Start();
            this.Location = new Point(200, 200);
        }
        private void Connect()
        {
            bool TryConnect=true;
            while (TryConnect)
            {
                try
                {
                    if(GlobalData .PLC_Connect_State !=true)
                    {
                        userLantern1.LanternBackground = Color.Red;
                        Equipment_Device.PLCInitialize();
                    }
                    else
                    {
                        userLantern1.LanternBackground = Color.LimeGreen;
                        TryConnect = false;
                        Main.IsStartTest01ThreadRun = true;
                    }
                    //Main.Link_AinuoTester();
                }
                catch (Exception E)
                {

                }
            }
        }
    }
}
