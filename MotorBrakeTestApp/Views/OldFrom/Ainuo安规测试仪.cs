using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
namespace MotorBrakeTestApp
{
    public partial class Ainuo安规测试仪 : Form
    {
        public Ainuo安规测试仪()
        {
            InitializeComponent();
            AiserialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(AiserialPort_DataReceived);
        }
        private void Ainuo安规测试仪_Load(object sender, EventArgs e)
        {
            string[] ArrayPort = SerialPort.GetPortNames();
            for (int i = 0; i < ArrayPort.Length; i++)
            {
                cb_serialport_list.Items.Add(ArrayPort[i]);
            }
            cb_serialport_baud.SelectedIndex = 5;
            cb_Parity.SelectedIndex = 0;
            DLINK.Enabled = false;
            Link.Enabled = true;
            button1.Enabled = false;
          
        }

        private void Link_Click(object sender, EventArgs e)
        {
            if(!int.TryParse (tx_databyte.Text ,out int datebits))
            {
                MessageBox.Show("数据位输入格式错误!!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if(!int.TryParse (tx_StopByte .Text ,out int stopbits))
            {
                MessageBox.Show("停止位输入格式错误！！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            try
            {
                AiserialPort.PortName = cb_serialport_list.Text;
                AiserialPort.BaudRate = Convert.ToInt16(cb_serialport_baud.Text);
                AiserialPort.DataBits = datebits;
                AiserialPort.StopBits = stopbits == 0 ? System.IO.Ports.StopBits.None : (stopbits == 1 ? System.IO.Ports.StopBits.One : System.IO.Ports.StopBits.Two);
                AiserialPort.Parity = cb_Parity.SelectedIndex == 0 ? System.IO.Ports.Parity.None : (cb_Parity.SelectedIndex == 1 ? System.IO.Ports.Parity.Even : System.IO.Ports.Parity.Odd);
                AiserialPort.Open();
                Link.Enabled = false;
                cb_serialport_list.Enabled = false;
                cb_serialport_baud.Enabled = false;
                cb_Parity.Enabled = false;
                tx_databyte.Enabled = false;
                tx_StopByte.Enabled = false;
                button1.Enabled = true;
            }
            catch(Exception  ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void DLINK_Click(object sender, EventArgs e)
        {
            AiserialPort.Close();
            DLINK.Enabled = false;
            Link.Enabled = true;
            cb_serialport_list.Enabled = true ;
            cb_serialport_baud.Enabled = true;
            cb_Parity.Enabled = true;
            tx_databyte.Enabled = true;
            tx_StopByte.Enabled = true;
            button1.Enabled = false;
        }
        #region 读取设备信息
        private void SendData_PC(byte StationID)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            byte[] PC_SendData = new byte[8];
            PC_SendData[0] = 0x7B;
            PC_SendData[1] = 0X00;
            PC_SendData[2] = 0X08;
            PC_SendData[3] = StationID;
            PC_SendData[4] = 0xF0;
            PC_SendData[5] = 0X03;
            PC_SendData[7] = 0X7D;
            PC_SendData[6] = (byte )(PC_SendData[1] + PC_SendData[2] + PC_SendData[3] + PC_SendData[4] + PC_SendData[5]);
            PC_SendData[6] = (byte )(PC_SendData [6]&0xFF);
            try
            {
                AiserialPort.Write(PC_SendData, 0, 8);
                MessageBox.Show("发送数据成功！！","提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void AiserialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] PC_GetData = new byte[AiserialPort.BytesToRead];
            AiserialPort.Read(PC_GetData, 0, PC_GetData.Length);
            ProcessGetData(PC_GetData);
        }
        private void ProcessGetData(byte[] data )
        {
            byte CheckSum;
            if (data.Length == 10 && data[0] == 0x7B && data[9] == 7D)
            {
                CheckSum = (byte)(data[1] + data[2] + data[3] + data[4] + data[5] + data[6] + data[7]);
                CheckSum = (byte)(CheckSum & 0XFF);
                if (CheckSum == data[8])
                {
                    textBox1.Text = "仪器型号:" + Convert.ToInt64(data[6]).ToString("X") + Convert.ToInt64(data[7]).ToString("X");
                }
            }
        }

        private void Ainuo安规测试仪_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(AiserialPort .IsOpen )
            {
                AiserialPort.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!byte.TryParse(tx_StationID.Text, out byte station))
            {
                MessageBox.Show("站号输入格式错误！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SendData_PC(station);
        }
        #endregion
    }
}
