namespace Bai18_ColorDialog
{
    partial class frmColor
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
            this.btnMauNen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMauNen
            // 
            this.btnMauNen.Location = new System.Drawing.Point(137, 91);
            this.btnMauNen.Name = "btnMauNen";
            this.btnMauNen.Size = new System.Drawing.Size(113, 46);
            this.btnMauNen.TabIndex = 0;
            this.btnMauNen.Text = "Màu nền";
            this.btnMauNen.UseVisualStyleBackColor = true;
            this.btnMauNen.Click += new System.EventHandler(this.btnMauNen_Click);
            // 
            // frmColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 244);
            this.Controls.Add(this.btnMauNen);
            this.Name = "frmColor";
            this.Text = "Đổi màu nền";
            this.Load += new System.EventHandler(this.frmColor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMauNen;
    }
}

