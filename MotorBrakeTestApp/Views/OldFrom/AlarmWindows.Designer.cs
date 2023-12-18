namespace MotorBrakeTestApp
{
    partial class AlarmWindows
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
            this.userLantern1 = new HslCommunication.Controls.UserLantern();
            this.userLantern2 = new HslCommunication.Controls.UserLantern();
            this.userLantern3 = new HslCommunication.Controls.UserLantern();
            this.userLantern4 = new HslCommunication.Controls.UserLantern();
            this.userLantern5 = new HslCommunication.Controls.UserLantern();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userLantern1
            // 
            this.userLantern1.BackColor = System.Drawing.Color.Transparent;
            this.userLantern1.Location = new System.Drawing.Point(62, 60);
            this.userLantern1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userLantern1.Name = "userLantern1";
            this.userLantern1.Size = new System.Drawing.Size(87, 79);
            this.userLantern1.TabIndex = 0;
            // 
            // userLantern2
            // 
            this.userLantern2.BackColor = System.Drawing.Color.Transparent;
            this.userLantern2.Location = new System.Drawing.Point(204, 60);
            this.userLantern2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userLantern2.Name = "userLantern2";
            this.userLantern2.Size = new System.Drawing.Size(87, 79);
            this.userLantern2.TabIndex = 1;
            // 
            // userLantern3
            // 
            this.userLantern3.BackColor = System.Drawing.Color.Transparent;
            this.userLantern3.Location = new System.Drawing.Point(346, 60);
            this.userLantern3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userLantern3.Name = "userLantern3";
            this.userLantern3.Size = new System.Drawing.Size(87, 79);
            this.userLantern3.TabIndex = 2;
            // 
            // userLantern4
            // 
            this.userLantern4.BackColor = System.Drawing.Color.Transparent;
            this.userLantern4.Location = new System.Drawing.Point(488, 60);
            this.userLantern4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userLantern4.Name = "userLantern4";
            this.userLantern4.Size = new System.Drawing.Size(87, 79);
            this.userLantern4.TabIndex = 3;
            // 
            // userLantern5
            // 
            this.userLantern5.BackColor = System.Drawing.Color.Transparent;
            this.userLantern5.Location = new System.Drawing.Point(630, 60);
            this.userLantern5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userLantern5.Name = "userLantern5";
            this.userLantern5.Size = new System.Drawing.Size(87, 79);
            this.userLantern5.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "PLC连接状态";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "电阻测试仪\r\n连接状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "安规连接状态";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "功率仪连接状态";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(610, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "电压表连接状态";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(216, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(335, 33);
            this.label6.TabIndex = 10;
            this.label6.Text = "尝试重新连接中。。。";
            // 
            // AlarmWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 367);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userLantern5);
            this.Controls.Add(this.userLantern4);
            this.Controls.Add(this.userLantern3);
            this.Controls.Add(this.userLantern2);
            this.Controls.Add(this.userLantern1);
            this.Name = "AlarmWindows";
            this.Text = "AlarmWindows";
            this.Load += new System.EventHandler(this.AlarmWindows_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HslCommunication.Controls.UserLantern userLantern1;
        private HslCommunication.Controls.UserLantern userLantern2;
        private HslCommunication.Controls.UserLantern userLantern3;
        private HslCommunication.Controls.UserLantern userLantern4;
        private HslCommunication.Controls.UserLantern userLantern5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}