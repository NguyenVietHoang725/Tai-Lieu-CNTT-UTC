namespace LibraryManagerApp.GUI.UserControls.TrangChu
{
    partial class ucFrmTrangChu
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.pnlWelcome = new System.Windows.Forms.Panel();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblUserIcon = new System.Windows.Forms.Label();
            this.pnlDateTime = new System.Windows.Forms.Panel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.tlpCards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardTaiLieu = new System.Windows.Forms.Panel();
            this.lblCountTaiLieu = new System.Windows.Forms.Label();
            this.lblTitleTaiLieu = new System.Windows.Forms.Label();
            this.lblIconTaiLieu = new System.Windows.Forms.Label();
            this.pnlCardBanDoc = new System.Windows.Forms.Panel();
            this.lblCountBanDoc = new System.Windows.Forms.Label();
            this.lblTitleBanDoc = new System.Windows.Forms.Label();
            this.lblIconBanDoc = new System.Windows.Forms.Label();
            this.pnlCardDangMuon = new System.Windows.Forms.Panel();
            this.lblCountDangMuon = new System.Windows.Forms.Label();
            this.lblTitleDangMuon = new System.Windows.Forms.Label();
            this.lblIconDangMuon = new System.Windows.Forms.Label();
            this.pnlCardQuaHan = new System.Windows.Forms.Panel();
            this.lblCountQuaHan = new System.Windows.Forms.Label();
            this.lblTitleQuaHan = new System.Windows.Forms.Label();
            this.lblIconQuaHan = new System.Windows.Forms.Label();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.pnlActivity = new System.Windows.Forms.Panel();
            this.dgvActivity = new System.Windows.Forms.DataGridView();
            this.lblActivityTitle = new System.Windows.Forms.Label();
            this.pnlTopBooks = new System.Windows.Forms.Panel();
            this.dgvTopBooks = new System.Windows.Forms.DataGridView();
            this.lblTopBooksTitle = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.pnlWelcome.SuspendLayout();
            this.pnlDateTime.SuspendLayout();
            this.tlpCards.SuspendLayout();
            this.pnlCardTaiLieu.SuspendLayout();
            this.pnlCardBanDoc.SuspendLayout();
            this.pnlCardDangMuon.SuspendLayout();
            this.pnlCardQuaHan.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.pnlActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).BeginInit();
            this.pnlTopBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(242)))));
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpHeader, 0, 0);
            this.tlpMain.Controls.Add(this.tlpCards, 0, 1);
            this.tlpMain.Controls.Add(this.tlpBottom, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(15);
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(822, 681);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpHeader.Controls.Add(this.pnlWelcome, 0, 0);
            this.tlpHeader.Controls.Add(this.pnlDateTime, 1, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(15, 15);
            this.tlpHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(792, 90);
            this.tlpHeader.TabIndex = 0;
            // 
            // pnlWelcome
            // 
            this.pnlWelcome.BackColor = System.Drawing.Color.White;
            this.pnlWelcome.Controls.Add(this.lblRole);
            this.pnlWelcome.Controls.Add(this.lblWelcome);
            this.pnlWelcome.Controls.Add(this.lblUserIcon);
            this.pnlWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWelcome.Location = new System.Drawing.Point(3, 3);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(548, 84);
            this.pnlWelcome.TabIndex = 0;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblRole.Location = new System.Drawing.Point(75, 48);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(168, 18);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Vai trò: Quản trị";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblWelcome.Location = new System.Drawing.Point(73, 18);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(346, 24);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Xin chào, [Tên nhân viên]";
            // 
            // lblUserIcon
            // 
            this.lblUserIcon.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserIcon.Location = new System.Drawing.Point(12, 12);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(60, 60);
            this.lblUserIcon.TabIndex = 0;
            this.lblUserIcon.Text = "👤";
            this.lblUserIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDateTime
            // 
            this.pnlDateTime.BackColor = System.Drawing.Color.White;
            this.pnlDateTime.Controls.Add(this.lblDateTime);
            this.pnlDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDateTime.Location = new System.Drawing.Point(557, 3);
            this.pnlDateTime.Name = "pnlDateTime";
            this.pnlDateTime.Size = new System.Drawing.Size(232, 84);
            this.pnlDateTime.TabIndex = 1;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateTime.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblDateTime.Location = new System.Drawing.Point(0, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(232, 84);
            this.lblDateTime.TabIndex = 0;
            this.lblDateTime.Text = "🕐 Thứ Sáu\r\n14/11/2025\r\n10:30 AM";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpCards
            // 
            this.tlpCards.ColumnCount = 4;
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCards.Controls.Add(this.pnlCardTaiLieu, 0, 0);
            this.tlpCards.Controls.Add(this.pnlCardBanDoc, 1, 0);
            this.tlpCards.Controls.Add(this.pnlCardDangMuon, 2, 0);
            this.tlpCards.Controls.Add(this.pnlCardQuaHan, 3, 0);
            this.tlpCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCards.Location = new System.Drawing.Point(15, 110);
            this.tlpCards.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.tlpCards.Name = "tlpCards";
            this.tlpCards.RowCount = 1;
            this.tlpCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCards.Size = new System.Drawing.Size(792, 170);
            this.tlpCards.TabIndex = 1;
            // 
            // pnlCardTaiLieu
            // 
            this.pnlCardTaiLieu.BackColor = System.Drawing.Color.White;
            this.pnlCardTaiLieu.Controls.Add(this.lblCountTaiLieu);
            this.pnlCardTaiLieu.Controls.Add(this.lblTitleTaiLieu);
            this.pnlCardTaiLieu.Controls.Add(this.lblIconTaiLieu);
            this.pnlCardTaiLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTaiLieu.Location = new System.Drawing.Point(3, 3);
            this.pnlCardTaiLieu.Name = "pnlCardTaiLieu";
            this.pnlCardTaiLieu.Size = new System.Drawing.Size(192, 164);
            this.pnlCardTaiLieu.TabIndex = 0;
            // 
            // lblCountTaiLieu
            // 
            this.lblCountTaiLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountTaiLieu.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountTaiLieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblCountTaiLieu.Location = new System.Drawing.Point(0, 60);
            this.lblCountTaiLieu.Name = "lblCountTaiLieu";
            this.lblCountTaiLieu.Size = new System.Drawing.Size(192, 69);
            this.lblCountTaiLieu.TabIndex = 2;
            this.lblCountTaiLieu.Text = "0";
            this.lblCountTaiLieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleTaiLieu
            // 
            this.lblTitleTaiLieu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTitleTaiLieu.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleTaiLieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitleTaiLieu.Location = new System.Drawing.Point(0, 129);
            this.lblTitleTaiLieu.Name = "lblTitleTaiLieu";
            this.lblTitleTaiLieu.Size = new System.Drawing.Size(192, 35);
            this.lblTitleTaiLieu.TabIndex = 1;
            this.lblTitleTaiLieu.Text = "Tổng tài liệu";
            this.lblTitleTaiLieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIconTaiLieu
            // 
            this.lblIconTaiLieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIconTaiLieu.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconTaiLieu.Location = new System.Drawing.Point(0, 0);
            this.lblIconTaiLieu.Name = "lblIconTaiLieu";
            this.lblIconTaiLieu.Size = new System.Drawing.Size(192, 60);
            this.lblIconTaiLieu.TabIndex = 0;
            this.lblIconTaiLieu.Text = "📚";
            this.lblIconTaiLieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardBanDoc
            // 
            this.pnlCardBanDoc.BackColor = System.Drawing.Color.White;
            this.pnlCardBanDoc.Controls.Add(this.lblCountBanDoc);
            this.pnlCardBanDoc.Controls.Add(this.lblTitleBanDoc);
            this.pnlCardBanDoc.Controls.Add(this.lblIconBanDoc);
            this.pnlCardBanDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardBanDoc.Location = new System.Drawing.Point(201, 3);
            this.pnlCardBanDoc.Name = "pnlCardBanDoc";
            this.pnlCardBanDoc.Size = new System.Drawing.Size(192, 164);
            this.pnlCardBanDoc.TabIndex = 1;
            // 
            // lblCountBanDoc
            // 
            this.lblCountBanDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountBanDoc.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountBanDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblCountBanDoc.Location = new System.Drawing.Point(0, 60);
            this.lblCountBanDoc.Name = "lblCountBanDoc";
            this.lblCountBanDoc.Size = new System.Drawing.Size(192, 69);
            this.lblCountBanDoc.TabIndex = 2;
            this.lblCountBanDoc.Text = "0";
            this.lblCountBanDoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleBanDoc
            // 
            this.lblTitleBanDoc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTitleBanDoc.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleBanDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitleBanDoc.Location = new System.Drawing.Point(0, 129);
            this.lblTitleBanDoc.Name = "lblTitleBanDoc";
            this.lblTitleBanDoc.Size = new System.Drawing.Size(192, 35);
            this.lblTitleBanDoc.TabIndex = 1;
            this.lblTitleBanDoc.Text = "Tổng bạn đọc";
            this.lblTitleBanDoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIconBanDoc
            // 
            this.lblIconBanDoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIconBanDoc.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconBanDoc.Location = new System.Drawing.Point(0, 0);
            this.lblIconBanDoc.Name = "lblIconBanDoc";
            this.lblIconBanDoc.Size = new System.Drawing.Size(192, 60);
            this.lblIconBanDoc.TabIndex = 0;
            this.lblIconBanDoc.Text = "👥";
            this.lblIconBanDoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardDangMuon
            // 
            this.pnlCardDangMuon.BackColor = System.Drawing.Color.White;
            this.pnlCardDangMuon.Controls.Add(this.lblCountDangMuon);
            this.pnlCardDangMuon.Controls.Add(this.lblTitleDangMuon);
            this.pnlCardDangMuon.Controls.Add(this.lblIconDangMuon);
            this.pnlCardDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardDangMuon.Location = new System.Drawing.Point(399, 3);
            this.pnlCardDangMuon.Name = "pnlCardDangMuon";
            this.pnlCardDangMuon.Size = new System.Drawing.Size(192, 164);
            this.pnlCardDangMuon.TabIndex = 2;
            // 
            // lblCountDangMuon
            // 
            this.lblCountDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountDangMuon.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDangMuon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblCountDangMuon.Location = new System.Drawing.Point(0, 60);
            this.lblCountDangMuon.Name = "lblCountDangMuon";
            this.lblCountDangMuon.Size = new System.Drawing.Size(192, 69);
            this.lblCountDangMuon.TabIndex = 2;
            this.lblCountDangMuon.Text = "0";
            this.lblCountDangMuon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleDangMuon
            // 
            this.lblTitleDangMuon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTitleDangMuon.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleDangMuon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitleDangMuon.Location = new System.Drawing.Point(0, 129);
            this.lblTitleDangMuon.Name = "lblTitleDangMuon";
            this.lblTitleDangMuon.Size = new System.Drawing.Size(192, 35);
            this.lblTitleDangMuon.TabIndex = 1;
            this.lblTitleDangMuon.Text = "Đang mượn";
            this.lblTitleDangMuon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIconDangMuon
            // 
            this.lblIconDangMuon.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIconDangMuon.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconDangMuon.Location = new System.Drawing.Point(0, 0);
            this.lblIconDangMuon.Name = "lblIconDangMuon";
            this.lblIconDangMuon.Size = new System.Drawing.Size(192, 60);
            this.lblIconDangMuon.TabIndex = 0;
            this.lblIconDangMuon.Text = "📖";
            this.lblIconDangMuon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardQuaHan
            // 
            this.pnlCardQuaHan.BackColor = System.Drawing.Color.White;
            this.pnlCardQuaHan.Controls.Add(this.lblCountQuaHan);
            this.pnlCardQuaHan.Controls.Add(this.lblTitleQuaHan);
            this.pnlCardQuaHan.Controls.Add(this.lblIconQuaHan);
            this.pnlCardQuaHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardQuaHan.Location = new System.Drawing.Point(597, 3);
            this.pnlCardQuaHan.Name = "pnlCardQuaHan";
            this.pnlCardQuaHan.Size = new System.Drawing.Size(192, 164);
            this.pnlCardQuaHan.TabIndex = 3;
            // 
            // lblCountQuaHan
            // 
            this.lblCountQuaHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountQuaHan.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountQuaHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblCountQuaHan.Location = new System.Drawing.Point(0, 60);
            this.lblCountQuaHan.Name = "lblCountQuaHan";
            this.lblCountQuaHan.Size = new System.Drawing.Size(192, 69);
            this.lblCountQuaHan.TabIndex = 2;
            this.lblCountQuaHan.Text = "0";
            this.lblCountQuaHan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleQuaHan
            // 
            this.lblTitleQuaHan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTitleQuaHan.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleQuaHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitleQuaHan.Location = new System.Drawing.Point(0, 129);
            this.lblTitleQuaHan.Name = "lblTitleQuaHan";
            this.lblTitleQuaHan.Size = new System.Drawing.Size(192, 35);
            this.lblTitleQuaHan.TabIndex = 1;
            this.lblTitleQuaHan.Text = "Sách quá hạn";
            this.lblTitleQuaHan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIconQuaHan
            // 
            this.lblIconQuaHan.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIconQuaHan.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconQuaHan.Location = new System.Drawing.Point(0, 0);
            this.lblIconQuaHan.Name = "lblIconQuaHan";
            this.lblIconQuaHan.Size = new System.Drawing.Size(192, 60);
            this.lblIconQuaHan.TabIndex = 0;
            this.lblIconQuaHan.Text = "⚠️";
            this.lblIconQuaHan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpBottom
            // 
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpBottom.Controls.Add(this.pnlActivity, 0, 0);
            this.tlpBottom.Controls.Add(this.pnlTopBooks, 1, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBottom.Location = new System.Drawing.Point(15, 290);
            this.tlpBottom.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.Size = new System.Drawing.Size(792, 376);
            this.tlpBottom.TabIndex = 2;
            // 
            // pnlActivity
            // 
            this.pnlActivity.BackColor = System.Drawing.Color.White;
            this.pnlActivity.Controls.Add(this.dgvActivity);
            this.pnlActivity.Controls.Add(this.lblActivityTitle);
            this.pnlActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActivity.Location = new System.Drawing.Point(3, 3);
            this.pnlActivity.Name = "pnlActivity";
            this.pnlActivity.Padding = new System.Windows.Forms.Padding(10);
            this.pnlActivity.Size = new System.Drawing.Size(469, 370);
            this.pnlActivity.TabIndex = 0;
            // 
            // dgvActivity
            // 
            this.dgvActivity.AllowUserToAddRows = false;
            this.dgvActivity.AllowUserToDeleteRows = false;
            this.dgvActivity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivity.BackgroundColor = System.Drawing.Color.White;
            this.dgvActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivity.Location = new System.Drawing.Point(10, 50);
            this.dgvActivity.Name = "dgvActivity";
            this.dgvActivity.ReadOnly = true;
            this.dgvActivity.RowHeadersVisible = false;
            this.dgvActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivity.Size = new System.Drawing.Size(449, 310);
            this.dgvActivity.TabIndex = 1;
            // 
            // lblActivityTitle
            // 
            this.lblActivityTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblActivityTitle.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblActivityTitle.Location = new System.Drawing.Point(10, 10);
            this.lblActivityTitle.Name = "lblActivityTitle";
            this.lblActivityTitle.Size = new System.Drawing.Size(449, 40);
            this.lblActivityTitle.TabIndex = 0;
            this.lblActivityTitle.Text = "📋 Hoạt động gần đây";
            this.lblActivityTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTopBooks
            // 
            this.pnlTopBooks.BackColor = System.Drawing.Color.White;
            this.pnlTopBooks.Controls.Add(this.dgvTopBooks);
            this.pnlTopBooks.Controls.Add(this.lblTopBooksTitle);
            this.pnlTopBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopBooks.Location = new System.Drawing.Point(478, 3);
            this.pnlTopBooks.Name = "pnlTopBooks";
            this.pnlTopBooks.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTopBooks.Size = new System.Drawing.Size(311, 370);
            this.pnlTopBooks.TabIndex = 1;
            // 
            // dgvTopBooks
            // 
            this.dgvTopBooks.AllowUserToAddRows = false;
            this.dgvTopBooks.AllowUserToDeleteRows = false;
            this.dgvTopBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopBooks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopBooks.Location = new System.Drawing.Point(10, 50);
            this.dgvTopBooks.Name = "dgvTopBooks";
            this.dgvTopBooks.ReadOnly = true;
            this.dgvTopBooks.RowHeadersVisible = false;
            this.dgvTopBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopBooks.Size = new System.Drawing.Size(291, 310);
            this.dgvTopBooks.TabIndex = 1;
            // 
            // lblTopBooksTitle
            // 
            this.lblTopBooksTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopBooksTitle.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopBooksTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(52)))), ((int)(((byte)(129)))));
            this.lblTopBooksTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTopBooksTitle.Name = "lblTopBooksTitle";
            this.lblTopBooksTitle.Size = new System.Drawing.Size(291, 40);
            this.lblTopBooksTitle.TabIndex = 0;
            this.lblTopBooksTitle.Text = "⭐ Top sách được mượn";
            this.lblTopBooksTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucFrmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.tlpMain);
            this.Name = "ucFrmTrangChu";
            this.Size = new System.Drawing.Size(822, 681);
            this.tlpMain.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.pnlWelcome.ResumeLayout(false);
            this.pnlWelcome.PerformLayout();
            this.pnlDateTime.ResumeLayout(false);
            this.tlpCards.ResumeLayout(false);
            this.pnlCardTaiLieu.ResumeLayout(false);
            this.pnlCardBanDoc.ResumeLayout(false);
            this.pnlCardDangMuon.ResumeLayout(false);
            this.pnlCardQuaHan.ResumeLayout(false);
            this.tlpBottom.ResumeLayout(false);
            this.pnlActivity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).EndInit();
            this.pnlTopBooks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Panel pnlWelcome;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblUserIcon;
        private System.Windows.Forms.Panel pnlDateTime;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.TableLayoutPanel tlpCards;
        private System.Windows.Forms.Panel pnlCardTaiLieu;
        private System.Windows.Forms.Label lblCountTaiLieu;
        private System.Windows.Forms.Label lblTitleTaiLieu;
        private System.Windows.Forms.Label lblIconTaiLieu;
        private System.Windows.Forms.Panel pnlCardBanDoc;
        private System.Windows.Forms.Label lblCountBanDoc;
        private System.Windows.Forms.Label lblTitleBanDoc;
        private System.Windows.Forms.Label lblIconBanDoc;
        private System.Windows.Forms.Panel pnlCardDangMuon;
        private System.Windows.Forms.Label lblCountDangMuon;
        private System.Windows.Forms.Label lblTitleDangMuon;
        private System.Windows.Forms.Label lblIconDangMuon;
        private System.Windows.Forms.Panel pnlCardQuaHan;
        private System.Windows.Forms.Label lblCountQuaHan;
        private System.Windows.Forms.Label lblTitleQuaHan;
        private System.Windows.Forms.Label lblIconQuaHan;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Panel pnlActivity;
        private System.Windows.Forms.DataGridView dgvActivity;
        private System.Windows.Forms.Label lblActivityTitle;
        private System.Windows.Forms.Panel pnlTopBooks;
        private System.Windows.Forms.DataGridView dgvTopBooks;
        private System.Windows.Forms.Label lblTopBooksTitle;
    }
}