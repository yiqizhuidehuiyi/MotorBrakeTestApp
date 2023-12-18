namespace MotorBrakeTestApp
{
    partial class LoadForm
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
            this.progressIndicator1 = new ReaLTaiizor.Controls.ProgressIndicator();
            this.label1 = new System.Windows.Forms.Label();
            this.progressIndicator2 = new ReaLTaiizor.Controls.ProgressIndicator();
            this.SuspendLayout();
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(388, 40);
            this.progressIndicator1.MinimumSize = new System.Drawing.Size(50, 50);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.P_AnimationColor = System.Drawing.Color.DimGray;
            this.progressIndicator1.P_AnimationSpeed = 100;
            this.progressIndicator1.P_BaseColor = System.Drawing.Color.DarkGray;
            this.progressIndicator1.Size = new System.Drawing.Size(80, 80);
            this.progressIndicator1.TabIndex = 3;
            this.progressIndicator1.Text = "progressIndicator1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(134, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 38);
            this.label1.TabIndex = 4;
            this.label1.Text = "正在启动中... ...";
            // 
            // progressIndicator2
            // 
            this.progressIndicator2.Location = new System.Drawing.Point(12, 40);
            this.progressIndicator2.MinimumSize = new System.Drawing.Size(50, 50);
            this.progressIndicator2.Name = "progressIndicator2";
            this.progressIndicator2.P_AnimationColor = System.Drawing.Color.DimGray;
            this.progressIndicator2.P_AnimationSpeed = 100;
            this.progressIndicator2.P_BaseColor = System.Drawing.Color.DarkGray;
            this.progressIndicator2.Size = new System.Drawing.Size(80, 80);
            this.progressIndicator2.TabIndex = 5;
            this.progressIndicator2.Text = "progressIndicator2";
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(213)))), ((int)(((byte)(88)))));
            this.ClientSize = new System.Drawing.Size(480, 160);
            this.Controls.Add(this.progressIndicator2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressIndicator1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.ProgressIndicator progressIndicator2;
    }
}