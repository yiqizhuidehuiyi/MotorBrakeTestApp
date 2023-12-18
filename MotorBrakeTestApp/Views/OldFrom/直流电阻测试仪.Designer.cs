namespace MotorBrakeTestApp
{
    partial class 直流电阻测试仪
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
            this.components = new System.ComponentModel.Container();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AiserialPort = new System.IO.Ports.SerialPort(this.components);
            this.tx_StopByte = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Link = new System.Windows.Forms.Button();
            this.DLINK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tx_databyte = new System.Windows.Forms.TextBox();
            this.cb_Parity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_serialport_baud = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_serialport_list = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DCR_serialPort = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(239, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(229, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "直流电阻测量仪(TH2516)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(128, 224);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(140, 45);
            this.textBox1.TabIndex = 45;
            // 
            // tx_StopByte
            // 
            this.tx_StopByte.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_StopByte.Location = new System.Drawing.Point(473, 24);
            this.tx_StopByte.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_StopByte.Name = "tx_StopByte";
            this.tx_StopByte.Size = new System.Drawing.Size(53, 23);
            this.tx_StopByte.TabIndex = 37;
            this.tx_StopByte.Text = "1";
            this.tx_StopByte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(409, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "停止位:";
            // 
            // Link
            // 
            this.Link.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Link.Location = new System.Drawing.Point(525, 71);
            this.Link.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(55, 23);
            this.Link.TabIndex = 26;
            this.Link.Text = "连接";
            this.Link.UseVisualStyleBackColor = true;
            this.Link.Click += new System.EventHandler(this.Link_Click);
            // 
            // DLINK
            // 
            this.DLINK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DLINK.Location = new System.Drawing.Point(595, 71);
            this.DLINK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DLINK.Name = "DLINK";
            this.DLINK.Size = new System.Drawing.Size(55, 23);
            this.DLINK.TabIndex = 27;
            this.DLINK.Text = "断开";
            this.DLINK.UseVisualStyleBackColor = true;
            this.DLINK.Click += new System.EventHandler(this.DLINK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(534, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "校验:";
            // 
            // tx_databyte
            // 
            this.tx_databyte.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_databyte.Location = new System.Drawing.Point(353, 24);
            this.tx_databyte.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_databyte.Name = "tx_databyte";
            this.tx_databyte.Size = new System.Drawing.Size(53, 23);
            this.tx_databyte.TabIndex = 35;
            this.tx_databyte.Text = "8";
            this.tx_databyte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cb_Parity
            // 
            this.cb_Parity.AutoCompleteCustomSource.AddRange(new string[] {
            "None",
            "Even",
            "Odd"});
            this.cb_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Parity.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Parity.FormattingEnabled = true;
            this.cb_Parity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cb_Parity.Location = new System.Drawing.Point(586, 24);
            this.cb_Parity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Parity.Name = "cb_Parity";
            this.cb_Parity.Size = new System.Drawing.Size(63, 22);
            this.cb_Parity.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(293, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "数据位:";
            // 
            // cb_serialport_baud
            // 
            this.cb_serialport_baud.AutoCompleteCustomSource.AddRange(new string[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.cb_serialport_baud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_serialport_baud.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_serialport_baud.FormattingEnabled = true;
            this.cb_serialport_baud.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.cb_serialport_baud.Location = new System.Drawing.Point(209, 24);
            this.cb_serialport_baud.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_serialport_baud.Name = "cb_serialport_baud";
            this.cb_serialport_baud.Size = new System.Drawing.Size(81, 22);
            this.cb_serialport_baud.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(144, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "波特率:";
            // 
            // cb_serialport_list
            // 
            this.cb_serialport_list.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_serialport_list.FormattingEnabled = true;
            this.cb_serialport_list.Location = new System.Drawing.Point(83, 24);
            this.cb_serialport_list.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_serialport_list.Name = "cb_serialport_list";
            this.cb_serialport_list.Size = new System.Drawing.Size(53, 22);
            this.cb_serialport_list.TabIndex = 30;
            this.cb_serialport_list.Text = "COM3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 29);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "设备端口:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tx_StopByte);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Link);
            this.groupBox1.Controls.Add(this.DLINK);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tx_databyte);
            this.groupBox1.Controls.Add(this.cb_Parity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_serialport_baud);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_serialport_list);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("楷体", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(665, 105);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "直流电阻通讯参数";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 224);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 44);
            this.button1.TabIndex = 44;
            this.button1.Text = "读取设备型号";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(8, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 114);
            this.panel1.TabIndex = 43;
            // 
            // DCR_serialPort
            // 
            this.DCR_serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.DCR_serialPort_DataReceived);
            // 
            // 直流电阻测试仪
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 652);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "直流电阻测试仪";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "直流电阻测试仪";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.直流电阻测试仪_FormClosed);
            this.Load += new System.EventHandler(this.直流电阻测试仪_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort AiserialPort;
        private System.Windows.Forms.TextBox tx_StopByte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Link;
        private System.Windows.Forms.Button DLINK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tx_databyte;
        private System.Windows.Forms.ComboBox cb_Parity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_serialport_baud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_serialport_list;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.IO.Ports.SerialPort DCR_serialPort;
    }
}