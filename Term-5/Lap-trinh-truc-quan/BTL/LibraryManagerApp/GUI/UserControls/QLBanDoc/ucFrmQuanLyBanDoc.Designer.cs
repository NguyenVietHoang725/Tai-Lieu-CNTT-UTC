namespace LibraryManagerApp.GUI.UserControls.QLBanDoc
{
    partial class ucFrmQuanLyBanDoc
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
            this.btnTheBanDoc = new System.Windows.Forms.Button();
            this.btnThongTinBanDoc = new System.Windows.Forms.Button();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
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
            this.pnlHeader.Size = new System.Drawing.Size(816, 118);
            this.pnlHeader.TabIndex = 0;
            // 
            // tlpChuyenChucNang
            // 
            this.tlpChuyenChucNang.ColumnCount = 2;
            this.tlpChuyenChucNang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.Controls.Add(this.btnTheBanDoc, 1, 0);
            this.tlpChuyenChucNang.Controls.Add(this.btnThongTinBanDoc, 0, 0);
            this.tlpChuyenChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpChuyenChucNang.Location = new System.Drawing.Point(0, 59);
            this.tlpChuyenChucNang.Name = "tlpChuyenChucNang";
            this.tlpChuyenChucNang.RowCount = 1;
            this.tlpChuyenChucNang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChuyenChucNang.Size = new System.Drawing.Size(816, 59);
            this.tlpChuyenChucNang.TabIndex = 1;
            // 
            // btnTheBanDoc
            // 
            this.btnTheBanDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTheBanDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheBanDoc.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTheBanDoc.Location = new System.Drawing.Point(408, 0);
            this.btnTheBanDoc.Margin = new System.Windows.Forms.Padding(0);
            this.btnTheBanDoc.Name = "btnTheBanDoc";
            this.btnTheBanDoc.Size = new System.Drawing.Size(408, 59);
            this.btnTheBanDoc.TabIndex = 2;
            this.btnTheBanDoc.Text = "Thẻ bạn đọc";
            this.btnTheBanDoc.UseVisualStyleBackColor = true;
            this.btnTheBanDoc.Click += new System.EventHandler(this.btnTheBanDoc_Click);
            // 
            // btnThongTinBanDoc
            // 
            this.btnThongTinBanDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongTinBanDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongTinBanDoc.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongTinBanDoc.Location = new System.Drawing.Point(0, 0);
            this.btnThongTinBanDoc.Margin = new System.Windows.Forms.Padding(0);
            this.btnThongTinBanDoc.Name = "btnThongTinBanDoc";
            this.btnThongTinBanDoc.Size = new System.Drawing.Size(408, 59);
            this.btnThongTinBanDoc.TabIndex = 1;
            this.btnThongTinBanDoc.Text = "Thông tin bạn đọc";
            this.btnThongTinBanDoc.UseVisualStyleBackColor = true;
            this.btnThongTinBanDoc.Click += new System.EventHandler(this.btnThongTinBanDoc_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.tableLayoutPanel1);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(816, 59);
            this.pnlTitle.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblTieuDe, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(816, 59);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(3, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(810, 59);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "XEM THÔNG TIN BẠN ĐỌC";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(242)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 118);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(816, 557);
            this.pnlContent.TabIndex = 1;
            // 
            // ucFrmQuanLyBanDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucFrmQuanLyBanDoc";
            this.Size = new System.Drawing.Size(816, 675);
            this.Load += new System.EventHandler(this.ucFrmQuanLyBanDoc_Load);
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
        private System.Windows.Forms.Button btnTheBanDoc;
        private System.Windows.Forms.Button btnThongTinBanDoc;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.TableLayoutPanel tlpChuyenChucNang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
