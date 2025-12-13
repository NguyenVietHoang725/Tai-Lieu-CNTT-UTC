namespace LibraryManagerApp.GUI.UserControls.QLPhanQuyen
{
    partial class ucFrmQuanLyPhanQuyen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.tlpChuyenChucNang = new System.Windows.Forms.TableLayoutPanel();
            this.btnThongTinTaiKhoan = new System.Windows.Forms.Button();
            this.btnThongTinNhanVien = new System.Windows.Forms.Button();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.tlpChuyenChucNang.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.tlpChuyenChucNang);
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(864, 118);
            this.pnlHeader.TabIndex = 0;
            // 
            // tlpChuyenChucNang
            // 
            this.tlpChuyenChucNang.ColumnCount = 2;
            this.tlpChuyenChucNang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.Controls.Add(this.btnThongTinTaiKhoan, 1, 0);
            this.tlpChuyenChucNang.Controls.Add(this.btnThongTinNhanVien, 0, 0);
            this.tlpChuyenChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpChuyenChucNang.Location = new System.Drawing.Point(0, 59);
            this.tlpChuyenChucNang.Name = "tlpChuyenChucNang";
            this.tlpChuyenChucNang.RowCount = 1;
            this.tlpChuyenChucNang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.Size = new System.Drawing.Size(864, 59);
            this.tlpChuyenChucNang.TabIndex = 1;
            // 
            // btnThongTinTaiKhoan
            // 
            this.btnThongTinTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongTinTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongTinTaiKhoan.Location = new System.Drawing.Point(432, 0);
            this.btnThongTinTaiKhoan.Margin = new System.Windows.Forms.Padding(0);
            this.btnThongTinTaiKhoan.Name = "btnThongTinTaiKhoan";
            this.btnThongTinTaiKhoan.Size = new System.Drawing.Size(432, 59);
            this.btnThongTinTaiKhoan.TabIndex = 2;
            this.btnThongTinTaiKhoan.Text = "Thông tin tài khoản";
            this.btnThongTinTaiKhoan.UseVisualStyleBackColor = true;
            this.btnThongTinTaiKhoan.Click += new System.EventHandler(this.btnThongTinTaiKhoan_Click);
            // 
            // btnThongTinNhanVien
            // 
            this.btnThongTinNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongTinNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongTinNhanVien.Location = new System.Drawing.Point(0, 0);
            this.btnThongTinNhanVien.Margin = new System.Windows.Forms.Padding(0);
            this.btnThongTinNhanVien.Name = "btnThongTinNhanVien";
            this.btnThongTinNhanVien.Size = new System.Drawing.Size(432, 59);
            this.btnThongTinNhanVien.TabIndex = 1;
            this.btnThongTinNhanVien.Text = "Thông tin nhân viên";
            this.btnThongTinNhanVien.UseVisualStyleBackColor = true;
            this.btnThongTinNhanVien.Click += new System.EventHandler(this.btnThongTinNhanVien_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.tableLayoutPanel1);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(864, 59);
            this.pnlTitle.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 59);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(858, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "XEM THÔNG TIN BẠN ĐỌC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(242)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 118);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(864, 563);
            this.pnlContent.TabIndex = 1;
            // 
            // ucFrmQuanLyPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucFrmQuanLyPhanQuyen";
            this.Size = new System.Drawing.Size(864, 681);
            this.Load += new System.EventHandler(this.ucFrmQuanLyPhanQuyen_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tlpChuyenChucNang.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnThongTinTaiKhoan;
        private System.Windows.Forms.Button btnThongTinNhanVien;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.TableLayoutPanel tlpChuyenChucNang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
