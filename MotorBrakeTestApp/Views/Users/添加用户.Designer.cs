namespace MotorBrakeTestApp
{
    partial class 添加用户
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(添加用户));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tx_id = new System.Windows.Forms.TextBox();
            this.tx_pw = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tx_pw_twice = new System.Windows.Forms.TextBox();
            this.cb_Level = new System.Windows.Forms.ComboBox();
            this.bt_register = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.txname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label1.Location = new System.Drawing.Point(355, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "添加用户";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label2.Location = new System.Drawing.Point(202, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "账号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label3.Location = new System.Drawing.Point(202, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label4.Location = new System.Drawing.Point(166, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "密码确认:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label5.Location = new System.Drawing.Point(166, 430);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "权限等级:";
            // 
            // tx_id
            // 
            this.tx_id.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_id.Location = new System.Drawing.Point(288, 125);
            this.tx_id.MaxLength = 10;
            this.tx_id.Multiline = true;
            this.tx_id.Name = "tx_id";
            this.tx_id.Size = new System.Drawing.Size(288, 41);
            this.tx_id.TabIndex = 7;
            // 
            // tx_pw
            // 
            this.tx_pw.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_pw.Location = new System.Drawing.Point(288, 205);
            this.tx_pw.MaxLength = 10;
            this.tx_pw.Multiline = true;
            this.tx_pw.Name = "tx_pw";
            this.tx_pw.PasswordChar = '*';
            this.tx_pw.Size = new System.Drawing.Size(288, 41);
            this.tx_pw.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MotorBrakeTestApp.Properties.Resources.BackPicture_add_user;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(804, 605);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tx_pw_twice
            // 
            this.tx_pw_twice.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_pw_twice.Location = new System.Drawing.Point(288, 286);
            this.tx_pw_twice.MaxLength = 10;
            this.tx_pw_twice.Multiline = true;
            this.tx_pw_twice.Name = "tx_pw_twice";
            this.tx_pw_twice.PasswordChar = '*';
            this.tx_pw_twice.Size = new System.Drawing.Size(288, 41);
            this.tx_pw_twice.TabIndex = 9;
            // 
            // cb_Level
            // 
            this.cb_Level.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cb_Level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Level.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Level.FormattingEnabled = true;
            this.cb_Level.Location = new System.Drawing.Point(288, 429);
            this.cb_Level.Name = "cb_Level";
            this.cb_Level.Size = new System.Drawing.Size(165, 36);
            this.cb_Level.TabIndex = 10;
            // 
            // bt_register
            // 
            this.bt_register.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_register.Location = new System.Drawing.Point(204, 492);
            this.bt_register.Name = "bt_register";
            this.bt_register.Size = new System.Drawing.Size(136, 54);
            this.bt_register.TabIndex = 11;
            this.bt_register.Text = "注册";
            this.bt_register.UseVisualStyleBackColor = true;
            this.bt_register.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_cancel.Location = new System.Drawing.Point(494, 492);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(136, 54);
            this.bt_cancel.TabIndex = 12;
            this.bt_cancel.Text = "取消";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // txname
            // 
            this.txname.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txname.Location = new System.Drawing.Point(288, 361);
            this.txname.MaxLength = 10;
            this.txname.Multiline = true;
            this.txname.Name = "txname";
            this.txname.PasswordChar = '*';
            this.txname.Size = new System.Drawing.Size(288, 41);
            this.txname.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label6.Location = new System.Drawing.Point(166, 371);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "人员姓名:";
            // 
            // 添加用户
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.txname);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_register);
            this.Controls.Add(this.cb_Level);
            this.Controls.Add(this.tx_pw_twice);
            this.Controls.Add(this.tx_pw);
            this.Controls.Add(this.tx_id);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "添加用户";
            this.Text = "添加用户";
            this.Load += new System.EventHandler(this.添加用户_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tx_id;
        private System.Windows.Forms.TextBox tx_pw;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tx_pw_twice;
        private System.Windows.Forms.ComboBox cb_Level;
        private System.Windows.Forms.Button bt_register;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.TextBox txname;
        private System.Windows.Forms.Label label6;
    }
}