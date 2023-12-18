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
    public partial class PLC读取地址设定 : Form
    {
        public PLC读取地址设定()
        {
            InitializeComponent();
        }
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //#region 操作配置文件
        private void button1_Click(object sender, EventArgs e)
        {
            string []  labeltext=new string [100];
            string[] textbox = new string [100]; 
            Label Label=new Label ();
            for(int i=55;i<=83;i++)
            {
                foreach (Control label in this.Controls )
                {
                    if(label is Label && label .Name =="label"+i.ToString() )
                    {
                        labeltext[i] = label.Text;
                    }
                }
            }
            for(int j=55;j<=83;j++)
            {
                foreach (Control text in this .Controls )
                {
                    if(text is TextBox && text.Name =="textBox"+j.ToString())
                    {
                        textbox[j] = text.Text;
                    }
                }
            }
            for (int K = 55; K <= 83; K++)
            {
                config.AppSettings.Settings.Add(labeltext[K], textbox[K]);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] labeltext = new string[100];
            //string[] textbox = new string[100];
            Label Label = new Label();
            for (int i = 1; i <= 83; i++)
            {
                foreach (Control label in this.Controls)
                {
                    if (label is Label && label.Name == "label" + i.ToString())
                    {
                        labeltext[i] = label.Text;
                    }
                }
            }
            for (int j = 1; j <= 83; j++)
            {
                foreach (Control text in this.Controls)
                {
                    if (text is TextBox && text.Name == "textBox" + j.ToString())
                    {
                        GlobalData.PLCIO[j+16] = text.Text;
                    }
                }
            }
            for (int K = 1; K <= 83; K++)
            {
                config.AppSettings.Settings[labeltext[K]].Value = GlobalData.PLCIO[K+16];
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            config.AppSettings.Settings["半功率运行时间"].Value = GlobalData.PLCIO[128];
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void PLC读取地址设定_Load(object sender, EventArgs e)
        {
            this.Width = 1300;
            this.Height = 680;
            GlobalData.ReadPLCIO();
            for (int j = 1; j <= 83; j++)
            {
                foreach (Control text in this.Controls)
                {
                    if (text is TextBox && text.Name == "textBox" + j.ToString())
                    {
                        text.Text = GlobalData.PLCIO[j+16];
                    }
                }
            }
            textBox84.Text = GlobalData.PLCIO[128];
            if (GlobalData.RemoteSetAinuo=="1") checkBox1.Checked = true;
            else checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1 .Checked )
            {
                GlobalData.RemoteSetAinuo = "1";
                config.AppSettings.Settings["远程设置安规"].Value = "1";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                GlobalData.RemoteSetAinuo = "0" ;
                config.AppSettings.Settings["远程设置安规"].Value = "0";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}
