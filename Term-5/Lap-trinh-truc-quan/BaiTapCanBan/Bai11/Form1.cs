using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cbFont.Items.AddRange(new string[]
            //{
            //    ".vnTime",
            //    ".vnTimeH",
            //    ".vnArial",
            //    ".vnArialH",
            //    ".vnUniverse",
            //    ".vnUniverseH"
            //});

            cbFont.Items.AddRange(new string[]
            {
                "Arial",
                "Calibri",
                "Consolas",
                "Verdana",
                "Tahoma",
                "Sans Serif Collection"
            });

            for (int size = 14; size <= 24; size++)
            {
                cbSize.Items.Add(size.ToString());
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            chbDam.Checked = false;
            chbNghieng.Checked = false;
            chbGachChan.Checked = false;

            rdbDen.Checked = false;
            rdbDo.Checked = false;
            rdbXanhLa.Checked = false;

            cbFont.SelectedIndex = -1;
            cbSize.SelectedIndex = -1;

            txtNoiDung.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            txtNoiDung.ForeColor = Color.Black;
        }

        private void changeFontAndSize()
        {
            if (cbFont.SelectedIndex != -1 && cbSize.SelectedIndex != -1)
            {
                string fontName = cbFont.SelectedItem.ToString();
                float fontSize = float.Parse(cbSize.SelectedItem.ToString());

                FontStyle style = txtNoiDung.Font.Style;
                Font newFont = new Font(fontName, fontSize, style);

                txtNoiDung.Font = newFont;
            }
        }

        private void btnFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFontAndSize();
        }

        private void btnSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFontAndSize();
        }

        private void changeFontStyle()
        {
            FontStyle style = FontStyle.Regular;
            if (chbDam.Checked)
            {
                style |= FontStyle.Bold;
            }
            if (chbNghieng.Checked)
            {
                style |= FontStyle.Italic;
            }
            if (chbGachChan.Checked)
            {
                style |= FontStyle.Underline;
            }
            string fontName = txtNoiDung.Font.Name;
            float fontSize = txtNoiDung.Font.Size;
            txtNoiDung.Font = new Font(fontName, fontSize, style);
        }

        private void chbDam_CheckedChanged(object sender, EventArgs e)
        {
            changeFontStyle();
        }

        private void chbNghieng_CheckedChanged(object sender, EventArgs e)
        {
            changeFontStyle();
        }

        private void chbGachChan_CheckedChanged(object sender, EventArgs e)
        {
            changeFontStyle();
        }

        private void changeFontColor()
        {
            if (rdbDen.Checked)
            {
                txtNoiDung.ForeColor = Color.Black;
            }
            else if (rdbDo.Checked)
            {
                txtNoiDung.ForeColor = Color.Red;
            }
            else if (rdbXanhLa.Checked)
            {
                txtNoiDung.ForeColor = Color.Green;
            }
        }

        private void rdbDen_CheckedChanged(object sender, EventArgs e)
        {
            changeFontColor();
        }

        private void rdbDo_CheckedChanged(object sender, EventArgs e)
        {
            changeFontColor();
        }

        private void rdbXanhLa_CheckedChanged(object sender, EventArgs e)
        {
            changeFontColor();
        }
    }
}
