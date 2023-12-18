using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotorBrakeTestApp
{
    public partial class 设备通讯参数 : Form
    {
        public 设备通讯参数()
        {
            InitializeComponent();
        }

        private void 设备通讯参数_Load(object sender, EventArgs e)
        {
            this.Location = new Point(200, 200);
            txplcip.Text = GlobalData.PLC_IP;
            tx_sql_add.Text = GlobalData.Sql_add;
            tx_sql_id.Text = GlobalData.Sql_ID;
            tx_sql_pw.Text = GlobalData.Sql_pw;
            txRIP.Text = GlobalData.R_DC_IP;
            txRport.Text = GlobalData.R_DC_Port;
            txainuoip.Text = GlobalData.Angui_Tester_IP;
            txainuoport.Text = GlobalData.Angui_Tester_Port;
            txpowerip.Text = GlobalData.Poewr_Tester_IP;
            txpowerport.Text = GlobalData.Poewr_Tester_Port;
            txdigitalip.Text = GlobalData.Power_digital_IP;
            txdigitalport.Text = GlobalData.Power_digital_Port;
            txpath.Text = GlobalData.ExcelFilePath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txplcip.Text = "";
            tx_sql_add.Text = "";
            tx_sql_id.Text = "";
            tx_sql_pw.Text = "";
            txRIP.Text = "";
            txRport.Text = "";
            txainuoip.Text = "";
            txainuoport.Text = "";
            txpowerip.Text = "";
            txpowerport.Text = "";
            txdigitalip.Text = "";
            txdigitalport.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txplcip.Text == txRIP.Text || txplcip.Text == txainuoip.Text || txplcip.Text == txpowerip.Text || txplcip.Text == txdigitalip.Text || txRIP.Text == txainuoip.Text || txRIP.Text == txpowerip.Text || txRIP.Text == txdigitalip.Text || txainuoip.Text == txpowerip.Text || txainuoip.Text == txdigitalip.Text || txpowerip.Text == txdigitalip.Text)
                {
                    MessageBox.Show("IP重复，请重新填写！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txplcip.Text == "" || txplcPort.Text == "" || txRIP.Text == "" || txRport.Text == "" || txainuoip.Text == "" || txainuoport.Text == "" || txpowerip.Text == "" || txpowerport.Text == "" || txdigitalip.Text == "" || txdigitalport.Text == "" || txpath.Text == "")
                {
                    MessageBox.Show("缺失必要参数，请填写完整！！");
                }
                else
                {
                    GlobalData.PLC_IP = txplcip.Text.Trim();
                    GlobalData.Sql_add = tx_sql_add.Text.Trim();
                    GlobalData.Sql_ID = tx_sql_id.Text.Trim();
                    GlobalData.Sql_pw = tx_sql_pw.Text.Trim();
                    GlobalData.R_DC_IP = txRIP.Text.Trim();
                    GlobalData.R_DC_Port = txRport.Text.Trim();
                    GlobalData.Angui_Tester_IP = txainuoip.Text.Trim();
                    GlobalData.Angui_Tester_Port = txainuoport.Text.Trim();
                    GlobalData.Poewr_Tester_IP = txpowerip.Text.Trim();
                    GlobalData.Poewr_Tester_Port = txpowerport.Text.Trim();
                    GlobalData.Power_digital_IP = txdigitalip.Text.Trim();
                    GlobalData.Power_digital_Port = txdigitalport.Text.Trim();
                    GlobalData.ExcelFilePath = txpath.Text;
                    GlobalData.Insulation_BValue = "12";
                    GlobalData.UpdataParameter();   //更新配置文件
                    if (MessageBox.Show("设置保存成功，下次启动时生效，是否马上重启软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        GlobalData.ReStartAPP();
                        this.Close();
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void btExcelPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK && fbd.SelectedPath != "")
            {
                txpath.Text = fbd.SelectedPath;
            }
        }
    }
}
