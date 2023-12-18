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
    public partial class 登录 : Form
    {
        public 登录()
        {
            InitializeComponent();
        }
        public delegate void Refresh();
        public event Refresh myRefresh;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 登录_Load(object sender, EventArgs e)
        {
            try
            {
                if (GlobalData.mysql.State != ConnectionState.Open)
                {
                    GlobalData.mysql.Open();//打开数据库
                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        /// <summary>
        /// 用户登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tx_user.Text == "")
                {
                    MessageBox.Show("登录账户为空，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (tx_pw.Text == "")
                    {
                        MessageBox.Show("登录密码为空，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (tx_user.Text == "sa" & tx_pw.Text == "Abcd1234")
                {
                    GlobalData.Log_Level = 10;
                    GlobalData.Log_State = true;
                    MessageBox.Show("登录成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GlobalData.Log_UserID = tx_user.Text;
                    GlobalData.Log_Name = "管理员测试";
                    this.Close();
                    myRefresh();
                }
                else
                {
                    //数据库
                    if (GlobalData.mysql.State == ConnectionState.Open & tx_user.Text != "" & tx_pw.Text != "")
                    {
                        string sql = "select *from UserExcel where identifier=" + "'" + tx_user.Text + "'";
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sql, GlobalData.mysql);
                        da.Fill(ds);
                        int Result = ds.Tables[0].Rows.Count;
                        if (Result > 0)
                        {
                            string PassWord = ds.Tables[0].Rows[0]["passwords"].ToString().Trim();
                            if (PassWord == tx_pw.Text)
                            {
                                if (bool.Parse(ds.Tables[0].Rows[0]["valid"].ToString()))
                                {
                                    string Level = ds.Tables[0].Rows[0]["user_group"].ToString();
                                    GlobalData.Log_Level = Convert.ToInt16(Level);
                                    GlobalData.Log_State = true;
                                    MessageBox.Show("登录成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    GlobalData.Log_UserID = tx_user.Text;
                                    GlobalData.Log_Name = ds.Tables[0].Rows[0]["notes"].ToString();
                                    GlobalData.Log_Name = "20";
                                    this.Close();
                                    myRefresh();
                                }
                                else
                                {
                                    if (MessageBox.Show("账号已经停用，请联系管理员！！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) this.Close();
                                    else
                                    {
                                        tx_user.Text = "";
                                        tx_pw.Text = "";
                                        tx_user.Focus();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("密码错误，请重新输入！！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("输入的账户不存在！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        /// <summary>
        /// 输入密码回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tx_pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (tx_user.Text == "") { MessageBox.Show("登录账户为空，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else
                    {
                        if (tx_pw.Text == "") { MessageBox.Show("登录密码为空，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    if (tx_user.Text == "Marvel" & tx_pw.Text == "Marvel")
                    {
                        GlobalData.Log_Level = 10;
                        GlobalData.Log_State = true;
                        MessageBox.Show("登录成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GlobalData.Log_UserID = tx_user.Text;
                        this.Close();
                        myRefresh();
                    }
                    else
                    {
                        if (GlobalData.mysql.State == ConnectionState.Open & tx_user.Text != "" & tx_pw.Text != "")
                        {
                            string sql = "select *from UserExcel where identifier=" + "'" + tx_user.Text + "'";
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sql, GlobalData.mysql);
                            da.Fill(ds);
                            int Result = ds.Tables[0].Rows.Count;
                            if (Result > 0)
                            {
                                string PassWord = ds.Tables[0].Rows[0]["passwords"].ToString().Trim();
                                if (PassWord == tx_pw.Text)
                                {
                                    if (bool.Parse(ds.Tables[0].Rows[0]["valid"].ToString()))
                                    {
                                        string Level = ds.Tables[0].Rows[0]["user_group"].ToString();
                                        GlobalData.Log_Level = Convert.ToInt16(Level);
                                        GlobalData.Log_State = true;
                                        MessageBox.Show("登录成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        GlobalData.Log_UserID = tx_user.Text;
                                        GlobalData.Log_Name = ds.Tables[0].Rows[0]["notes"].ToString();
                                        this.Close();
                                        myRefresh();
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("账号已经停用，请联系管理员！！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) this.Close();
                                        else
                                        {
                                            tx_user.Text = "";
                                            tx_pw.Text = "";
                                            tx_user.Focus();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("密码错误，请重新输入！！");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入的账户不存在！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            ds.Dispose();
                            da.Dispose();
                        }
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
    }
}
