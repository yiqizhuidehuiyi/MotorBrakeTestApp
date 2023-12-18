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
    public partial class 删除用户 : Form
    {
        public 删除用户()
        {
            InitializeComponent();
        }
        public string sql = null;
        private void 删除用户_Load(object sender, EventArgs e)
        {  
            this.Location = new Point(350, 400);
            this.pictureBox1.SendToBack();
            this.label2.BackColor = Color.Transparent;
            this.label2.Parent = this.pictureBox1;
            this.label2.BringToFront();
            this.label3.BackColor = Color.Transparent;
            this.label3.Parent = this.pictureBox1;
            this.label3.BringToFront();
            try
            {

                if (GlobalData.mysql.State != ConnectionState.Open)
                {
                    GlobalData.mysql.Open();
                }
                if(GlobalData .Log_Level ==1)
                {
                     sql = "select *from UserExcel where user_group='1' or user_group='0' ";
                }else if (GlobalData.Log_Level >= 5)
                {
                    sql = "select identifier from UserExcel";
                }  
                SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData.mysql);
                DataSet Ds = new DataSet();
                Da.Fill(Ds, "UserExcel");
                cb_ID.DataSource = Ds.Tables["UserExcel"];
                cb_ID.DisplayMember = "identifier";
                Ds.Dispose();
                Da.Dispose();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalData.mysql.State != ConnectionState.Open)
                {
                    GlobalData.mysql.Open();
                }
                if (MessageBox .Show ("确定删除账户么？","警告",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.OK)
                {
                    string sqlcmd = "delete from UserExcel where identifier=" + "'" + cb_ID .Text +"'";
                    SqlCommand mysqlcmd = new SqlCommand(sqlcmd, GlobalData.mysql);
                    int Result = (int)mysqlcmd.ExecuteNonQuery();
                    if(Result >0)
                    {
                        cb_ID.DataSource = null;
                        cb_ID.Items.Clear();
                        MessageBox.Show("删除成功！！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SqlDataAdapter Da_2 = new SqlDataAdapter(sql, GlobalData.mysql);
                        DataSet Ds_2 = new DataSet();
                        Da_2.Fill(Ds_2, "UserExcel");
                        cb_ID.DataSource = Ds_2.Tables["UserExcel"];
                        cb_ID.DisplayMember = "identifier";
                        Da_2.Dispose();
                        Ds_2.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    mysqlcmd.Dispose();
                    
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                //Logger.Instance.WriteException(E);
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalData.mysql.State != ConnectionState.Open)
                {
                    GlobalData.mysql.Open();
                }
                if (MessageBox.Show("确定停用账户么？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string sqlcmd = " UPDATE UserExcel SET valid = 'False' where identifier = '"+cb_ID.Text +"'";
                    SqlCommand mysqlcmd = new SqlCommand(sqlcmd, GlobalData.mysql);
                    int Result = (int)mysqlcmd.ExecuteNonQuery();
                    if (Result > 0)
                    {
                        cb_ID.DataSource = null;
                        cb_ID.Items.Clear();
                        MessageBox.Show("修改成功！！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SqlDataAdapter Da_2 = new SqlDataAdapter(sql, GlobalData.mysql);
                        DataSet Ds_2 = new DataSet();
                        Da_2.Fill(Ds_2, "UserExcel");
                        cb_ID.DataSource = Ds_2.Tables["UserExcel"];
                        cb_ID.DisplayMember = "identifier";
                        Da_2.Dispose();
                        Ds_2.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("修改失败！！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    mysqlcmd.Dispose();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                //Logger.Instance.WriteException(E);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalData.mysql.State != ConnectionState.Open)
                {
                    GlobalData.mysql.Open();
                }
                if (MessageBox.Show("确定启用账户么？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string sqlcmd = " UPDATE UserExcel SET valid = 'True' where identifier = '"+cb_ID.Text +"'";
                    SqlCommand mysqlcmd = new SqlCommand(sqlcmd, GlobalData.mysql);
                    int Result = (int)mysqlcmd.ExecuteNonQuery();
                    if (Result > 0)
                    {
                        cb_ID.DataSource = null;
                        cb_ID.Items.Clear();
                        MessageBox.Show("修改成功！！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SqlDataAdapter Da_2 = new SqlDataAdapter(sql, GlobalData.mysql);
                        DataSet Ds_2 = new DataSet();
                        Da_2.Fill(Ds_2, "UserExcel");
                        cb_ID.DataSource = Ds_2.Tables["UserExcel"];
                        cb_ID.DisplayMember = "identifier";
                        Ds_2.Dispose();
                        Da_2.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("修改失败！！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    mysqlcmd.Dispose();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                //Logger.Instance.WriteException(E);
            }
        }
    }
}
