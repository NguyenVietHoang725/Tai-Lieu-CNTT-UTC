using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai18_ColorDialog
{
    public partial class frmColor : Form
    {
        public frmColor()
        {
            InitializeComponent();
        }

        private void frmColor_Load(object sender, EventArgs e)
        {

        }

        private void btnMauNen_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();

            dlgColor.FullOpen = true;

            if (dlgColor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BackColor = dlgColor.Color;
            }
            else
            {
                MessageBox.Show("Bạn đã click Cancel", "Color Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
