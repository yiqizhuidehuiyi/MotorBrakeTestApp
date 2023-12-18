using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HslCommunication.ModBus;
using System.IO.Ports;
using HslCommunication.Profinet;
using HslCommunication;
namespace MotorBrakeTestApp
{
    public partial class ModbusTCP : Form
    {
        public ModbusTCP()
        {
            InitializeComponent();
        }
        private ModbusTcpNet busTcpClint = null;
        private void ModbusTCP_Load(object sender, EventArgs e)
        {

        }
        #region 读取寄存器处理函数
        private void ReadResultRender<T>(OperateResult<T> result, String address, TextBox textBox)
        {
            if (result.IsSuccess)
            {
                textBox.AppendText(DateTime.Now.ToString("[HH:mm:ss]" + $"[{address}]+{result.Content}{Environment.NewLine}"));
            }
            else
            {
                MessageBox.Show($"读取{address}错误！！");
            }

        }
        #endregion
        private void WriteResultRender(OperateResult result, string address)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show("写入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"写入{address}+失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Link_Click(object sender, EventArgs e)
        {
            if (!System.Net.IPAddress.TryParse(txPLCIP.Text, out System.Net.IPAddress address))
            {
                MessageBox.Show("IP地址输入格式错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txPLCPort.Text, out int Port))
            {
                MessageBox.Show("端口输入格式错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!byte.TryParse(tx_StationID.Text, out byte station))
            {
                MessageBox.Show("站号输入格式错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            busTcpClint?.ConnectClose();
            busTcpClint = new ModbusTcpNet(txPLCIP.Text, Port, station);
            busTcpClint.AddressStartWithZero = checkBox1.Checked;
            comboBox2_SelectedIndexChanged(null, new EventArgs());
            busTcpClint.IsStringReverse = checkBox3.Checked;
            try
            {
                OperateResult connect = busTcpClint.ConnectServer();
                if (connect.IsSuccess)
                {
                    MessageBox.Show("连接成功！！");
                    Link.Enabled = false;
                    DLINK.Enabled = true;
                }
                else
                {
                    MessageBox.Show("连接失败！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (busTcpClint != null)
            {
                busTcpClint.IsStringReverse = checkBox3.Checked;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (busTcpClint != null)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0: busTcpClint.DataFormat = HslCommunication.Core.DataFormat.ABCD; break;
                    case 1: busTcpClint.DataFormat = HslCommunication.Core.DataFormat.BADC; break;
                    case 2: busTcpClint.DataFormat = HslCommunication.Core.DataFormat.CDAB; break;
                    case 3: busTcpClint.DataFormat = HslCommunication.Core.DataFormat.DCBA; break;
                    default: break;
                }
            }
        }

        private void DLINK_Click(object sender, EventArgs e)
        {
            busTcpClint.ConnectClose();
            Link.Enabled = true;
        }
        #region 读取单个寄存器
        private void Bool_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadCoil(txAddress.Text), txAddress.Text, txResult);
        }

        private void Byte_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadDiscrete(txAddress.Text), txAddress.Text, txResult);
        }

        private void Short_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadInt16(txAddress.Text), txAddress.Text, txResult);
        }

        private void ushort_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadUInt16(txAddress.Text), txAddress.Text, txResult);
        }

        private void Int32_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadInt32(txAddress.Text), txAddress.Text, txResult);
        }

        private void uInt32_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadUInt32(txAddress.Text), txAddress.Text, txResult);
        }

        private void Int64_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadUInt16(txAddress.Text), txAddress.Text, txResult);
        }

        private void uInt64_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadUInt64(txAddress.Text), txAddress.Text, txResult);
        }

        private void Float_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadFloat(txAddress.Text), txAddress.Text, txResult);
        }

        private void Double_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadDouble(txAddress.Text), txAddress.Text, txResult);
        }

        private void String_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busTcpClint.ReadString(txAddress.Text, ushort.Parse(txstr_leth.Text)), txAddress.Text, txResult);
        }
        #endregion
        #region 读取多个寄存器
        private void button1_Click(object sender, EventArgs e)
        {
            OperateResult<byte[]> read = busTcpClint.Read(Moreaddress.Text, ushort.Parse(ReadLeth.Text));
            if (read.IsSuccess)
            {
                MoreReadResult.Text = "结果" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString(read.Content);
            }
            else
            {
                MessageBox.Show($"读取{Moreaddress.Text}+{Convert.ToInt16(Moreaddress.Text) + Convert.ToInt16(ReadLeth.Text)}+错误！！");
            }
        }

        #endregion
        #region 写入单个数据
        private void bool_Write_Click(object sender, EventArgs e)
        {
            //布尔型数据
            try
            {
                //  WriteResultRender(busTcpClint.WriteCoil(txWirteData.Text, bool.Parse(txWriteAD.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void short_Write_Click(object sender, EventArgs e)
        {
            //有符号short型数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, short.Parse(txWriteAD.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ushort_Write_Click(object sender, EventArgs e)
        {
            //无符号short型数据
            try
            {
                WriteResultRender(busTcpClint.WriteOneRegister(txWriteAD.Text, ushort.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Int_Write_Click(object sender, EventArgs e)
        {
            //有符号整型
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, int.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uInt_Write_Click(object sender, EventArgs e)
        {
            //无符号整型数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, uint.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void float_Write_Click(object sender, EventArgs e)
        {
            //浮点型数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, float.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Long_Write_Click(object sender, EventArgs e)
        {
            //有符号长整型数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, long.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uLong_Write_Click(object sender, EventArgs e)
        {
            //无符号长整型数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, ulong.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void double_Write_Click(object sender, EventArgs e)
        {
            //双字型数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, double.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void String_Write_Click(object sender, EventArgs e)
        {
            //字符串数据
            try
            {
                WriteResultRender(busTcpClint.Write(txAddress.Text, txWirteData.Text), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
