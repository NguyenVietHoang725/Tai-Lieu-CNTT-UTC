namespace Bai11
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
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbGachChan = new System.Windows.Forms.CheckBox();
            this.chbNghieng = new System.Windows.Forms.CheckBox();
            this.chbDam = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbDen = new System.Windows.Forms.RadioButton();
            this.rdbXanhLa = new System.Windows.Forms.RadioButton();
            this.rdbDo = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLamLai = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoiDung.Location = new System.Drawing.Point(104, 32);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(584, 72);
            this.txtNoiDung.TabIndex = 0;
            this.txtNoiDung.Text = "BÀI THI MÔN TIN HỌC ĐẠI CƯƠNG";
            this.txtNoiDung.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSize);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbFont);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(42, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(717, 119);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // cbSize
            // 
            this.cbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSize.FormattingEnabled = true;
            this.cbSize.Location = new System.Drawing.Point(527, 43);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(143, 32);
            this.cbSize.TabIndex = 6;
            this.cbSize.SelectedIndexChanged += new System.EventHandler(this.btnSize_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(422, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kích thước";
            // 
            // cbFont
            // 
            this.cbFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Location = new System.Drawing.Point(101, 43);
            this.cbFont.Name = "cbFont";
            this.cbFont.Size = new System.Drawing.Size(233, 32);
            this.cbFont.TabIndex = 5;
            this.cbFont.SelectedIndexChanged += new System.EventHandler(this.btnFont_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kiểu chữ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Font";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbGachChan);
            this.groupBox2.Controls.Add(this.chbNghieng);
            this.groupBox2.Controls.Add(this.chbDam);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(42, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(717, 119);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // chbGachChan
            // 
            this.chbGachChan.AutoSize = true;
            this.chbGachChan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbGachChan.Location = new System.Drawing.Point(515, 45);
            this.chbGachChan.Name = "chbGachChan";
            this.chbGachChan.Size = new System.Drawing.Size(121, 28);
            this.chbGachChan.TabIndex = 6;
            this.chbGachChan.Text = "Gạch chân";
            this.chbGachChan.UseVisualStyleBackColor = true;
            this.chbGachChan.CheckedChanged += new System.EventHandler(this.chbGachChan_CheckedChanged);
            // 
            // chbNghieng
            // 
            this.chbNghieng.AutoSize = true;
            this.chbNghieng.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbNghieng.Location = new System.Drawing.Point(282, 45);
            this.chbNghieng.Name = "chbNghieng";
            this.chbNghieng.Size = new System.Drawing.Size(102, 28);
            this.chbNghieng.TabIndex = 5;
            this.chbNghieng.Text = "Nghiêng";
            this.chbNghieng.UseVisualStyleBackColor = true;
            this.chbNghieng.CheckedChanged += new System.EventHandler(this.chbNghieng_CheckedChanged);
            // 
            // chbDam
            // 
            this.chbDam.AutoSize = true;
            this.chbDam.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbDam.Location = new System.Drawing.Point(80, 45);
            this.chbDam.Name = "chbDam";
            this.chbDam.Size = new System.Drawing.Size(71, 28);
            this.chbDam.TabIndex = 4;
            this.chbDam.Text = "Đậm";
            this.chbDam.UseVisualStyleBackColor = true;
            this.chbDam.CheckedChanged += new System.EventHandler(this.chbDam_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "Hiệu ứng chữ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbDen);
            this.groupBox3.Controls.Add(this.rdbXanhLa);
            this.groupBox3.Controls.Add(this.rdbDo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(42, 421);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(717, 119);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // rdbDen
            // 
            this.rdbDen.AutoSize = true;
            this.rdbDen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDen.ForeColor = System.Drawing.Color.Black;
            this.rdbDen.Location = new System.Drawing.Point(546, 45);
            this.rdbDen.Name = "rdbDen";
            this.rdbDen.Size = new System.Drawing.Size(63, 28);
            this.rdbDen.TabIndex = 5;
            this.rdbDen.TabStop = true;
            this.rdbDen.Text = "Đen";
            this.rdbDen.UseVisualStyleBackColor = true;
            this.rdbDen.CheckedChanged += new System.EventHandler(this.rdbDen_CheckedChanged);
            // 
            // rdbXanhLa
            // 
            this.rdbXanhLa.AutoSize = true;
            this.rdbXanhLa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbXanhLa.ForeColor = System.Drawing.Color.Lime;
            this.rdbXanhLa.Location = new System.Drawing.Point(289, 45);
            this.rdbXanhLa.Name = "rdbXanhLa";
            this.rdbXanhLa.Size = new System.Drawing.Size(127, 28);
            this.rdbXanhLa.TabIndex = 4;
            this.rdbXanhLa.TabStop = true;
            this.rdbXanhLa.Text = "Xanh lá cây";
            this.rdbXanhLa.UseVisualStyleBackColor = true;
            this.rdbXanhLa.CheckedChanged += new System.EventHandler(this.rdbXanhLa_CheckedChanged);
            // 
            // rdbDo
            // 
            this.rdbDo.AutoSize = true;
            this.rdbDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDo.ForeColor = System.Drawing.Color.Red;
            this.rdbDo.Location = new System.Drawing.Point(107, 45);
            this.rdbDo.Name = "rdbDo";
            this.rdbDo.Size = new System.Drawing.Size(52, 28);
            this.rdbDo.TabIndex = 3;
            this.rdbDo.TabStop = true;
            this.rdbDo.Text = "Đỏ";
            this.rdbDo.UseVisualStyleBackColor = true;
            this.rdbDo.CheckedChanged += new System.EventHandler(this.rdbDo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Màu chữ";
            // 
            // btnLamLai
            // 
            this.btnLamLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamLai.Location = new System.Drawing.Point(249, 561);
            this.btnLamLai.Name = "btnLamLai";
            this.btnLamLai.Size = new System.Drawing.Size(136, 48);
            this.btnLamLai.TabIndex = 9;
            this.btnLamLai.Text = "&Làm lại";
            this.btnLamLai.UseVisualStyleBackColor = true;
            this.btnLamLai.Click += new System.EventHandler(this.btnLamLai_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(415, 561);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(136, 48);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 634);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLamLai);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNoiDung);
            this.Name = "Form1";
            this.Text = "Thay đổi font chữ";
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

        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbGachChan;
        private System.Windows.Forms.CheckBox chbNghieng;
        private System.Windows.Forms.CheckBox chbDam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbDen;
        private System.Windows.Forms.RadioButton rdbXanhLa;
        private System.Windows.Forms.RadioButton rdbDo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLamLai;
        private System.Windows.Forms.Button btnThoat;
    }
}

