namespace MotorBrakeTestApp
{
    partial class 登录
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(登录));
            this.tx_user = new System.Windows.Forms.TextBox();
            this.tx_pw = new System.Windows.Forms.TextBox();
            this.bt_login = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bt_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tx_user
            // 
            this.tx_user.BackColor = System.Drawing.SystemColors.Info;
            this.tx_user.Location = new System.Drawing.Point(211, 173);
            this.tx_user.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_user.Name = "tx_user";
            this.tx_user.Size = new System.Drawing.Size(143, 21);
            this.tx_user.TabIndex = 1;
            // 
            // tx_pw
            // 
            this.tx_pw.BackColor = System.Drawing.SystemColors.Info;
            this.tx_pw.Location = new System.Drawing.Point(211, 223);
            this.tx_pw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_pw.Name = "tx_pw";
            this.tx_pw.PasswordChar = '*';
            this.tx_pw.Size = new System.Drawing.Size(143, 21);
            this.tx_pw.TabIndex = 3;
            this.tx_pw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tx_pw_KeyDown);
            // 
            // bt_login
            // 
            this.bt_login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_login.Image = global::MotorBrakeTestApp.Properties.Resources.登录button;
            this.bt_login.Location = new System.Drawing.Point(119, 331);
            this.bt_login.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bt_login.Name = "bt_login";
            this.bt_login.Size = new System.Drawing.Size(102, 26);
            this.bt_login.TabIndex = 5;
            this.bt_login.UseVisualStyleBackColor = true;
            this.bt_login.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::MotorBrakeTestApp.Properties.Resources.password;
            this.pictureBox3.Location = new System.Drawing.Point(173, 216);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(191, 32);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MotorBrakeTestApp.Properties.Resources.user;
            this.pictureBox2.Location = new System.Drawing.Point(173, 167);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(191, 32);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MotorBrakeTestApp.Properties.Resources.logbackpicture;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(536, 403);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // bt_cancel
            // 
            this.bt_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_cancel.Image = global::MotorBrakeTestApp.Properties.Resources.取消button;
            this.bt_cancel.Location = new System.Drawing.Point(325, 331);
            this.bt_cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(102, 26);
            this.bt_cancel.TabIndex = 6;
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // 登录
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 407);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_login);
            this.Controls.Add(this.tx_pw);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.tx_user);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "登录";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.登录_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tx_user;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox tx_pw;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button bt_login;
        private System.Windows.Forms.Button bt_cancel;
    }
}