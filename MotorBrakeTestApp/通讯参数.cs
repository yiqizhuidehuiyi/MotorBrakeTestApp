using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
namespace MotorBrakeTestApp
{

    public partial class SetData : Form
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SetData));
        public SetData()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log.Debug("启动通讯参数设置窗体");
            string[] ArryPort = System.IO.Ports.SerialPort.GetPortNames();
            //this.Location = new Point(200, 200);
            for (int i = 0; i < ArryPort.Length; i++)
            {
                comboBox1.Items.Add(ArryPort[i]);
                comboBox2.Items.Add(ArryPort[i]);
                comboBox3.Items.Add(ArryPort[i]);
                comboBox4.Items.Add(ArryPort[i]);
            }
            textBox2.Text = GlobalData.PLC_IP;
            tx_sql_add.Text = GlobalData.Sql_add;
            tx_sql_id.Text = GlobalData.Sql_ID;
            tx_sql_pw.Text = GlobalData.Sql_pw;
            comboBox1.Text = GlobalData.R_DC;
            comboBox2.Text = GlobalData.WithstandPortName;
            comboBox3.Text = GlobalData.Power_digital;
            comboBox4.Text = GlobalData.Poewr_Tester;
            dataGridView1.DataSource = GlobalData.BrakeVoltageDb;
            dataGridView1.AutoResizeColumns();
        }
        /// <summary>
        /// 重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text || comboBox1.Text == comboBox4.Text || comboBox2.Text == comboBox3.Text || comboBox2.Text == comboBox4.Text || comboBox3.Text == comboBox4.Text)
                {
                    MessageBox.Show("端口选择有重复！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "" || tx_sql_add.Text == "" || tx_sql_id.Text == "" || tx_sql_pw.Text == "")
                {
                    MessageBox.Show("确实必要参数，请填写完整！！");
                }
                else
                {
                    GlobalData.PLC_IP = textBox2.Text;
                    GlobalData.Sql_add = tx_sql_add.Text;
                    GlobalData.Sql_ID = tx_sql_id.Text;
                    GlobalData.Sql_pw = tx_sql_pw.Text;
                    GlobalData.R_DC = comboBox1.Text;
                    GlobalData.WithstandPortName = comboBox2.Text;
                    GlobalData.Power_digital = comboBox3.Text;
                    GlobalData.Poewr_Tester = comboBox4.Text;
                    GlobalData.UpdataParameter();   //更新配置文件
                    log.DebugFormat("更新配置文件成功！");
                    MessageBox.Show("保存配置文件成功！");
                }
            }
            catch (Exception E)
            {
                log.DebugFormat("保存配置文件失败:" + E.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Services.BrakeDbContext context = new Services.BrakeDbContext();
            Services.VoltageModels model = new Services.VoltageModels();
            model.BrakeModel = textBox3.Text;
            model.BrakeVoltage = Convert.ToDouble(textBox4.Text);
            model.Adapter = textBox5.Text;
            model.Address = Convert.ToInt16(textBox6.Text);
            context.VoltageModel.Add(model);
            context.SaveChanges();
            GlobalData.BrakeVoltageDb = context.VoltageModel.ToList();
            dataGridView1.DataSource = GlobalData.BrakeVoltageDb;
            dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
