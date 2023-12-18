namespace MotorBrakeTestApp
{
    partial class ModbusTCP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txstr_leth = new System.Windows.Forms.TextBox();
            this.ushort_Read = new System.Windows.Forms.Button();
            this.Byte_Read = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txAddress = new System.Windows.Forms.TextBox();
            this.txResult = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ushort_Write = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txWriteAD = new System.Windows.Forms.TextBox();
            this.txWirteData = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ReadLeth = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MoreReadResult = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Moreaddress = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.Link = new System.Windows.Forms.Button();
            this.DLINK = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tx_StationID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txPLCPort = new System.Windows.Forms.TextBox();
            this.Lable4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txPLCIP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txstr_leth);
            this.groupBox4.Controls.Add(this.ushort_Read);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.Byte_Read);
            this.groupBox4.Controls.Add(this.txResult);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txAddress);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(11, 191);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(762, 120);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "单个寄存器读取";
            // 
            // txstr_leth
            // 
            this.txstr_leth.Font = new System.Drawing.Font("楷体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txstr_leth.Location = new System.Drawing.Point(68, 75);
            this.txstr_leth.Margin = new System.Windows.Forms.Padding(2);
            this.txstr_leth.Multiline = true;
            this.txstr_leth.Name = "txstr_leth";
            this.txstr_leth.Size = new System.Drawing.Size(150, 29);
            this.txstr_leth.TabIndex = 45;
            this.txstr_leth.Text = "输入数据长度";
            // 
            // ushort_Read
            // 
            this.ushort_Read.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ushort_Read.Location = new System.Drawing.Point(237, 74);
            this.ushort_Read.Margin = new System.Windows.Forms.Padding(2);
            this.ushort_Read.Name = "ushort_Read";
            this.ushort_Read.Size = new System.Drawing.Size(125, 30);
            this.ushort_Read.TabIndex = 44;
            this.ushort_Read.Text = "读取";
            this.ushort_Read.UseVisualStyleBackColor = true;
            this.ushort_Read.Click += new System.EventHandler(this.ushort_Read_Click);
            // 
            // Byte_Read
            // 
            this.Byte_Read.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Byte_Read.Location = new System.Drawing.Point(237, 32);
            this.Byte_Read.Margin = new System.Windows.Forms.Padding(2);
            this.Byte_Read.Name = "Byte_Read";
            this.Byte_Read.Size = new System.Drawing.Size(125, 30);
            this.Byte_Read.TabIndex = 33;
            this.Byte_Read.Text = "离散输入读取";
            this.Byte_Read.UseVisualStyleBackColor = true;
            this.Byte_Read.Click += new System.EventHandler(this.Byte_Read_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(26, 41);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 17);
            this.label12.TabIndex = 31;
            this.label12.Text = "地址:";
            // 
            // txAddress
            // 
            this.txAddress.Location = new System.Drawing.Point(68, 31);
            this.txAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txAddress.Name = "txAddress";
            this.txAddress.Size = new System.Drawing.Size(150, 29);
            this.txAddress.TabIndex = 30;
            // 
            // txResult
            // 
            this.txResult.Location = new System.Drawing.Point(476, 30);
            this.txResult.Margin = new System.Windows.Forms.Padding(2);
            this.txResult.Multiline = true;
            this.txResult.Name = "txResult";
            this.txResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txResult.Size = new System.Drawing.Size(270, 60);
            this.txResult.TabIndex = 28;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ABCD",
            "BADC",
            "CDAB",
            "DCBA"});
            this.comboBox2.Location = new System.Drawing.Point(501, 77);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(75, 24);
            this.comboBox2.TabIndex = 46;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ushort_Write);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.txWirteData);
            this.groupBox7.Controls.Add(this.txWriteAD);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.Location = new System.Drawing.Point(11, 447);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(762, 100);
            this.groupBox7.TabIndex = 33;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "单个寄存器写入";
            // 
            // ushort_Write
            // 
            this.ushort_Write.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ushort_Write.Location = new System.Drawing.Point(521, 33);
            this.ushort_Write.Margin = new System.Windows.Forms.Padding(2);
            this.ushort_Write.Name = "ushort_Write";
            this.ushort_Write.Size = new System.Drawing.Size(125, 30);
            this.ushort_Write.TabIndex = 44;
            this.ushort_Write.Text = "写入";
            this.ushort_Write.UseVisualStyleBackColor = true;
            this.ushort_Write.Click += new System.EventHandler(this.ushort_Write_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(245, 42);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 17);
            this.label9.TabIndex = 32;
            this.label9.Text = "值:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(17, 42);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 17);
            this.label13.TabIndex = 31;
            this.label13.Text = "地址:";
            // 
            // txWriteAD
            // 
            this.txWriteAD.Location = new System.Drawing.Point(57, 35);
            this.txWriteAD.Margin = new System.Windows.Forms.Padding(2);
            this.txWriteAD.Name = "txWriteAD";
            this.txWriteAD.Size = new System.Drawing.Size(151, 29);
            this.txWriteAD.TabIndex = 30;
            // 
            // txWirteData
            // 
            this.txWirteData.Location = new System.Drawing.Point(275, 35);
            this.txWirteData.Margin = new System.Windows.Forms.Padding(2);
            this.txWirteData.Name = "txWirteData";
            this.txWirteData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txWirteData.Size = new System.Drawing.Size(152, 29);
            this.txWirteData.TabIndex = 28;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.ReadLeth);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.MoreReadResult);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.Moreaddress);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(11, 319);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(762, 120);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "多寄存器读取";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(18, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 51;
            this.label8.Text = "长度:";
            // 
            // ReadLeth
            // 
            this.ReadLeth.Location = new System.Drawing.Point(57, 75);
            this.ReadLeth.Margin = new System.Windows.Forms.Padding(2);
            this.ReadLeth.Name = "ReadLeth";
            this.ReadLeth.Size = new System.Drawing.Size(161, 29);
            this.ReadLeth.TabIndex = 50;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(237, 35);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 30);
            this.button1.TabIndex = 28;
            this.button1.Text = "批量读取";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MoreReadResult
            // 
            this.MoreReadResult.Location = new System.Drawing.Point(476, 30);
            this.MoreReadResult.Margin = new System.Windows.Forms.Padding(2);
            this.MoreReadResult.Multiline = true;
            this.MoreReadResult.Name = "MoreReadResult";
            this.MoreReadResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MoreReadResult.Size = new System.Drawing.Size(270, 60);
            this.MoreReadResult.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 49;
            this.label5.Text = "地址:";
            // 
            // Moreaddress
            // 
            this.Moreaddress.Location = new System.Drawing.Point(57, 34);
            this.Moreaddress.Margin = new System.Windows.Forms.Padding(2);
            this.Moreaddress.Name = "Moreaddress";
            this.Moreaddress.Size = new System.Drawing.Size(161, 29);
            this.Moreaddress.TabIndex = 48;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox3.Location = new System.Drawing.Point(365, 80);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(111, 20);
            this.checkBox3.TabIndex = 45;
            this.checkBox3.Text = "字符串颠倒";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // Link
            // 
            this.Link.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Link.Location = new System.Drawing.Point(601, 31);
            this.Link.Margin = new System.Windows.Forms.Padding(2);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(125, 30);
            this.Link.TabIndex = 26;
            this.Link.Text = "连接";
            this.Link.UseVisualStyleBackColor = true;
            this.Link.Click += new System.EventHandler(this.Link_Click);
            // 
            // DLINK
            // 
            this.DLINK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DLINK.Location = new System.Drawing.Point(601, 74);
            this.DLINK.Margin = new System.Windows.Forms.Padding(2);
            this.DLINK.Name = "DLINK";
            this.DLINK.Size = new System.Drawing.Size(125, 30);
            this.DLINK.TabIndex = 27;
            this.DLINK.Text = "断开";
            this.DLINK.UseVisualStyleBackColor = true;
            this.DLINK.Click += new System.EventHandler(this.DLINK_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(210, 79);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(130, 20);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.Text = "首地址从0开始";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tx_StationID
            // 
            this.tx_StationID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_StationID.Location = new System.Drawing.Point(111, 75);
            this.tx_StationID.Margin = new System.Windows.Forms.Padding(2);
            this.tx_StationID.Name = "tx_StationID";
            this.tx_StationID.Size = new System.Drawing.Size(53, 26);
            this.tx_StationID.TabIndex = 38;
            this.tx_StationID.Text = "1";
            this.tx_StationID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(28, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 16);
            this.label11.TabIndex = 23;
            this.label11.Text = "设备站号:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.txPLCPort);
            this.groupBox1.Controls.Add(this.tx_StationID);
            this.groupBox1.Controls.Add(this.Lable4);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.txPLCIP);
            this.groupBox1.Controls.Add(this.Link);
            this.groupBox1.Controls.Add(this.DLINK);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(11, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(762, 120);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备通讯参数";
            // 
            // txPLCPort
            // 
            this.txPLCPort.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txPLCPort.Location = new System.Drawing.Point(339, 35);
            this.txPLCPort.Margin = new System.Windows.Forms.Padding(2);
            this.txPLCPort.Name = "txPLCPort";
            this.txPLCPort.Size = new System.Drawing.Size(47, 23);
            this.txPLCPort.TabIndex = 50;
            this.txPLCPort.Text = "502";
            this.txPLCPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Lable4
            // 
            this.Lable4.AutoSize = true;
            this.Lable4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lable4.Location = new System.Drawing.Point(256, 39);
            this.Lable4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lable4.Name = "Lable4";
            this.Lable4.Size = new System.Drawing.Size(77, 14);
            this.Lable4.TabIndex = 49;
            this.Lable4.Text = "设备 Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(9, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 14);
            this.label6.TabIndex = 48;
            this.label6.Text = "设备 IP 地址:";
            // 
            // txPLCIP
            // 
            this.txPLCIP.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txPLCIP.Location = new System.Drawing.Point(111, 35);
            this.txPLCIP.Margin = new System.Windows.Forms.Padding(2);
            this.txPLCIP.Name = "txPLCIP";
            this.txPLCIP.Size = new System.Drawing.Size(125, 23);
            this.txPLCIP.TabIndex = 47;
            this.txPLCIP.Text = "192.168.0.1";
            this.txPLCIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(242, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(300, 28);
            this.label7.TabIndex = 38;
            this.label7.Text = "Modbus TCP/IP 设备调试窗口";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(434, 31);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "结果:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(434, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "结果:";
            // 
            // ModbusTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ModbusTCP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModbusTCP";
            this.Load += new System.EventHandler(this.ModbusTCP_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txstr_leth;
        private System.Windows.Forms.Button ushort_Read;
        private System.Windows.Forms.Button Byte_Read;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txAddress;
        private System.Windows.Forms.TextBox txResult;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button ushort_Write;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txWriteAD;
        private System.Windows.Forms.TextBox txWirteData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ReadLeth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox MoreReadResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Moreaddress;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button Link;
        private System.Windows.Forms.Button DLINK;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox tx_StationID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txPLCPort;
        private System.Windows.Forms.Label Lable4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txPLCIP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
    }
}