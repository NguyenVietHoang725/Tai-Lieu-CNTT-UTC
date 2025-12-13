using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai14_DiChuyenLapLai
{
    public partial class FrmVDTimer : Form
    {
        bool hp = true;

        public FrmVDTimer()
        {
            InitializeComponent();
        }

        private void FrmVDTimer_Load(object sender, EventArgs e)
        {
            hp = true;
            tmrTimer.Start();
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            if (hp)
            {
                if ((lblMove.Left + lblMove.Width) < this.ClientSize.Width)
                    lblMove.Left += 10;
                else
                    hp = false;
            }
            else
            {
                if (lblMove.Left > 0)
                    lblMove.Left -= 10;
                else
                    hp = true;
            }
        }
    }
}
