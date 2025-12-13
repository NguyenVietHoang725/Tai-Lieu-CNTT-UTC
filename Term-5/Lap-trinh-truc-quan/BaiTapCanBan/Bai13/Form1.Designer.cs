namespace Bai13
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNhapN = new System.Windows.Forms.TextBox();
            this.btnTimSoDuongNN = new System.Windows.Forms.Button();
            this.btnNhapDay = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnLamLai = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNhapK = new System.Windows.Forms.TextBox();
            this.lblXuatDS = new System.Windows.Forms.Label();
            this.lblXuatSoDuongNN = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập số phần tử";
            // 
            // txtNhapN
            // 
            this.txtNhapN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapN.Location = new System.Drawing.Point(195, 20);
            this.txtNhapN.Name = "txtNhapN";
            this.txtNhapN.Size = new System.Drawing.Size(100, 29);
            this.txtNhapN.TabIndex = 1;
            // 
            // btnTimSoDuongNN
            // 
            this.btnTimSoDuongNN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimSoDuongNN.Location = new System.Drawing.Point(40, 151);
            this.btnTimSoDuongNN.Name = "btnTimSoDuongNN";
            this.btnTimSoDuongNN.Size = new System.Drawing.Size(250, 50);
            this.btnTimSoDuongNN.TabIndex = 2;
            this.btnTimSoDuongNN.Text = "Tìm số dương nhỏ nhất";
            this.btnTimSoDuongNN.UseVisualStyleBackColor = true;
            this.btnTimSoDuongNN.Click += new System.EventHandler(this.btnTimSoDuongNN_Click);
            // 
            // btnNhapDay
            // 
            this.btnNhapDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhapDay.Location = new System.Drawing.Point(422, 10);
            this.btnNhapDay.Name = "btnNhapDay";
            this.btnNhapDay.Size = new System.Drawing.Size(125, 50);
            this.btnNhapDay.TabIndex = 3;
            this.btnNhapDay.Text = "&Nhập dãy";
            this.btnNhapDay.UseVisualStyleBackColor = true;
            this.btnNhapDay.Click += new System.EventHandler(this.btnNhapDay_Click);
            // 
            // btnTim
            // 
            this.btnTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.Location = new System.Drawing.Point(422, 288);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(125, 50);
            this.btnTim.TabIndex = 4;
            this.btnTim.Text = "&Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnLamLai
            // 
            this.btnLamLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamLai.Location = new System.Drawing.Point(273, 369);
            this.btnLamLai.Name = "btnLamLai";
            this.btnLamLai.Size = new System.Drawing.Size(125, 50);
            this.btnLamLai.TabIndex = 5;
            this.btnLamLai.Text = "&Làm lại";
            this.btnLamLai.UseVisualStyleBackColor = true;
            this.btnLamLai.Click += new System.EventHandler(this.btnLamLai_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(422, 369);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(125, 50);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nhập một số k =";
            // 
            // txtNhapK
            // 
            this.txtNhapK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapK.Location = new System.Drawing.Point(193, 298);
            this.txtNhapK.Name = "txtNhapK";
            this.txtNhapK.Size = new System.Drawing.Size(100, 29);
            this.txtNhapK.TabIndex = 8;
            // 
            // lblXuatDS
            // 
            this.lblXuatDS.AutoSize = true;
            this.lblXuatDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXuatDS.ForeColor = System.Drawing.Color.Red;
            this.lblXuatDS.Location = new System.Drawing.Point(40, 88);
            this.lblXuatDS.Name = "lblXuatDS";
            this.lblXuatDS.Size = new System.Drawing.Size(179, 24);
            this.lblXuatDS.TabIndex = 9;
            this.lblXuatDS.Text = "Dãy số vừa nhập là: ";
            // 
            // lblXuatSoDuongNN
            // 
            this.lblXuatSoDuongNN.AutoSize = true;
            this.lblXuatSoDuongNN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXuatSoDuongNN.Location = new System.Drawing.Point(40, 240);
            this.lblXuatSoDuongNN.Name = "lblXuatSoDuongNN";
            this.lblXuatSoDuongNN.Size = new System.Drawing.Size(201, 24);
            this.lblXuatSoDuongNN.TabIndex = 10;
            this.lblXuatSoDuongNN.Text = "Số dương nhỏ nhất là: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 450);
            this.Controls.Add(this.lblXuatSoDuongNN);
            this.Controls.Add(this.lblXuatDS);
            this.Controls.Add(this.txtNhapK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLamLai);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.btnNhapDay);
            this.Controls.Add(this.btnTimSoDuongNN);
            this.Controls.Add(this.txtNhapN);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Làm việc với dãy số";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNhapN;
        private System.Windows.Forms.Button btnTimSoDuongNN;
        private System.Windows.Forms.Button btnNhapDay;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnLamLai;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNhapK;
        private System.Windows.Forms.Label lblXuatDS;
        private System.Windows.Forms.Label lblXuatSoDuongNN;
    }
}

