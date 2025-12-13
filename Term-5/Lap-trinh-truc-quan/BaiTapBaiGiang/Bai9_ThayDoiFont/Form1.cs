using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai9_ThayDoiFont
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void CapNhatFont()
        {
            string fontName = txtKetQua.Font.FontFamily.Name;
            if (rdbFont1.Checked) fontName = "Times New Roman";
            else if (rdbFont2.Checked) fontName = "Calibri";
            else if (rdbFont3.Checked) fontName = "Consolas";
            else if (rdbFont4.Checked) fontName = "Tahoma";

            FontStyle style = FontStyle.Regular;
            if (chbBold.Checked) style |= FontStyle.Bold;
            if (chbItalic.Checked) style |= FontStyle.Italic;
            if (chbUnderline.Checked) style |= FontStyle.Underline;
            if (chbStrikeout.Checked) style |= FontStyle.Strikeout;

            float size = txtKetQua.Font.Size;

            txtKetQua.Font = new Font(fontName, size, style);
        }

        private void rdbFontChanged(object sender, EventArgs e)
        {
            CapNhatFont();
        }

        private void chbEffectChanged(object sender, EventArgs e)
        {
            CapNhatFont();
        }

        private void rdbColorChanged(object sender, EventArgs e)
        {
            if (rdbRed.Checked)
            {
                txtKetQua.ForeColor = Color.Red;
            }
            else if (rdbBlue.Checked)
            {
                txtKetQua.ForeColor = Color.Blue;
            }
            else if (rdbGreen.Checked)
            {
                txtKetQua.ForeColor = Color.Green;
            }
            else if (rdbViolet.Checked)
            {
                txtKetQua.ForeColor = Color.Violet;
            }
        }
    }
}
