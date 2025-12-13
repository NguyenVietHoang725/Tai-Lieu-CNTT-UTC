using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinhLaiSuatTienGuiTietKiem
{
    public partial class Form2 : Form
    {
        List<NguoiGui> listNguoiGuis = new List<NguoiGui>();
        public Form2()
        {
            InitializeComponent();
            listNguoiGuis = StaticData._NguoiGui;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            lstThongTinTimKiem.Items.Clear(); // xóa kết quả cũ
            int timthay = 0;
            int maKH;
            if (!int.TryParse(txtTimKiem.Text, out maKH))
            {
                MessageBox.Show("Mã KH phải là số!");
                return;
            }

            foreach (NguoiGui i in listNguoiGuis)
            {
                if (i.MaKH1 == maKH)
                {
                    timthay = 1;
                    lstThongTinTimKiem.Items.Add(
                        $"Khách hàng {i.TenKH1} phải trả {i.Tien1} nghìn đồng."
                    );
                }
            }

            if (timthay == 0)
            {
                lstThongTinTimKiem.Items.Add(
                    $"Khách hàng {txtTimKiem.Text} không tồn tại trong danh sách."
                );
            }
        }

    }
}
