namespace ChangeColor
{
    partial class Form1
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
            this.tbColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hsbRed = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRed = new System.Windows.Forms.Label();
            this.lbGreen = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.hsbGreen = new System.Windows.Forms.HScrollBar();
            this.lbBlue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.hsbBlue = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // tbColor
            // 
            this.tbColor.Location = new System.Drawing.Point(58, 56);
            this.tbColor.Multiline = true;
            this.tbColor.Name = "tbColor";
            this.tbColor.Size = new System.Drawing.Size(300, 175);
            this.tbColor.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color";
            // 
            // hsbRed
            // 
            this.hsbRed.Location = new System.Drawing.Point(102, 276);
            this.hsbRed.Maximum = 255;
            this.hsbRed.Name = "hsbRed";
            this.hsbRed.Size = new System.Drawing.Size(228, 29);
            this.hsbRed.TabIndex = 2;
            this.hsbRed.ValueChanged += new System.EventHandler(this.ColorChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(40, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Red";
            // 
            // lbRed
            // 
            this.lbRed.AutoSize = true;
            this.lbRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRed.ForeColor = System.Drawing.Color.Red;
            this.lbRed.Location = new System.Drawing.Point(347, 276);
            this.lbRed.Name = "lbRed";
            this.lbRed.Size = new System.Drawing.Size(30, 24);
            this.lbRed.TabIndex = 4;
            this.lbRed.Text = "25";
            // 
            // lbGreen
            // 
            this.lbGreen.AutoSize = true;
            this.lbGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreen.ForeColor = System.Drawing.Color.Green;
            this.lbGreen.Location = new System.Drawing.Point(347, 338);
            this.lbGreen.Name = "lbGreen";
            this.lbGreen.Size = new System.Drawing.Size(30, 24);
            this.lbGreen.TabIndex = 7;
            this.lbGreen.Text = "25";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(40, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Green";
            // 
            // hsbGreen
            // 
            this.hsbGreen.Location = new System.Drawing.Point(102, 338);
            this.hsbGreen.Maximum = 255;
            this.hsbGreen.Name = "hsbGreen";
            this.hsbGreen.Size = new System.Drawing.Size(228, 29);
            this.hsbGreen.TabIndex = 5;
            this.hsbGreen.ValueChanged += new System.EventHandler(this.ColorChange);
            // 
            // lbBlue
            // 
            this.lbBlue.AutoSize = true;
            this.lbBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBlue.ForeColor = System.Drawing.Color.Blue;
            this.lbBlue.Location = new System.Drawing.Point(347, 401);
            this.lbBlue.Name = "lbBlue";
            this.lbBlue.Size = new System.Drawing.Size(30, 24);
            this.lbBlue.TabIndex = 10;
            this.lbBlue.Text = "25";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(40, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "Blue";
            // 
            // hsbBlue
            // 
            this.hsbBlue.Location = new System.Drawing.Point(102, 401);
            this.hsbBlue.Maximum = 255;
            this.hsbBlue.Name = "hsbBlue";
            this.hsbBlue.Size = new System.Drawing.Size(228, 29);
            this.hsbBlue.TabIndex = 8;
            this.hsbBlue.ValueChanged += new System.EventHandler(this.ColorChange);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 553);
            this.Controls.Add(this.lbBlue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.hsbBlue);
            this.Controls.Add(this.lbGreen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hsbGreen);
            this.Controls.Add(this.lbRed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hsbRed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbColor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar hsbRed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbRed;
        private System.Windows.Forms.Label lbGreen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.HScrollBar hsbGreen;
        private System.Windows.Forms.Label lbBlue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HScrollBar hsbBlue;
    }
}

