using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanhSachChatLieu
{
    public partial class frmChatLieu : Form
    {
        ProcessDataBase db = new ProcessDataBase();

        public frmChatLieu()
        {
            InitializeComponent();
        }

         private void frmChatLieu_Load(object sender, EventArgs e)
        {
            DataTable dtChatLieu = db.DocBang("select * from tblChatLieu");

            dtChatLieu.Columns["MaCL"].Caption = "Mã chất liệu";
            dtChatLieu.Columns["TenCL"].Caption = "Tên chất liệu";
            
            dgvChatLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvChatLieu.DataSource = dtChatLieu;
        }
    }
}
