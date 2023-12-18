using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using HslCommunication.ModBus;
using HslCommunication;
namespace MotorBrakeTestApp
{
    public partial class Modbus_RTU : Form
    {
        public Modbus_RTU()
        {
            InitializeComponent();
        }
        private ModbusRtu busRtuClient = null;
        private void Modbus_RTU_Load(object sender, EventArgs e)
        {
            string[] ArrayPort = SerialPort.GetPortNames();
            for (int i=0;i<ArrayPort.Length;i++)
            {
                cb_serialport_list.Items.Add(ArrayPort[i]);
            }
            Link.Enabled = true;
            DLINK.Enabled = false;
            cb_Parity.SelectedIndex = 0;
            cb_serialport_baud.SelectedIndex = 5;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (busRtuClient != null)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.ABCD; break;
                    case 1: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.BADC; break;
                    case 2: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.CDAB; break;
                    case 3: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.DCBA; break;
                    default: break;
                }
            }
        }
        private void Link_Click(object sender, EventArgs e)
        {
            if(!int.TryParse(tx_databyte .Text ,out int databits) )
            {
                MessageBox.Show("数据位输入错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(tx_StopByte.Text, out int stopbits))
            {
                MessageBox.Show("停止位输入错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!byte.TryParse (tx_StationID .Text ,out byte stationID))
            {
                MessageBox.Show("设备站号输入错误","错误",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                return;
            }
            busRtuClient?.Close();
            busRtuClient = new ModbusRtu(stationID);
            busRtuClient.AddressStartWithZero = checkBox1.Checked;
            ComboBox2_SelectedIndexChanged(null, new EventArgs());
            busRtuClient.IsStringReverse = checkBox3.Checked;
            try
            {
                busRtuClient.SerialPortInni(sp =>
              {
                  sp.PortName = cb_serialport_list.Text;
                  sp.BaudRate = Convert.ToInt16(cb_serialport_baud.Text);
                  sp.DataBits = Convert .ToInt16 ( tx_databyte.Text);
                  sp.StopBits =stopbits == 0 ? System.IO.Ports.StopBits.None : (stopbits ==1?System .IO.Ports .StopBits .One :System .IO.Ports .StopBits .Two );
                  sp.Parity = cb_Parity.SelectedIndex == 0 ? System.IO.Ports.Parity.None : (cb_Parity .SelectedIndex ==1?System .IO.Ports .Parity .Even:System.IO.Ports.Parity .Odd );
              });
                busRtuClient.Open();
                Link.Enabled = false;
                DLINK .Enabled = true;
                cb_serialport_list.Enabled = false;
                cb_serialport_baud.Enabled = false;
                cb_Parity.Enabled = false;
                tx_databyte.Enabled = false;
                tx_StopByte.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DLINK_Click(object sender, EventArgs e)
        {
            busRtuClient.Close();
            Link.Enabled = true;
            DLINK.Enabled = false;
        }

        private void Modbus_RTU_FormClosing(object sender, FormClosingEventArgs e)
        {
            //busRtuClient.Close();

        }
        #region 读取数据处理函数
        private void ReadResultRender<T>(OperateResult<T> result, string address, TextBox textbox)
        {
            if(result .IsSuccess )
            {
                textbox.AppendText(DateTime .Now .ToString ("[HH:mm:ss]")+$"[{address }]+{result .Content }{Environment .NewLine }");
            }
            else
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss]")+ $"[{address }] 读取失败{Environment.NewLine }");
            }
        }
        #endregion
        #region 写入数据处理函数
        private void WriteResultRender(OperateResult result ,string address)
        {
            if(result.IsSuccess )
            {
                MessageBox.Show($"写入{address}成功！！");
            }
            else
            {
                MessageBox.Show($"写入{address}失败！！");
            }
        }
        #endregion
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cb_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region 单个寄存器读取
        private void Bool_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient.ReadCoil(txAddress.Text), txAddress.Text, txResult);
        }

        private void Byte_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadDiscrete (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void Short_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadInt16 (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void ushort_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadUInt16 (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void Int32_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadInt32 (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void uInt32_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient.ReadUInt32(txAddress.Text), txAddress.Text, txResult);
        }

        private void Int64_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadInt64 (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void uInt64_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadUInt64 (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void Float_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadFloat (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void Double_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadDouble (txAddress .Text ),txAddress .Text ,txResult );
        }

        private void String_Read_Click(object sender, EventArgs e)
        {
            ReadResultRender(busRtuClient .ReadString (txAddress .Text,ushort .Parse (txstr_leth .Text ) ),txAddress .Text ,txResult );
        }
        #endregion
        #region 批量读取
        private void  button1_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult<byte[]> read = busRtuClient.Read(Moreaddress .Text ,ushort .Parse (ReadLeth .Text ));
                if (read.IsSuccess )
                {
                    MoreReadResult.Text = "结果" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString(read.Content);
                }
                else
                {
                    MessageBox.Show("读取失败"+read.ToMessageShowString ());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取失败:"+ex.Message+"!!");
            }
        }
        #endregion
        #region 写入数据
        private void bool_Write_Click(object sender, EventArgs e)
        {
            try
            {
              //  WriteResultRender(busRtuClient.WriteCoil(txAddress.Text, bool.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }           
        }

        private void short_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, short.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ushort_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, ushort.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Int_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, int.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void uInt_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, uint.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void float_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, float.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Long_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, long.Parse(txWirteData.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void uLong_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, ulong.Parse(txWriteAD.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void double_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, double.Parse(txWriteAD.Text)), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void String_Write_Click(object sender, EventArgs e)
        {
            try
            {
                WriteResultRender(busRtuClient.Write(txAddress.Text, txWriteAD.Text), txAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(busRtuClient !=null )
            {
                busRtuClient.IsStringReverse = checkBox3.Checked;
            }
        }

    }
}
