using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai12_GiaiPTBacNhat
{
    public partial class frmPTBN : Form
    {
        public frmPTBN()
        {
            InitializeComponent();
        }

        private void frmPTBN_Load(object sender, EventArgs e)
        {

        }

        private void btnGiai_Click(object sender, EventArgs e)
        {
            int a, b;
            float x;
            string str = "A = " + nudA.Value.ToString() + "; B = " + nudB.Value.ToString();

            a = Convert.ToInt32(nudA.Value);
            b = Convert.ToInt32(nudB.Value);

            if (a == 0)
            {
                if (b == 0)
                    str = str + ". Phương trình có vô số nghiệm!";
                else
                    str = str + ". Phương trình vô nghiệm!";
            }
            else
            {
                x = (float)-b / a;
                str = str + ". Phương trình có nghiệm x = " + x.ToString();
            }
            txtKetQua.Text = str;
        }
    }
}
