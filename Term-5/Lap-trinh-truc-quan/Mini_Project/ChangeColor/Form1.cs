using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeColor
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
            int r, g, b;
            r = hsbRed.Value;
            g = hsbGreen.Value;
            b = hsbBlue.Value;

            tbColor.BackColor = Color.FromArgb(r, g, b);
            
            lbRed.Text = r.ToString();
            lbGreen.Text = g.ToString();
            lbBlue.Text = b.ToString();
        }
    }
}
