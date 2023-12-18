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
    public partial class 直流电阻测试仪 : Form
    {
        public 直流电阻测试仪()
        {
            InitializeComponent();
            DCR_serialPort.DataReceived += new SerialDataReceivedEventHandler(DCR_serialPort_DataReceived);
        }
        // public string PC_GetString = null;
        private void 直流电阻测试仪_Load(object sender, EventArgs e)
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
            if (!int.TryParse(tx_databyte.Text, out int datebits))
            {
                MessageBox.Show("数据位输入格式错误!!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!int.TryParse(tx_StopByte.Text, out int stopbits))
            {
                MessageBox.Show("停止位输入格式错误！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                DCR_serialPort.PortName = cb_serialport_list.Text;
                DCR_serialPort.BaudRate = Convert.ToInt16(cb_serialport_baud.Text);
                DCR_serialPort.DataBits = datebits;
                DCR_serialPort.StopBits = stopbits == 0 ? System.IO.Ports.StopBits.None : (stopbits == 1 ? System.IO.Ports.StopBits.One : System.IO.Ports.StopBits.Two);
                DCR_serialPort.Parity = cb_Parity.SelectedIndex == 0 ? System.IO.Ports.Parity.None : (cb_Parity.SelectedIndex == 1 ? System.IO.Ports.Parity.Even : System.IO.Ports.Parity.Odd);
                DCR_serialPort.Open();
                Link.Enabled = false;
                cb_serialport_list.Enabled = false;
                cb_serialport_baud.Enabled = false;
                cb_Parity.Enabled = false;
                tx_databyte.Enabled = false;
                tx_StopByte.Enabled = false;
                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DLINK_Click(object sender, EventArgs e)
        {
            DCR_serialPort.Close();
            DLINK.Enabled = false;
            Link.Enabled = true;
            cb_serialport_list.Enabled = true;
            cb_serialport_baud.Enabled = true;
            cb_Parity.Enabled = true;
            tx_databyte.Enabled = true;
            tx_StopByte.Enabled = true;
            button1.Enabled = false;
        }
        private void SendData_PC()
        {
            byte[] PC_SendString = { 0x2a, 0x69, 0x64, 0x6e, 0x3f, 0x0d, 0x0a };
            try
            {
                DCR_serialPort.Write(PC_SendString, 0, PC_SendString.Length);
                MessageBox.Show("发送数据成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DCR_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string PC_GetString = "";
            Control.CheckForIllegalCrossThreadCalls = false;
            byte[] buff = new byte[DCR_serialPort.BytesToRead];
            DCR_serialPort.Read(buff, 0, buff.Length);
            foreach (byte buf in buff)
            {
                PC_GetString += Convert.ToChar(buf);
            }
            string[] Array = PC_GetString.Split(',');
            for (int i = 0; i < Array.Length; i++)
            {
                textBox1.AppendText(Array[i] + Environment.NewLine);
            }
            //textBox1.AppendText( PC_GetString);

        }
        private void 直流电阻测试仪_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DCR_serialPort.IsOpen)
            {
                DCR_serialPort.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendData_PC();
        }
    }
}
