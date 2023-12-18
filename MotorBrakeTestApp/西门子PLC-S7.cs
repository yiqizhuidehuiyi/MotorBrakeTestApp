using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HslCommunication.Profinet.Siemens;
using System.Threading;
using HslCommunication;
using HslCommunication.Profinet;
namespace MotorBrakeTestApp
{
    public partial class 西门子PLC_S7 : Form
    {
        public 西门子PLC_S7(SiemensPLCS siemensPLCS)
        {
            InitializeComponent();
            siemensTcpNet = new SiemensS7Net(siemensPLCS);
        }
        private SiemensS7Net siemensTcpNet = null;
        private void 西门子PLC_S7_Load(object sender, EventArgs e)
        {
            Link.Enabled = true;
            DLINK.Enabled = false;
            panel2.Enabled = false;
            txPLCPort.Enabled = false;
        }
        private void 西门子PLC_S7_FormClosed(object sender, FormClosedEventArgs e)
        {
            siemensTcpNet.ConnectClose();
            DLINK.Enabled = false;
            Link.Enabled = true;
            panel1.Enabled = true;
        }
        private void ReadResultRender<T>(OperateResult<T> result, string address, TextBox textBox)
        {
            if (result.IsSuccess)
            {
                textBox.AppendText(DateTime.Now.ToString("[HH:mm:ss]") + $"[{address}] {result.Content}{Environment.NewLine}");
            }
            else
            {
                textBox.AppendText(DateTime.Now.ToString("[HH:mm:ss]") + $"[{address}] 读取错误 {Environment.NewLine}");
            }
        }
        private void WriteResultRender(OperateResult result, string address)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入成功");
            }
            else
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }
        #region 连接和断开
        private void button1_Click(object sender, EventArgs e)
        {
            Link.Enabled = false;
            if (!System.Net.IPAddress.TryParse(txPLCIP.Text, out System.Net.IPAddress address))
            {
                MessageBox.Show("IP输入不正确！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Link.Enabled = true;
            }
            siemensTcpNet.IpAddress = txPLCIP.Text;
            siemensTcpNet.Port = 502;
            // siemensTcpNet.Port = System .Convert .ToUInt16 ( txPLCPort.Text);
            try
            {
                OperateResult connect = siemensTcpNet.ConnectServer();
                if (connect.IsSuccess)
                {
                    MessageBox.Show("连接成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Link.Enabled = false;
                    DLINK.Enabled = true;
                    //panel1.Enabled = false;
                    panel2.Enabled = true;
                }
                else
                {
                    MessageBox.Show("连接失败！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Link.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DLINK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定断开连接？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                siemensTcpNet.ConnectClose();
                DLINK.Enabled = false;
                Link.Enabled = true;
                panel1.Enabled = true;
                panel2.Enabled = false;
            }
        }
        #endregion

        #region 单数据读取
        private void button1_Click_1(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadBool(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txResult);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadByte(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txResult);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadInt16(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txResult);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadUInt16(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void Int32_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadInt32(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void uInt32_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadUInt32(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void Int64_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadInt64(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void uInt64_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadUInt64(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void Float_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadFloat(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void String_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadString(comboBox1.Text + txAddress.Text, ushort.Parse(txstr_leth.Text)), comboBox1.Text + txAddress.Text, txAddress);
        }

        private void Double_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadDouble(comboBox1.Text + txAddress.Text), comboBox1.Text + txAddress.Text, txAddress);
        }
        #endregion
        #region 批量读取寄存器
        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                OperateResult<byte[]> read = siemensTcpNet.Read(comboBox2.Text + Moreaddress.Text, ushort.Parse(ReadLeth.Text));
                if (read.IsSuccess)
                {
                    MoreReadResult.Text = "结果:" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString(read.Content);
                }
                else
                {
                    MessageBox.Show("读取失败！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region 单寄存器写入
        private void bool_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, bool.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void byte_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, byte.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void short_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, short.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ushort_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, ushort.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Int_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, int.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uInt_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, uint.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Long_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, long.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uLong_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, ulong.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void float_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, float.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void double_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, double.Parse(txWirteData.Text)), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void String_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(siemensTcpNet.Write(comboBox4.Text + txWriteAD.Text, txWirteData.Text), comboBox4.Text + txWriteAD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region 定时读数据
        private void TimeRead()
        {

        }
        #endregion 
    }
}
