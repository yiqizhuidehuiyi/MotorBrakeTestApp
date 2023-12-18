using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace MotorBrakeTestApp
{
    public partial class 添加用户 : Form
    {
        public 添加用户()
        {
            InitializeComponent();
        }

        private void 添加用户_Load(object sender, EventArgs e)
        {
            this.Location = new Point(350,400);
            this.pictureBox1.SendToBack();
            this.label1.BackColor = Color.Transparent;
            this.label1.Parent = this.pictureBox1;
            this.label1.BringToFront();
            this.label2.BackColor = Color.Transparent;
            this.label2.Parent = this.pictureBox1;
            this.label2.BringToFront(); this.label1.BackColor = Color.Transparent;
            this.label2.Parent = this.pictureBox1;
            this.label2.BringToFront();
            this.label3.BackColor = Color.Transparent;
            this.label3.Parent = this.pictureBox1;
            this.label3.BringToFront();
            this.label4.BackColor = Color.Transparent;
            this.label4.Parent = this.pictureBox1;
            this.label4.BringToFront();
            this.label5.BackColor = Color.Transparent;
            this.label5.Parent = this.pictureBox1;
            this.label5.BringToFront();
            this.label6.BackColor = Color.Transparent;
            this.label6.Parent = this.pictureBox1;
            this.label6.BringToFront();
            if (GlobalData .Log_Level==1)
            {
                cb_Level.Items.Add("普通用户");
                cb_Level.Items.Add("管理员");
            }
            if (GlobalData.Log_Level >=5)
            {
                cb_Level.Items.Add("普通用户");
                cb_Level.Items.Add("管理员");
                cb_Level.Items.Add("开发人员");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Level = null;
            try
            {
                if (GlobalData .mysql .State !=ConnectionState.Open)
                {
                    GlobalData.mysql.Open();
                }
                if (tx_id.Text == "" || tx_pw.Text == "" || tx_pw_twice.Text == "" || cb_Level.Text == ""||txname .Text =="")
                {
                    MessageBox.Show("信息填写不完整,请输入完整信息！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (tx_pw.Text != tx_pw_twice.Text)
                {
                    MessageBox.Show("两次输入的密码不一样，请重新输入！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx_pw_twice.Focus();
                }
                else
                {
                    string sqlcmd = "select *from UserExcel where identifier=" + "'" + tx_id.Text + "'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd, GlobalData.mysql);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("用户名已经存在，请重新输入！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (cb_Level.Text == "普通用户")
                        {
                            Level = "0";
                        }
                        else if (cb_Level.Text == "管理员")
                        {
                            Level = "1";
                        }
                        else if (cb_Level.Text == "开发人员")
                        {
                            Level = "10";
                        }
                        sqlcmd = "insert  UserExcel(identifier,passwords,user_group,notes) values(" + "'"+tx_id.Text.Trim().ToString() + "'," +"'" +tx_pw.Text.Trim().ToString() + "'," +"'"+ Level + "'," + "'" + txname .Text .Trim ()+"')";
                        SqlCommand myCom = new SqlCommand(sqlcmd, GlobalData.mysql);
                        int Result =(int) myCom.ExecuteNonQuery();
                        if (Result > 0)
                        {
                            MessageBox.Show("添加数据成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("添加数据失败！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
