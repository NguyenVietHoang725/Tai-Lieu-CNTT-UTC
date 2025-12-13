using Bai4.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4.FormStorage
{
    public partial class FormBaiTapDienTu : Form
    {
        private BaiTapDienTu _bai;

        public FormBaiTapDienTu(BaiTapDienTu bai)
        {
            InitializeComponent();
            _bai = bai;
        }

        private void BaiTapDienTu_Load(object sender, EventArgs e)
        {
            HienThiBaiTap();

            // Gắn sự kiện cho 4 nút
            btnOK.Click += BtnOK_Click;
            btnDapAn.Click += BtnDapAn_Click;
            btnLamLai.Click += BtnLamLai_Click;
            btnThoat.Click += BtnThoat_Click;
        }

        private void HienThiBaiTap()
        {
            // Hiển thị đề bài
            txtDeBai.Text = _bai.DeBai;

            // Xóa nội dung cũ
            foreach (var tb in LayTatCaTextBoxDapAn())
            {
                tb.Text = string.Empty;
                tb.Enabled = false;
                tb.Visible = false;
            }

            // Chỉ bật số textbox đúng bằng số đáp án
            var listTextBox = LayTatCaTextBoxDapAn().ToList();
            for (int i = 0; i < _bai.DapAnDung.Count && i < listTextBox.Count; i++)
            {
                listTextBox[i].Enabled = true;
                listTextBox[i].Visible = true;
            }
        }

        // ✅ Nút OK: Chấm điểm
        private void BtnOK_Click(object sender, EventArgs e)
        {
            _bai.DapAnNguoiDung.Clear();

            foreach (var tb in LayTatCaTextBoxDapAn())
            {
                if (tb.Enabled)
                    _bai.DapAnNguoiDung.Add(tb.Text.Trim());
            }

            int diem = _bai.TinhDiem();
            MessageBox.Show($"Bạn làm đúng {diem}/{_bai.DapAnDung.Count} câu.", "Kết quả");
        }

        // ✅ Nút Đáp án: Hiển thị đáp án đúng vào các textbox
        private void BtnDapAn_Click(object sender, EventArgs e)
        {
            var listTextBox = LayTatCaTextBoxDapAn().ToList();

            for (int i = 0; i < _bai.DapAnDung.Count && i < listTextBox.Count; i++)
            {
                listTextBox[i].Text = _bai.DapAnDung[i];
            }
        }

        // ✅ Nút Làm lại: Xóa toàn bộ câu trả lời
        private void BtnLamLai_Click(object sender, EventArgs e)
        {
            foreach (var tb in LayTatCaTextBoxDapAn())
            {
                if (tb.Enabled)
                    tb.Text = string.Empty;
            }
        }

        // ✅ Nút Thoát: Quay về form chính
        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Lấy danh sách 10 TextBox đáp án theo thứ tự
        private IEnumerable<TextBox> LayTatCaTextBoxDapAn()
        {
            return groupBoxDapAn.Controls
                .OfType<TextBox>()
                .OrderBy(tb => tb.Name); // txtDapAn1, txtDapAn2, ...
        }
    }
}
