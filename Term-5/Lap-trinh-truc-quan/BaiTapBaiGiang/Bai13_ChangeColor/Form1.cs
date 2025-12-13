using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai13_ChangeColor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ColorChange(sender, e);
        }

        private void ColorChange(object sender, EventArgs e)
        {
            int r, b, g;
            
            r = hsbRed.Value;
            b = hsbGreen.Value;
            g = hsbBlue.Value;

            txtBox.BackColor = Color.FromArgb(r, g, b); 

            lblRed.Text = r.ToString();
            lblGreen.Text = b.ToString();
            lblBlue.Text = g.ToString();
        }
    }
}
