using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai10_GhepQuocGiavoiThuDo
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

        private void EmptyOption()
        {
            foreach (Control ctrl in gbCapital.Controls)
            {
                RadioButton rdb = ctrl as RadioButton;
                if (rdb != null)
                {
                    rdb.Checked = false;
                }
            }
        }

        private void KiemTra(object sender, EventArgs e)
        {
            RadioButton selectedCountry = gbCountry.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            RadioButton selectedCapital = gbCapital.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (selectedCountry == null && selectedCapital == null)
            {
                txtThongBao.Text = "Hãy chọn cả quốc gia và thủ đô!";
                return;
            }
            else if (selectedCountry != null && selectedCapital == null)
            {
                txtThongBao.Text = "Hãy chọn thủ đô cho " + selectedCountry.Text;
                EmptyOption();  
                return;
            }

            if (selectedCountry.Tag.ToString() == selectedCapital.Tag.ToString())
            {
                txtThongBao.Text = "Chúc mừng bạn đã chọn đúng! Thủ dô của " + selectedCountry.Text + " là " + selectedCapital.Text;
            }
            else
            {
                txtThongBao.Text = "Rất tiếc! Bạn đã chọn sai! Thủ đô của " + selectedCountry.Text + " không phải là " + selectedCapital.Text;
            }
        }
    }
}
