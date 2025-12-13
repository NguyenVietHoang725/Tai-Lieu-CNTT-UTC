using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai15_DongHoDemNguoc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrTimer.Stop();
            ResetTimer();
        }

        private void ResetTimer()
        {
            txtMinute.Text = "5";
            txtSecond.Text = "30";
        }

        private void btnChay_Click(object sender, EventArgs e)
        {
            tmrTimer.Start();
        }

        private void btnTamDung_Click(object sender, EventArgs e)
        {
            tmrTimer.Stop();
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            tmrTimer.Stop();
            ResetTimer();
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            int minute = int.Parse(txtMinute.Text);
            int second = int.Parse(txtSecond.Text);

            if (second == 0)
            {
                if (minute == 0)
                {
                    tmrTimer.Stop();
                    MessageBox.Show("Hết giờ!");
                }
                else
                {
                    minute--;
                    second = 59;
                }
            }
            else
            {
                second--;
            }

            txtMinute.Text = minute.ToString();
            txtSecond.Text = second.ToString();
        }
    }
}
