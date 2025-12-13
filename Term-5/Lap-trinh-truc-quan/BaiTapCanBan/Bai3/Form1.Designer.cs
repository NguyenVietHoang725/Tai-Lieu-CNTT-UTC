namespace Bai3
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnThemDS = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTien = new System.Windows.Forms.TextBox();
            this.cbSL = new System.Windows.Forms.ComboBox();
            this.cbDoUong = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGiaThuyen = new System.Windows.Forms.TextBox();
            this.rdbNuaNgay = new System.Windows.Forms.RadioButton();
            this.rdbCaNgay = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstDSKH = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHoTen);
            this.groupBox1.Controls.Add(this.btnThemMoi);
            this.groupBox1.Controls.Add(this.btnThemDS);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTien);
            this.groupBox1.Controls.Add(this.cbSL);
            this.groupBox1.Controls.Add(this.cbDoUong);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtGiaThuyen);
            this.groupBox1.Controls.Add(this.rdbNuaNgay);
            this.groupBox1.Controls.Add(this.rdbCaNgay);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(25, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 303);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(63, 25);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(155, 20);
            this.txtHoTen.TabIndex = 16;
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Location = new System.Drawing.Point(176, 217);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(99, 40);
            this.btnThemMoi.TabIndex = 15;
            this.btnThemMoi.Text = "Thêm &mới";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // btnThemDS
            // 
            this.btnThemDS.Location = new System.Drawing.Point(35, 217);
            this.btnThemDS.Name = "btnThemDS";
            this.btnThemDS.Size = new System.Drawing.Size(99, 40);
            this.btnThemDS.TabIndex = 14;
            this.btnThemDS.Text = "Thêm vào DS";
            this.btnThemDS.UseVisualStyleBackColor = true;
            this.btnThemDS.Click += new System.EventHandler(this.btnThemDS_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "$";
            // 
            // txtTien
            // 
            this.txtTien.Location = new System.Drawing.Point(203, 170);
            this.txtTien.Name = "txtTien";
            this.txtTien.ReadOnly = true;
            this.txtTien.Size = new System.Drawing.Size(72, 20);
            this.txtTien.TabIndex = 12;
            // 
            // cbSL
            // 
            this.cbSL.FormattingEnabled = true;
            this.cbSL.Location = new System.Drawing.Point(125, 169);
            this.cbSL.Name = "cbSL";
            this.cbSL.Size = new System.Drawing.Size(72, 21);
            this.cbSL.TabIndex = 11;
            this.cbSL.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // cbDoUong
            // 
            this.cbDoUong.FormattingEnabled = true;
            this.cbDoUong.Location = new System.Drawing.Point(21, 169);
            this.cbDoUong.Name = "cbDoUong";
            this.cbDoUong.Size = new System.Drawing.Size(99, 21);
            this.cbDoUong.TabIndex = 10;
            this.cbDoUong.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Tiền";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(128, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Số lượng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Chọn đồ uống";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "$";
            // 
            // txtGiaThuyen
            // 
            this.txtGiaThuyen.Location = new System.Drawing.Point(97, 102);
            this.txtGiaThuyen.Name = "txtGiaThuyen";
            this.txtGiaThuyen.ReadOnly = true;
            this.txtGiaThuyen.Size = new System.Drawing.Size(100, 20);
            this.txtGiaThuyen.TabIndex = 5;
            // 
            // rdbNuaNgay
            // 
            this.rdbNuaNgay.AutoSize = true;
            this.rdbNuaNgay.Location = new System.Drawing.Point(126, 63);
            this.rdbNuaNgay.Name = "rdbNuaNgay";
            this.rdbNuaNgay.Size = new System.Drawing.Size(71, 17);
            this.rdbNuaNgay.TabIndex = 4;
            this.rdbNuaNgay.TabStop = true;
            this.rdbNuaNgay.Text = "Nửa ngày";
            this.rdbNuaNgay.UseVisualStyleBackColor = true;
            this.rdbNuaNgay.CheckedChanged += new System.EventHandler(this.rdb_CheckedChanged);
            // 
            // rdbCaNgay
            // 
            this.rdbCaNgay.AutoSize = true;
            this.rdbCaNgay.Location = new System.Drawing.Point(21, 63);
            this.rdbCaNgay.Name = "rdbCaNgay";
            this.rdbCaNgay.Size = new System.Drawing.Size(64, 17);
            this.rdbCaNgay.TabIndex = 3;
            this.rdbCaNgay.TabStop = true;
            this.rdbCaNgay.Text = "Cả ngày";
            this.rdbCaNgay.UseVisualStyleBackColor = true;
            this.rdbCaNgay.CheckedChanged += new System.EventHandler(this.rdb_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Giá du thuyền";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Họ tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhận thông tin khách hàng đặt tour";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstDSKH);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(344, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 257);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // lstDSKH
            // 
            this.lstDSKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDSKH.FormattingEnabled = true;
            this.lstDSKH.ItemHeight = 16;
            this.lstDSKH.Location = new System.Drawing.Point(7, 16);
            this.lstDSKH.Name = "lstDSKH";
            this.lstDSKH.Size = new System.Drawing.Size(431, 228);
            this.lstDSKH.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Danh sách khách hàng đặt tour";
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(652, 276);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(99, 40);
            this.btnThoat.TabIndex = 16;
            this.btnThoat.Text = "T&hoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 329);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Công ty du thuyền Hồ Tây";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbNuaNgay;
        private System.Windows.Forms.RadioButton rdbCaNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGiaThuyen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnThemDS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTien;
        private System.Windows.Forms.ComboBox cbSL;
        private System.Windows.Forms.ComboBox cbDoUong;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ListBox lstDSKH;
    }
}

