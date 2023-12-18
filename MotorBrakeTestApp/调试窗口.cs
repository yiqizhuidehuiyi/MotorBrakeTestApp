using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HslCommunication.Profinet.Siemens;
using MotorBrakeTestApp.Views.Meters;

namespace MotorBrakeTestApp
{
    public partial class debugTest : Form
    {
        public debugTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// PLC(S7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Views.Meters.FrmPLC frmPLC = new Views.Meters.FrmPLC();
            frmPLC.Show();
            //Hide();
            //using (西门子PLC_S7 PLC = new 西门子PLC_S7(SiemensPLCS.S1200))
            //{
            //    PLC.ShowDialog();
            //}
            //Show();
        }

        /// <summary>
        /// PLC IO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            //Hide();
            //using (PLC读取地址设定 PLCIO = new PLC读取地址设定())
            //{
            //    PLCIO.ShowDialog();
            //}
            //Show();
            //Services.BrakeDbContext context = new Services.BrakeDbContext();
            //var rr = context.VoltageModel.ToList();
            FrmBS601102C frmBS601102C = new FrmBS601102C();
            frmBS601102C.Show();
        }

        /// <summary>
        /// 多功能数字表   (DL8A)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //Hide();
            //using (Modbus_RTU RTU = new Modbus_RTU())
            //{
            //    RTU.ShowDialog();
            //}
            //Show();
            FrmTH2683B frmTH2683B = new FrmTH2683B();
            frmTH2683B.Show();
        }

        /// <summary>
        /// 安规测试仪(Ainuo)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //Hide();
            //using (Ainuo安规测试仪 安规 = new Ainuo安规测试仪())
            //{
            //    安规.ShowDialog();
            //}
            //Show();
            FrmCS9914AX frmCS9914AX = new FrmCS9914AX();
            frmCS9914AX.Show();
        }

        /// <summary>
        /// 数字功率计(WT310E)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //Hide();
            //using (ModbusTCP TCP = new ModbusTCP())
            //{
            //    TCP.ShowDialog();
            //}
            //Show();
            FrmWT310E frmWT310 = new FrmWT310E();
            frmWT310.Show();
        }

        /// <summary>
        /// 直流电阻测试仪(TH2516)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button4_Click(object sender, EventArgs e)
        {
            //Hide();
            //using (直流电阻测试仪 TH2516 = new 直流电阻测试仪())
            //{
            //    TH2516.ShowDialog();
            //}
            //Show();
            FrmTH2512B frmTH2512B = new FrmTH2512B();
            frmTH2512B.Show();
        }

        private void btnModbusTCP_Click(object sender, EventArgs e)
        {
            using (ModbusTCP TCP = new ModbusTCP())
            {
                TCP.ShowDialog();
            }
        }
    }
}
