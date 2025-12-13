namespace Bai9_ThayDoiFont
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
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbFont4 = new System.Windows.Forms.RadioButton();
            this.rdbFont3 = new System.Windows.Forms.RadioButton();
            this.rdbFont2 = new System.Windows.Forms.RadioButton();
            this.rdbFont1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbViolet = new System.Windows.Forms.RadioButton();
            this.rdbGreen = new System.Windows.Forms.RadioButton();
            this.rdbRed = new System.Windows.Forms.RadioButton();
            this.rdbBlue = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbUnderline = new System.Windows.Forms.CheckBox();
            this.chbStrikeout = new System.Windows.Forms.CheckBox();
            this.chbItalic = new System.Windows.Forms.CheckBox();
            this.chbBold = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKetQua
            // 
            this.txtKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKetQua.Location = new System.Drawing.Point(123, 38);
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.Size = new System.Drawing.Size(337, 38);
            this.txtKetQua.TabIndex = 0;
            this.txtKetQua.Text = "Microsoft Visual C#";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbFont4);
            this.groupBox1.Controls.Add(this.rdbFont3);
            this.groupBox1.Controls.Add(this.rdbFont2);
            this.groupBox1.Controls.Add(this.rdbFont1);
            this.groupBox1.Location = new System.Drawing.Point(36, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 185);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kiểu font";
            // 
            // rdbFont4
            // 
            this.rdbFont4.AutoSize = true;
            this.rdbFont4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFont4.Location = new System.Drawing.Point(21, 134);
            this.rdbFont4.Name = "rdbFont4";
            this.rdbFont4.Size = new System.Drawing.Size(63, 17);
            this.rdbFont4.TabIndex = 3;
            this.rdbFont4.TabStop = true;
            this.rdbFont4.Text = "Tahoma";
            this.rdbFont4.UseVisualStyleBackColor = true;
            this.rdbFont4.CheckedChanged += new System.EventHandler(this.rdbFontChanged);
            // 
            // rdbFont3
            // 
            this.rdbFont3.AutoSize = true;
            this.rdbFont3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFont3.Location = new System.Drawing.Point(21, 101);
            this.rdbFont3.Name = "rdbFont3";
            this.rdbFont3.Size = new System.Drawing.Size(73, 17);
            this.rdbFont3.TabIndex = 2;
            this.rdbFont3.TabStop = true;
            this.rdbFont3.Text = "Consolas";
            this.rdbFont3.UseVisualStyleBackColor = true;
            this.rdbFont3.CheckedChanged += new System.EventHandler(this.rdbFontChanged);
            // 
            // rdbFont2
            // 
            this.rdbFont2.AutoSize = true;
            this.rdbFont2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFont2.Location = new System.Drawing.Point(21, 68);
            this.rdbFont2.Name = "rdbFont2";
            this.rdbFont2.Size = new System.Drawing.Size(56, 17);
            this.rdbFont2.TabIndex = 1;
            this.rdbFont2.TabStop = true;
            this.rdbFont2.Text = "Calibri";
            this.rdbFont2.UseVisualStyleBackColor = true;
            this.rdbFont2.CheckedChanged += new System.EventHandler(this.rdbFontChanged);
            // 
            // rdbFont1
            // 
            this.rdbFont1.AutoSize = true;
            this.rdbFont1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFont1.Location = new System.Drawing.Point(21, 35);
            this.rdbFont1.Name = "rdbFont1";
            this.rdbFont1.Size = new System.Drawing.Size(113, 18);
            this.rdbFont1.TabIndex = 0;
            this.rdbFont1.TabStop = true;
            this.rdbFont1.Text = "Times New Roman";
            this.rdbFont1.UseVisualStyleBackColor = true;
            this.rdbFont1.CheckedChanged += new System.EventHandler(this.rdbFontChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbViolet);
            this.groupBox2.Controls.Add(this.rdbGreen);
            this.groupBox2.Controls.Add(this.rdbRed);
            this.groupBox2.Controls.Add(this.rdbBlue);
            this.groupBox2.Location = new System.Drawing.Point(392, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 185);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Màu chữ";
            // 
            // rdbViolet
            // 
            this.rdbViolet.AutoSize = true;
            this.rdbViolet.ForeColor = System.Drawing.Color.Violet;
            this.rdbViolet.Location = new System.Drawing.Point(37, 134);
            this.rdbViolet.Name = "rdbViolet";
            this.rdbViolet.Size = new System.Drawing.Size(44, 17);
            this.rdbViolet.TabIndex = 3;
            this.rdbViolet.TabStop = true;
            this.rdbViolet.Text = "Tím";
            this.rdbViolet.UseVisualStyleBackColor = true;
            this.rdbViolet.CheckedChanged += new System.EventHandler(this.rdbColorChanged);
            // 
            // rdbGreen
            // 
            this.rdbGreen.AutoSize = true;
            this.rdbGreen.ForeColor = System.Drawing.Color.Green;
            this.rdbGreen.Location = new System.Drawing.Point(37, 101);
            this.rdbGreen.Name = "rdbGreen";
            this.rdbGreen.Size = new System.Drawing.Size(61, 17);
            this.rdbGreen.TabIndex = 2;
            this.rdbGreen.TabStop = true;
            this.rdbGreen.Text = "Xanh lá";
            this.rdbGreen.UseVisualStyleBackColor = true;
            this.rdbGreen.CheckedChanged += new System.EventHandler(this.rdbColorChanged);
            // 
            // rdbRed
            // 
            this.rdbRed.AutoSize = true;
            this.rdbRed.ForeColor = System.Drawing.Color.Red;
            this.rdbRed.Location = new System.Drawing.Point(37, 68);
            this.rdbRed.Name = "rdbRed";
            this.rdbRed.Size = new System.Drawing.Size(39, 17);
            this.rdbRed.TabIndex = 1;
            this.rdbRed.TabStop = true;
            this.rdbRed.Text = "Đỏ";
            this.rdbRed.UseVisualStyleBackColor = true;
            this.rdbRed.CheckedChanged += new System.EventHandler(this.rdbColorChanged);
            // 
            // rdbBlue
            // 
            this.rdbBlue.AutoSize = true;
            this.rdbBlue.ForeColor = System.Drawing.Color.Blue;
            this.rdbBlue.Location = new System.Drawing.Point(37, 35);
            this.rdbBlue.Name = "rdbBlue";
            this.rdbBlue.Size = new System.Drawing.Size(83, 17);
            this.rdbBlue.TabIndex = 0;
            this.rdbBlue.TabStop = true;
            this.rdbBlue.Text = "Xanh dương";
            this.rdbBlue.UseVisualStyleBackColor = true;
            this.rdbBlue.CheckedChanged += new System.EventHandler(this.rdbColorChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbUnderline);
            this.groupBox3.Controls.Add(this.chbStrikeout);
            this.groupBox3.Controls.Add(this.chbItalic);
            this.groupBox3.Controls.Add(this.chbBold);
            this.groupBox3.Location = new System.Drawing.Point(214, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 185);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hiệu ứng";
            // 
            // chbUnderline
            // 
            this.chbUnderline.AutoSize = true;
            this.chbUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbUnderline.Location = new System.Drawing.Point(37, 134);
            this.chbUnderline.Name = "chbUnderline";
            this.chbUnderline.Size = new System.Drawing.Size(71, 17);
            this.chbUnderline.TabIndex = 7;
            this.chbUnderline.Text = "Underline";
            this.chbUnderline.UseVisualStyleBackColor = true;
            this.chbUnderline.CheckedChanged += new System.EventHandler(this.chbEffectChanged);
            // 
            // chbStrikeout
            // 
            this.chbStrikeout.AutoSize = true;
            this.chbStrikeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbStrikeout.Location = new System.Drawing.Point(37, 101);
            this.chbStrikeout.Name = "chbStrikeout";
            this.chbStrikeout.Size = new System.Drawing.Size(68, 17);
            this.chbStrikeout.TabIndex = 6;
            this.chbStrikeout.Text = "Strikeout";
            this.chbStrikeout.UseVisualStyleBackColor = true;
            this.chbStrikeout.CheckedChanged += new System.EventHandler(this.chbEffectChanged);
            // 
            // chbItalic
            // 
            this.chbItalic.AutoSize = true;
            this.chbItalic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbItalic.Location = new System.Drawing.Point(37, 68);
            this.chbItalic.Name = "chbItalic";
            this.chbItalic.Size = new System.Drawing.Size(48, 17);
            this.chbItalic.TabIndex = 5;
            this.chbItalic.Text = "Italic";
            this.chbItalic.UseVisualStyleBackColor = true;
            this.chbItalic.CheckedChanged += new System.EventHandler(this.chbEffectChanged);
            // 
            // chbBold
            // 
            this.chbBold.AutoSize = true;
            this.chbBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbBold.Location = new System.Drawing.Point(37, 35);
            this.chbBold.Name = "chbBold";
            this.chbBold.Size = new System.Drawing.Size(51, 17);
            this.chbBold.TabIndex = 4;
            this.chbBold.Text = "Bold";
            this.chbBold.UseVisualStyleBackColor = true;
            this.chbBold.CheckedChanged += new System.EventHandler(this.chbEffectChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 412);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtKetQua);
            this.Name = "Form1";
            this.Text = "Program of Font";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKetQua;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbFont2;
        private System.Windows.Forms.RadioButton rdbFont1;
        private System.Windows.Forms.RadioButton rdbFont4;
        private System.Windows.Forms.RadioButton rdbFont3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbViolet;
        private System.Windows.Forms.RadioButton rdbGreen;
        private System.Windows.Forms.RadioButton rdbRed;
        private System.Windows.Forms.RadioButton rdbBlue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chbUnderline;
        private System.Windows.Forms.CheckBox chbStrikeout;
        private System.Windows.Forms.CheckBox chbItalic;
        private System.Windows.Forms.CheckBox chbBold;
    }
}

