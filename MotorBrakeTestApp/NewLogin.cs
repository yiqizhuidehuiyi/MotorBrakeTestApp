using ClassLogin_Input;
using MotorBrakeTestApp.WebApi.Read.Universal;
using MotorBrakeTestApp.WebApi.SuZhouMotor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotorBrakeTestApp
{
    public partial class NewLogin : Form
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(NewLogin));

        public NewLogin()
        {
            InitializeComponent();
        }

        private void NewLogin_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_Click(object sender, EventArgs e)
        {
            if (!UnifyJudge()) return;
            LoginInInput loginInInput = new LoginInInput();
            loginInInput.username = txtuserN.Text;
            loginInInput.password = txtPassword.Text;
            loginInInput.clientType = "testBench_brake";
            RoutineTestUser routineTestUser = WebApiFuction.Login(loginInInput);
            if (routineTestUser != null)
            {
                MessageBox.Show("登录成功!");
                log.Debug("登录成功!");
                this.Close();
            }
            else
            {
                MessageBox.Show("登录失败!");
                log.Debug("登录失败!");
            }

        }

        /// <summary>
        /// 统一判断
        /// </summary>
        private bool UnifyJudge()
        {
            if (string.IsNullOrEmpty(txtuserN.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("用户名或密码为空,请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 确认登录
        /// </summary>
        /// <returns></returns>
        private bool LoginAffirm()
        {
            bool flag = false;
            if (txtuserN.Text == "sa" && txtPassword.Text == "Abcd1234")
            {
                GlobalData.Log_Level = 10;
                GlobalData.Log_State = true;
                MessageBox.Show("登录成功!");
                GlobalData.Log_UserID = txtuserN.Text;
                GlobalData.Log_Name = "管理员测试";
                this.Close();
                flag = true;
            }
            return flag;
        }

        /// <summary>
        ///数据库配置 
        /// </summary>
        private void DatabaseJudge()
        {
            if (GlobalData.mysql.State == ConnectionState.Open & txtuserN.Text != "" & txtPassword.Text != "")
            {
                string sql = "select *from UserExcel where identifier=" + "'" + txtuserN.Text + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sql, GlobalData.mysql);
                da.Fill(ds);
                int Result = ds.Tables[0].Rows.Count;
                if (Result > 0)
                {
                    string PassWord = ds.Tables[0].Rows[0]["passwords"].ToString().Trim();
                    if (PassWord == txtPassword.Text)
                    {
                        if (bool.Parse(ds.Tables[0].Rows[0]["valid"].ToString()))
                        {
                            string Level = ds.Tables[0].Rows[0]["user_group"].ToString();
                            GlobalData.Log_Level = Convert.ToInt16(Level);
                            GlobalData.Log_State = true;
                            MessageBox.Show("登录成功!");
                            GlobalData.Log_UserID = txtuserN.Text;
                            GlobalData.Log_Name = ds.Tables[0].Rows[0]["notes"].ToString();
                            GlobalData.Log_Name = "20";
                            this.Close();
                        }
                        else
                        {
                            if (MessageBox.Show("账号已经停用，请联系管理员！！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) this.Close();
                            else
                            {
                                txtuserN.Text = "";
                                txtPassword.Text = "";
                                txtuserN.Focus();
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
}
