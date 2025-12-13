namespace Bai10_GhepQuocGiavoiThuDo
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
            this.gbCountry = new System.Windows.Forms.GroupBox();
            this.rdbFrance = new System.Windows.Forms.RadioButton();
            this.rdbJapan = new System.Windows.Forms.RadioButton();
            this.rdbHungary = new System.Windows.Forms.RadioButton();
            this.rdbSpain = new System.Windows.Forms.RadioButton();
            this.radTurkey = new System.Windows.Forms.RadioButton();
            this.rdbTheUK = new System.Windows.Forms.RadioButton();
            this.rdbItaly = new System.Windows.Forms.RadioButton();
            this.rdbTheUSA = new System.Windows.Forms.RadioButton();
            this.gbCapital = new System.Windows.Forms.GroupBox();
            this.rdbParis = new System.Windows.Forms.RadioButton();
            this.rdbBudapest = new System.Windows.Forms.RadioButton();
            this.rdbAnkara = new System.Windows.Forms.RadioButton();
            this.rdbLondon = new System.Windows.Forms.RadioButton();
            this.rdbMadrid = new System.Windows.Forms.RadioButton();
            this.rdbRome = new System.Windows.Forms.RadioButton();
            this.rdbTokyo = new System.Windows.Forms.RadioButton();
            this.rdbWashington = new System.Windows.Forms.RadioButton();
            this.txtThongBao = new System.Windows.Forms.TextBox();
            this.gbCountry.SuspendLayout();
            this.gbCapital.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCountry
            // 
            this.gbCountry.Controls.Add(this.rdbTheUSA);
            this.gbCountry.Controls.Add(this.rdbItaly);
            this.gbCountry.Controls.Add(this.rdbTheUK);
            this.gbCountry.Controls.Add(this.radTurkey);
            this.gbCountry.Controls.Add(this.rdbSpain);
            this.gbCountry.Controls.Add(this.rdbHungary);
            this.gbCountry.Controls.Add(this.rdbJapan);
            this.gbCountry.Controls.Add(this.rdbFrance);
            this.gbCountry.Location = new System.Drawing.Point(40, 43);
            this.gbCountry.Name = "gbCountry";
            this.gbCountry.Size = new System.Drawing.Size(169, 381);
            this.gbCountry.TabIndex = 0;
            this.gbCountry.TabStop = false;
            this.gbCountry.Text = "Country";
            // 
            // rdbFrance
            // 
            this.rdbFrance.AutoSize = true;
            this.rdbFrance.Location = new System.Drawing.Point(50, 39);
            this.rdbFrance.Name = "rdbFrance";
            this.rdbFrance.Size = new System.Drawing.Size(58, 17);
            this.rdbFrance.TabIndex = 0;
            this.rdbFrance.TabStop = true;
            this.rdbFrance.Tag = "1";
            this.rdbFrance.Text = "France";
            this.rdbFrance.UseVisualStyleBackColor = true;
            this.rdbFrance.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbJapan
            // 
            this.rdbJapan.AutoSize = true;
            this.rdbJapan.Location = new System.Drawing.Point(50, 82);
            this.rdbJapan.Name = "rdbJapan";
            this.rdbJapan.Size = new System.Drawing.Size(54, 17);
            this.rdbJapan.TabIndex = 1;
            this.rdbJapan.TabStop = true;
            this.rdbJapan.Tag = "2";
            this.rdbJapan.Text = "Japan";
            this.rdbJapan.UseVisualStyleBackColor = true;
            this.rdbJapan.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbHungary
            // 
            this.rdbHungary.AutoSize = true;
            this.rdbHungary.Location = new System.Drawing.Point(50, 125);
            this.rdbHungary.Name = "rdbHungary";
            this.rdbHungary.Size = new System.Drawing.Size(65, 17);
            this.rdbHungary.TabIndex = 2;
            this.rdbHungary.TabStop = true;
            this.rdbHungary.Tag = "3";
            this.rdbHungary.Text = "Hungary";
            this.rdbHungary.UseVisualStyleBackColor = true;
            this.rdbHungary.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbSpain
            // 
            this.rdbSpain.AutoSize = true;
            this.rdbSpain.Location = new System.Drawing.Point(50, 168);
            this.rdbSpain.Name = "rdbSpain";
            this.rdbSpain.Size = new System.Drawing.Size(52, 17);
            this.rdbSpain.TabIndex = 3;
            this.rdbSpain.TabStop = true;
            this.rdbSpain.Tag = "4";
            this.rdbSpain.Text = "Spain";
            this.rdbSpain.UseVisualStyleBackColor = true;
            this.rdbSpain.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // radTurkey
            // 
            this.radTurkey.AutoSize = true;
            this.radTurkey.Location = new System.Drawing.Point(50, 211);
            this.radTurkey.Name = "radTurkey";
            this.radTurkey.Size = new System.Drawing.Size(58, 17);
            this.radTurkey.TabIndex = 4;
            this.radTurkey.TabStop = true;
            this.radTurkey.Tag = "5";
            this.radTurkey.Text = "Turkey";
            this.radTurkey.UseVisualStyleBackColor = true;
            this.radTurkey.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbTheUK
            // 
            this.rdbTheUK.AutoSize = true;
            this.rdbTheUK.Location = new System.Drawing.Point(50, 254);
            this.rdbTheUK.Name = "rdbTheUK";
            this.rdbTheUK.Size = new System.Drawing.Size(62, 17);
            this.rdbTheUK.TabIndex = 5;
            this.rdbTheUK.TabStop = true;
            this.rdbTheUK.Tag = "6";
            this.rdbTheUK.Text = "The UK";
            this.rdbTheUK.UseVisualStyleBackColor = true;
            this.rdbTheUK.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbItaly
            // 
            this.rdbItaly.AutoSize = true;
            this.rdbItaly.Location = new System.Drawing.Point(50, 297);
            this.rdbItaly.Name = "rdbItaly";
            this.rdbItaly.Size = new System.Drawing.Size(44, 17);
            this.rdbItaly.TabIndex = 6;
            this.rdbItaly.TabStop = true;
            this.rdbItaly.Tag = "7";
            this.rdbItaly.Text = "Italy";
            this.rdbItaly.UseVisualStyleBackColor = true;
            this.rdbItaly.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbTheUSA
            // 
            this.rdbTheUSA.AutoSize = true;
            this.rdbTheUSA.Location = new System.Drawing.Point(50, 340);
            this.rdbTheUSA.Name = "rdbTheUSA";
            this.rdbTheUSA.Size = new System.Drawing.Size(69, 17);
            this.rdbTheUSA.TabIndex = 8;
            this.rdbTheUSA.TabStop = true;
            this.rdbTheUSA.Tag = "8";
            this.rdbTheUSA.Text = "The USA";
            this.rdbTheUSA.UseVisualStyleBackColor = true;
            this.rdbTheUSA.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // gbCapital
            // 
            this.gbCapital.Controls.Add(this.rdbParis);
            this.gbCapital.Controls.Add(this.rdbBudapest);
            this.gbCapital.Controls.Add(this.rdbAnkara);
            this.gbCapital.Controls.Add(this.rdbLondon);
            this.gbCapital.Controls.Add(this.rdbMadrid);
            this.gbCapital.Controls.Add(this.rdbRome);
            this.gbCapital.Controls.Add(this.rdbTokyo);
            this.gbCapital.Controls.Add(this.rdbWashington);
            this.gbCapital.Location = new System.Drawing.Point(252, 43);
            this.gbCapital.Name = "gbCapital";
            this.gbCapital.Size = new System.Drawing.Size(169, 381);
            this.gbCapital.TabIndex = 9;
            this.gbCapital.TabStop = false;
            this.gbCapital.Text = "Capital";
            // 
            // rdbParis
            // 
            this.rdbParis.AutoSize = true;
            this.rdbParis.Location = new System.Drawing.Point(50, 340);
            this.rdbParis.Name = "rdbParis";
            this.rdbParis.Size = new System.Drawing.Size(48, 17);
            this.rdbParis.TabIndex = 8;
            this.rdbParis.TabStop = true;
            this.rdbParis.Tag = "1";
            this.rdbParis.Text = "Paris";
            this.rdbParis.UseVisualStyleBackColor = true;
            this.rdbParis.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbBudapest
            // 
            this.rdbBudapest.AutoSize = true;
            this.rdbBudapest.Location = new System.Drawing.Point(50, 297);
            this.rdbBudapest.Name = "rdbBudapest";
            this.rdbBudapest.Size = new System.Drawing.Size(70, 17);
            this.rdbBudapest.TabIndex = 6;
            this.rdbBudapest.TabStop = true;
            this.rdbBudapest.Tag = "3";
            this.rdbBudapest.Text = "Budapest";
            this.rdbBudapest.UseVisualStyleBackColor = true;
            this.rdbBudapest.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbAnkara
            // 
            this.rdbAnkara.AutoSize = true;
            this.rdbAnkara.Location = new System.Drawing.Point(50, 254);
            this.rdbAnkara.Name = "rdbAnkara";
            this.rdbAnkara.Size = new System.Drawing.Size(59, 17);
            this.rdbAnkara.TabIndex = 5;
            this.rdbAnkara.TabStop = true;
            this.rdbAnkara.Tag = "5";
            this.rdbAnkara.Text = "Ankara";
            this.rdbAnkara.UseVisualStyleBackColor = true;
            this.rdbAnkara.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbLondon
            // 
            this.rdbLondon.AutoSize = true;
            this.rdbLondon.Location = new System.Drawing.Point(50, 211);
            this.rdbLondon.Name = "rdbLondon";
            this.rdbLondon.Size = new System.Drawing.Size(61, 17);
            this.rdbLondon.TabIndex = 4;
            this.rdbLondon.TabStop = true;
            this.rdbLondon.Tag = "6";
            this.rdbLondon.Text = "London";
            this.rdbLondon.UseVisualStyleBackColor = true;
            this.rdbLondon.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbMadrid
            // 
            this.rdbMadrid.AutoSize = true;
            this.rdbMadrid.Location = new System.Drawing.Point(50, 168);
            this.rdbMadrid.Name = "rdbMadrid";
            this.rdbMadrid.Size = new System.Drawing.Size(57, 17);
            this.rdbMadrid.TabIndex = 3;
            this.rdbMadrid.TabStop = true;
            this.rdbMadrid.Tag = "4";
            this.rdbMadrid.Text = "Madrid";
            this.rdbMadrid.UseVisualStyleBackColor = true;
            this.rdbMadrid.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbRome
            // 
            this.rdbRome.AutoSize = true;
            this.rdbRome.Location = new System.Drawing.Point(50, 125);
            this.rdbRome.Name = "rdbRome";
            this.rdbRome.Size = new System.Drawing.Size(53, 17);
            this.rdbRome.TabIndex = 2;
            this.rdbRome.TabStop = true;
            this.rdbRome.Tag = "7";
            this.rdbRome.Text = "Rome";
            this.rdbRome.UseVisualStyleBackColor = true;
            this.rdbRome.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbTokyo
            // 
            this.rdbTokyo.AutoSize = true;
            this.rdbTokyo.Location = new System.Drawing.Point(50, 82);
            this.rdbTokyo.Name = "rdbTokyo";
            this.rdbTokyo.Size = new System.Drawing.Size(55, 17);
            this.rdbTokyo.TabIndex = 1;
            this.rdbTokyo.TabStop = true;
            this.rdbTokyo.Tag = "2";
            this.rdbTokyo.Text = "Tokyo";
            this.rdbTokyo.UseVisualStyleBackColor = true;
            this.rdbTokyo.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // rdbWashington
            // 
            this.rdbWashington.AutoSize = true;
            this.rdbWashington.Location = new System.Drawing.Point(50, 39);
            this.rdbWashington.Name = "rdbWashington";
            this.rdbWashington.Size = new System.Drawing.Size(106, 17);
            this.rdbWashington.TabIndex = 0;
            this.rdbWashington.TabStop = true;
            this.rdbWashington.Tag = "8";
            this.rdbWashington.Text = "Washington, D.C";
            this.rdbWashington.UseVisualStyleBackColor = true;
            this.rdbWashington.CheckedChanged += new System.EventHandler(this.KiemTra);
            // 
            // txtThongBao
            // 
            this.txtThongBao.Location = new System.Drawing.Point(40, 444);
            this.txtThongBao.Multiline = true;
            this.txtThongBao.Name = "txtThongBao";
            this.txtThongBao.Size = new System.Drawing.Size(381, 47);
            this.txtThongBao.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(461, 515);
            this.Controls.Add(this.txtThongBao);
            this.Controls.Add(this.gbCapital);
            this.Controls.Add(this.gbCountry);
            this.Name = "Form1";
            this.Text = "Capital of Country";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbCountry.ResumeLayout(false);
            this.gbCountry.PerformLayout();
            this.gbCapital.ResumeLayout(false);
            this.gbCapital.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCountry;
        private System.Windows.Forms.RadioButton rdbTheUSA;
        private System.Windows.Forms.RadioButton rdbItaly;
        private System.Windows.Forms.RadioButton rdbTheUK;
        private System.Windows.Forms.RadioButton radTurkey;
        private System.Windows.Forms.RadioButton rdbSpain;
        private System.Windows.Forms.RadioButton rdbHungary;
        private System.Windows.Forms.RadioButton rdbJapan;
        private System.Windows.Forms.RadioButton rdbFrance;
        private System.Windows.Forms.GroupBox gbCapital;
        private System.Windows.Forms.RadioButton rdbParis;
        private System.Windows.Forms.RadioButton rdbBudapest;
        private System.Windows.Forms.RadioButton rdbAnkara;
        private System.Windows.Forms.RadioButton rdbLondon;
        private System.Windows.Forms.RadioButton rdbMadrid;
        private System.Windows.Forms.RadioButton rdbRome;
        private System.Windows.Forms.RadioButton rdbTokyo;
        private System.Windows.Forms.RadioButton rdbWashington;
        private System.Windows.Forms.TextBox txtThongBao;
    }
}

