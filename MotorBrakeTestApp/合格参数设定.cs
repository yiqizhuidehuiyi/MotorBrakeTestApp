using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace MotorBrakeTestApp
{
    public partial class 合格参数设定 : Form
    {
        public 合格参数设定()
        {
            InitializeComponent();
        }
        private bool AddModel=false ;
        private int tim;
        private int Times;
        private int times=-1;
        private int times04=-1;
        private Socket Ainuo_socketCore = null;
        private byte[] buffer = new byte[2048];
        private bool Ainuo_Connect_State;
        private int GetAinuoSendTimes;
        public delegate void Refresh();
        public event Refresh myRefresh;
        private void 合格参数设定_Load(object sender, EventArgs e)
        {
            try
            {

                this.Location = new Point(200, 200);

                LoadPlcParameter();

                txaddModel.Visible = true;
                cbModel.Enabled = false;
                panel1.Enabled = false;
                AddModel = false;
                label30.Visible = false;
                txaddModel.Visible = false;
                if(GlobalData.mysql.State != ConnectionState.Open)
                {
                    GlobalData. mysql.Open();
                }
                if (GlobalData. mysql.State == ConnectionState.Open)
                {
                    string sql = "select identifier from brake_routine_standard";
                    DataSet Ds = new DataSet();
                    SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData. mysql);
                    Da.Fill(Ds, "brake_routine_standard");
                    cbModel.DataSource = Ds.Tables["brake_routine_standard"];
                    cbModel.DisplayMember = "identifier";
                    cbModel.Enabled = true;
                    panel1.Enabled = true;
                    cbModel.Focus();
                    sql = "select distinct brake_routine_standard_id from brake_routine_standard_detail";
                    Da = new SqlDataAdapter(sql, GlobalData.mysql);
                    Da.Fill(Ds, "brake_routine_standard_detail");
                    comboBox2.DataSource = Ds.Tables["brake_routine_standard_detail"];
                    comboBox2.DisplayMember = "brake_routine_standard_id";
                    comboBox2.Enabled = true;
                    comboBox2.Focus();
                    sql = "select *from 本底值 where ID='1'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sql, GlobalData.mysql);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        textBox7.Text = ds.Tables[0].Rows[0]["绝缘电阻"].ToString();
                        textBox6 .Text = ds.Tables[0].Rows[0]["泄露电流"].ToString();
                        textBox5 .Text = ds.Tables[0].Rows[0]["转矩"].ToString();
                    }
                    Ds.Dispose();
                    Da.Dispose();
                }
                else
                {
                    MessageBox.Show("测试2");
                    MessageBox.Show("数据库打开失败");
                    panel1.Enabled = false;
                }
                if (Ainuo_Connect_State != true) button1.Enabled = false;
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Times += 1;
                if (GlobalData.mysql.State == ConnectionState.Open & Times > 1)
                {
                    string sqlcmd = "select *from brake_routine_standard  where identifier=" + "'" + cbModel.Text  + "'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd, GlobalData.mysql);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        comboBox2.SelectedIndex = comboBox2.FindString(ds.Tables[0].Rows[0]["id"].ToString().Trim());
                        txR_tstime.Text = ((ds.Tables[0].Rows[0]["resistance_duration"].ToString().Trim())).ToString ();       //电阻测试时间
                        txts_temper.Text  = ds.Tables[0].Rows[0]["reference_temperature"].ToString().Trim();        //基准温度
                        textBox1.Text = ds.Tables[0].Rows[0]["resistance_coefficient"].ToString().Trim();      //电阻温度系数
                        textBox2.Text = ds.Tables[0].Rows[0]["insulation_resistance_duration"].ToString().Trim();   //绝缘电阻测试时间
                        txrmvoltage.Text= ds.Tables[0].Rows[0]["insulation_resistance_voltage"].ToString().Trim();  //绝缘电阻测试电压
                        txIR_L .Text= ds.Tables[0].Rows[0]["insulation_resistance_min"].ToString().Trim();                //绝缘电阻min                                                                                                 
                        txpuncture_V.Text = ds.Tables[0].Rows[0]["with_stand_voltage"].ToString().Trim();            //耐压电压
                        txpuncture_Time.Text  = ds.Tables[0].Rows[0]["with_stand_duration"].ToString().Trim();            //耐压时间
                        txleak_AH .Text = ds.Tables[0].Rows[0]["leakage_current_max"].ToString().Trim();                //泄露电流max
                        txRated_Time .Text = ds.Tables[0].Rows[0]["full_voltage_time"].ToString().Trim();           //满压时间
                        txRated_60Time .Text = ds.Tables[0].Rows[0]["decreased_voltage_time"].ToString().Trim();      //降压时间
                        textBox4.Text = ds.Tables[0].Rows[0]["decreased_voltage_ratio"].ToString().Trim();        //降压比例
                        txRun_ver.Text  = ds.Tables[0].Rows[0]["grinding_speed"].ToString().Trim();                      //磨合转速
                        txRun_Time .Text = ds.Tables[0].Rows[0]["grinding_time_min"].ToString().Trim();                 //磨合时间 min   
                        textBox3 .Text = ds.Tables[0].Rows[0]["grinding_time_max"].ToString().Trim();                //磨合时间 max
                        txRemain_NH .Text = ds.Tables[0].Rows[0]["residual_torque_max"].ToString().Trim();                //残留转矩max
                    }
                    da.Dispose();
                    ds.Dispose();
                }
                if (Times > 4)
                {
                    Times = 2;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)      //型号选择
        {
            try
            {
                tim += 1;
                if (times > 0)
                {
                    times = -2;
                }
                if (times04 > 0)
                {
                    times04 = -2;
                }
                if (GlobalData.mysql.State == ConnectionState.Open && tim >1)
                {
                    comboBox3.Enabled = false;
                    comboBox3.DataSource = null;
                    comboBox3.Items.Clear();
                    string sql = "select rated_voltage from brake_routine_standard_detail where brake_routine_standard_id='" + comboBox2.Text.Trim() + "'";
                    DataSet Ds = new DataSet();
                    SqlDataAdapter Da = new SqlDataAdapter(sql, GlobalData.mysql);
                    Da.Fill(Ds, "brake_routine_standard_detail");
                    comboBox3.DataSource = Ds.Tables["brake_routine_standard_detail"];
                    comboBox3.DisplayMember = "rated_voltage";
                    comboBox3.Enabled = true;
                    comboBox3.Focus();
                    comboBox4.DataSource = null;
                    comboBox4.Items.Clear();
                    sql = "select rated_torque from brake_routine_standard_torque where brake_routine_standard_id='" + comboBox2.Text.Trim() + "'";
                    Da = new SqlDataAdapter(sql, GlobalData.mysql);
                    Da.Fill(Ds, "brake_routine_standard_torque");
                    comboBox4.DataSource = Ds.Tables["brake_routine_standard_torque"];
                    comboBox4.DisplayMember = "rated_torque";
                    comboBox4.Enabled = true;
                    comboBox4.Focus();
                    Ds.Dispose();
                    Da.Dispose();
                }
                if (tim > 4)
                {
                    tim = 1;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)      //型号选择
        {
            try
            {
                times += 1;
                if (GlobalData.mysql.State == ConnectionState.Open && times >= 1)
                {
                    string sqlcmd = "select *from brake_routine_standard_detail  where brake_routine_standard_id='" + comboBox2.Text + "' and rated_voltage='" + comboBox3.Text + "'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd, GlobalData.mysql);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txR1L.Text = ds.Tables[0].Rows[0]["resistance_bs_min"].ToString().Trim();
                        txR1H.Text = ds.Tables[0].Rows[0]["resistance_bs_max"].ToString().Trim();
                        txR2L.Text = ds.Tables[0].Rows[0]["resistance_ts_min"].ToString().Trim();
                        txR2H.Text = ds.Tables[0].Rows[0]["resistance_ts_max"].ToString().Trim();
                        txRated_V.Text = comboBox3 .Text .ToString().Trim();
                        txRated_AL.Text = ds.Tables[0].Rows[0]["full_voltage_current_min"].ToString().Trim();
                        txRated_AH.Text = ds.Tables[0].Rows[0]["full_voltage_current_max"].ToString().Trim();
                        txRated_PL.Text = ds.Tables[0].Rows[0]["full_voltage_power_min"].ToString().Trim();
                        txRated_PH.Text = ds.Tables[0].Rows[0]["full_voltage_power_max"].ToString().Trim();
                        //MotorModel.Run_Time = ds.Tables[0].Rows[0]["跑合时间"].ToString().Trim());
                        txRated_60V.Text  = (float .Parse(txRated_V .Text )*0.6).ToString ();
                        txRated_60A_L.Text = ds.Tables[0].Rows[0]["decreased_voltage_current_min"].ToString().Trim();
                        txRated_60A_H.Text = ds.Tables[0].Rows[0]["decreased_voltage_current_max"].ToString().Trim();
                        txRated_60P_L.Text = ds.Tables[0].Rows[0]["decreased_voltage_power_min"].ToString().Trim();
                        txRated_60P_H.Text = ds.Tables[0].Rows[0]["decreased_voltage_power_max"].ToString().Trim();
                        //MotorModel.Rated_60pTimer = ds.Tables[0].Rows[0]["时间60p"].ToString().Trim());
                        //MotorModel.WithStand_V = ds.Tables[0].Rows[0]["耐压电压"].ToString().Trim());
                        //MotorModel.Leak_AL = ds.Tables[0].Rows[0]["泄露电流下限"].ToString().Trim());
                        //MotorModel.WithStandV_Time = ds.Tables[0].Rows[0]["耐压时间"].ToString().Trim());
                        //MotorModel.Spare_temperL = ds.Tables[0].Rows[0]["备用温度下限"].ToString().Trim());
                        //MotorModel.Spare_temperH = ds.Tables[0].Rows[0]["备用温度上限"].ToString().Trim());
                        //MotorModel.Spare_temper = ds.Tables[0].Rows[0]["备用温度值"].ToString().Trim());
                        //tx绝缘电阻本底值.Text = ds.Tables[0].Rows[0]["绝缘电阻本底值"].ToString().Trim();
                        //tx制动器转矩本底.Text = ds.Tables[0].Rows[0]["制动转矩本底值"].ToString().Trim();
                        //tx本底泄露电流.Text = ds.Tables[0].Rows[0]["泄露电流本底值"].ToString().Trim();
                        //Send_TH2516(":TEMP:CORR:PAR " + MotorModel.StandardTemper.ToString().Trim() + ",3390");
                    }
                    da.Dispose();
                    ds.Dispose();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)      //型号选择
        {
            try
            {
                times04 += 1;
                if (GlobalData.mysql.State == ConnectionState.Open && times04 >= 1)
                {
                    string sqlcmd = "select *from brake_routine_standard_torque  where brake_routine_standard_id='" + comboBox2.Text + "' and rated_torque='" + comboBox4.Text + "'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd, GlobalData.mysql);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txbrake_NL.Text = ds.Tables[0].Rows[0]["brake_torque_min"].ToString().Trim();          //制动转矩 Min
                        txbrake_NH.Text = ds.Tables[0].Rows[0]["brake_torque_max"].ToString().Trim();          //制动转矩 Max
                        txRun_NL.Text = ds.Tables[0].Rows[0]["grinding_torque_min"].ToString().Trim();         //磨合转矩 Min
                        txRun_NH.Text = ds.Tables[0].Rows[0]["grinding_torque_max"].ToString().Trim();         //磨合转矩 Max
                    }
                    ds.Dispose();
                    da.Dispose();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void 添加型号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cbModel.Visible = false;
            cbModel.Enabled = false;
            txaddModel.Visible = true;
            txaddModel.Visible = true;
            label30.Visible = true;
            //btnsaveModel.Visible = true;
            foreach (Control c in this.Controls )
            {
                if(c is TextBox )
                {
                    c.Text = "";
                }
            }
            AddModel = true ;
        }
        private void UpdataModel()
        {
            bool textbox_ok = false;
            try
            {
                foreach (Control textbox in panel1.Controls)
                {
                    if (textbox is TextBox)
                    {
                        if (string.IsNullOrEmpty((textbox as TextBox).Text))
                        {
                            MessageBox.Show("参数填写不完整,请重新填写！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textbox_ok = false;
                            break;
                        }
                        else if (!float.TryParse((textbox as TextBox).Text, out float nump))
                        {
                            MessageBox.Show("参数填写格式错误,请重新填写！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textbox_ok = false;
                            break;
                        }
                        else
                        {
                            textbox_ok = true;
                        }
                    }
                }
                if (textbox_ok == true)
                {
                    if (GlobalData.mysql.State != ConnectionState.Open)
                    {
                        GlobalData.mysql.Open();
                    }
                    string sqlcmd = "UPDATE brake_routine_standard_detail " +
                        "SET resistance_bs_min='" + txR1L.Text.Trim() + "'," +
                        "resistance_bs_max='" + txR1H.Text.Trim() + "'," +
                        "resistance_ts_min='" + txR2L.Text.Trim() + "'," +
                        "resistance_ts_max='" + txR2H.Text.Trim() + "'," +
                        "full_voltage_current_min='" + txRated_AL.Text.Trim() + "'," +
                        "full_voltage_current_max='" + txRated_AH.Text.Trim() + "'," +
                        "full_voltage_power_min='" + txRated_PL.Text.Trim() + "'," +
                        "full_voltage_power_max='" + txRated_PH.Text.Trim() + "'," +
                        "decreased_voltage_current_min='" + txRated_60A_L.Text.Trim() + "'," +
                        "decreased_voltage_current_max='" + txRated_60A_H.Text.Trim() + "'," +
                        "decreased_voltage_power_min='" + txRated_60P_L.Text.Trim() + "'," +
                        "decreased_voltage_power_max='" + txRated_60P_H.Text.Trim() + "'" +
                        "where brake_routine_standard_id=" + "'"+comboBox2 .Text+ "'and rated_voltage='" + comboBox3.Text +"'";           
                    SqlCommand myCom = new SqlCommand(sqlcmd, GlobalData.mysql);
                    myCom.ExecuteNonQuery();
                    sqlcmd = "UPDATE brake_routine_standard SET" +
                        "  resistance_duration='" + txR_tstime.Text.Trim() + "'," +
                        "reference_temperature='" + txts_temper.Text.Trim() + "'," +
                        "full_voltage_time='" + txRated_Time.Text.Trim() + "'," +
                        "grinding_speed='" + txRun_ver.Text.Trim() + "'," +
                        "residual_torque_max='" + txRemain_NH.Text.Trim() + "'," +
                        "decreased_voltage_time='" + txRated_60Time.Text.Trim() + "'," +
                        "insulation_resistance_min='" + txIR_L.Text.Trim() + "'," +
                        "with_stand_voltage='" + txpuncture_V.Text.Trim() + "'," +
                        "leakage_current_max='" + txleak_AH.Text.Trim() + "'," +
                        "with_stand_duration='" + txpuncture_Time.Text.Trim() + "'" +
                        "where identifier=" + "'" + cbModel.Text + "'";
                    myCom = new SqlCommand(sqlcmd, GlobalData.mysql);
                    myCom.ExecuteNonQuery();
                    sqlcmd = "UPDATE brake_routine_standard_torque SET " +
                        "grinding_torque_min='" + txRun_NL.Text.Trim() + "'," +
                        "grinding_torque_max='" + txRun_NH.Text.Trim() + "'," +
                        "brake_torque_min='" + txbrake_NL.Text.Trim() + "'," +
                        "brake_torque_max='" + txbrake_NH.Text.Trim() + "'" +
                        "where brake_routine_standard_id=" + "'" + comboBox2 .Text + "'and rated_torque='" +comboBox4.Text + "'";
                    myCom = new SqlCommand(sqlcmd, GlobalData.mysql);
                    int Result = (int)myCom.ExecuteNonQuery();
                    if (Result > 0)
                    {
                        MessageBox.Show("保存数据成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("保存数据失败！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    myCom.Dispose();
                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }
        }
        private void InsertModel()
        {
            bool textbox_ok = false;
            try
            {
                foreach (Control textbox in this.Controls)
                {
                    if (textbox is TextBox)
                    {
                        if (string.IsNullOrEmpty((textbox as TextBox).Text))
                        {
                            MessageBox.Show("参数填写不完整,请重新填写！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textbox_ok = false;
                            break;
                        }
                        else if (!float.TryParse((textbox as TextBox).Text, out float nump))
                        {
                            MessageBox.Show("参数填写格式错误,请重新填写！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textbox_ok = false;
                            break;
                        }
                        else
                        {
                            textbox_ok = true;
                        }
                    }
                }
                if (textbox_ok == true)
                {
                    if (GlobalData.mysql.State != ConnectionState.Open)
                    {
                        GlobalData.mysql.Open();
                    }
                    string sqlcmd = "insert into  brake_routine_standard " +
                        "(identifier," +
                        "resistance_duration," +
                        "reference_temperature," +
                        "resistance_coefficient," +
                        "insulation_resistance_duration," +
                        "insulation_resistance_voltage," +
                        "insulation_resistance_min," +
                        "with_stand_duration," +
                        "with_stand_voltage," +
                        "leakage_current_max," +
                        "full_voltage_time," +
                        "decreased_voltage_time," +
                        "decreased_voltage_ratio," +
                        "grinding_speed," +
                        "grinding_time_min," +
                        "grinding_time_max," +
                        "residual_torque_max) " +
                        "values('" + txaddModel.Text.Trim () + "'," +
                        "'" + txR_tstime.Text.Trim() + "'," +
                        "'" + txts_temper .Text.Trim() + "'," +
                        "'" + textBox1.Text.Trim() + "'," +
                        "'" + textBox2.Text.Trim() + "'," +
                        "'" + txrmvoltage.Text.Trim() + "'," +
                        "'" + txIR_L.Text.Trim() + "'," +
                        "'" + txpuncture_Time.Text.Trim() + "'," +
                        "'" + txpuncture_V.Text.Trim() + "'," +
                        "'" + txleak_AH.Text.Trim() + "'," +
                        "'" + txRated_Time.Text.Trim() + "'," +
                        "'" + txRated_60Time.Text.Trim() + "'," +
                        "'" + textBox4.Text.Trim() + "'," +
                        "'" + txRun_ver.Text.Trim() + "'," +
                        "'" + txRun_Time.Text.Trim() + "'," +
                        "'" + textBox3.Text.Trim() + "'," +
                        "'" + txRemain_NH.Text.Trim() +  "')";
                    SqlCommand myCom = new SqlCommand(sqlcmd, GlobalData.mysql);
                    int Result = (int)myCom.ExecuteNonQuery();
                    if (Result > 0)
                    {
                        MessageBox.Show("添加型号成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("添加型号失败！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    myCom.Dispose();
                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }
        }
        private void 保存参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AddModel == true)
            {
                InsertModel();
                AddModel = false;
            }
            if (AddModel == false)
            {
                UpdataModel();
            }
        }
        #region 校正按钮
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "update 本底值 SET 泄露电流='" + textBox6.Text.Trim() + "',绝缘电阻='" + textBox7.Text.Trim() + "',转矩='" + textBox5.Text.Trim() + "' where ID='1'";
            SqlCommand mycom = new SqlCommand(sql, GlobalData.mysql);
            if (mycom.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("保存成功！！");
            }
            else
            {
                MessageBox.Show("保存失败！！");
            }
            mycom.Dispose();
        }
        #endregion

        private void  LoadPlcParameter()
        {
            txt_IntervelLeftMin.Text = Equipment_Device.ReadPLCfloat(GlobalData.PLCIO[157]);
            txt_IntervelLeftMax.Text = Equipment_Device.ReadPLCfloat(GlobalData.PLCIO[158]);

            txt_IntervelRightMin.Text = Equipment_Device.ReadPLCfloat(GlobalData.PLCIO[159]);
            txt_IntervelRightMax.Text = Equipment_Device.ReadPLCfloat(GlobalData.PLCIO[160]);

            textBox左间隙补偿.Text = Equipment_Device.ReadPLCfloat(GlobalData.PLCIO[161]);
            textBox右间隙补偿.Text = Equipment_Device.ReadPLCfloat(GlobalData.PLCIO[162]);
        }
        private void buttonSaveIntervel_Click(object sender, EventArgs e)
        {
            #region
            try
            {
                Equipment_Device.WritePLCFloat(GlobalData.PLCIO[157], float.Parse(txt_IntervelLeftMin.Text));
                Equipment_Device.WritePLCFloat(GlobalData.PLCIO[158], float.Parse(txt_IntervelLeftMax.Text));

                Equipment_Device.WritePLCFloat(GlobalData.PLCIO[159], float.Parse(txt_IntervelRightMin.Text));
                Equipment_Device.WritePLCFloat(GlobalData.PLCIO[160], float.Parse(txt_IntervelRightMax.Text));

                Equipment_Device.WritePLCFloat(GlobalData.PLCIO[161], float.Parse(textBox左间隙补偿.Text));
                Equipment_Device.WritePLCFloat(GlobalData.PLCIO[162], float.Parse(textBox右间隙补偿.Text));
            }
            catch
            {

            }

            #endregion
        }

        private void button跑合时间设置_Click(object sender, EventArgs e)
        {
            Equipment_Device.WritePLCInt(GlobalData.PLCIO[60], Convert.ToInt32(textBox跑合时间设置.Text.Trim()) * 1000);
        }

        private void button制动时间设置_Click(object sender, EventArgs e)
        {
            Equipment_Device.WritePLCInt(GlobalData.PLCIO[62], Convert.ToInt32(textBox制动时间设置.Text.Trim()) * 1000);
        }

        private void button残留时间设置_Click(object sender, EventArgs e)
        {
            Equipment_Device.WritePLCInt(GlobalData.PLCIO[64], Convert.ToInt32(textBox残留时间设置.Text.Trim()) * 1000);
        }

        private void 补偿参数设定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (补偿参数设定 form = new 补偿参数设定())
            {
                form.ShowDialog();
            }
        }
    }
}
