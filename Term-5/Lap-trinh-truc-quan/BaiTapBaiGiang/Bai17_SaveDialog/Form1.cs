using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bai17_SaveDialog
{
    public partial class frmSave : Form
    {
        public frmSave()
        {
            InitializeComponent();
        }

        private void frmSave_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();

            dlgSave.Filter = "Text file(*.txt)|*.txt |Word Document(*.doc)| *.doc | All files(*.*) | *.* ";
            dlgSave.InitialDirectory = "E:\\00_UNIVERSITY\\TERM_05\\LapTrinhTrucQuan\\BaiTapBaiGiang\\Bai17_SaveDialog";
            dlgSave.FilterIndex = 2;
            dlgSave.Title = "Chọn file để lưu"; 
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".doc";

            if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(dlgSave.FileName);
                try
                {
                    file.Write(txtSave.Text);
                    MessageBox.Show("Lưu file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể lưu file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                file.Close();
            }
            else
            {
                MessageBox.Show("Bạn chọn Cancle!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }
    }
}
