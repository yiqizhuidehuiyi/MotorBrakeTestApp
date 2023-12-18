namespace MotorBrakeTestApp
{
    partial class Alarm
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
            this.SuspendLayout();
            // 
            // userLantern1
            // 
            this.userLantern1.BackColor = System.Drawing.Color.Transparent;
            this.userLantern1.Location = new System.Drawing.Point(26, 12);
            this.userLantern1.Name = "userLantern1";
            this.userLantern1.Size = new System.Drawing.Size(97, 121);
            this.userLantern1.TabIndex = 0;
            // 
            // Alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.userLantern1);
            this.Name = "Alarm";
            this.Text = "Alarm";
            this.ResumeLayout(false);

        }

        #endregion

        private HslCommunication.Controls.UserLantern userLantern1;
    }
}