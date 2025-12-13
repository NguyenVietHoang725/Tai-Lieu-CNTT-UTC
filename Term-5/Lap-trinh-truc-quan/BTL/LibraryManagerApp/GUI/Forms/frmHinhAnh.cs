// File: LibraryManagerApp.GUI.Forms/frmHinhAnh.cs

using LibraryManagerApp.BLL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.Forms
{
    public partial class frmHinhAnh : Form
    {
        private TaiLieuBLL _bll = new TaiLieuBLL();

        // Đường dẫn TƯƠNG ĐỐI (tính từ thư mục Project)
        private const string IMAGE_DIRECTORY = @"Resources\img\bookcovers";

        private string _maTL;
        private string _currentRelativePath;

        // Biến tạm
        private string _newSourcePath = null;
        private string _newRelativePath = null;

        #region KHỞI TẠO VÀ LOAD FORM

        public frmHinhAnh(string maTL, string currentImagePath)
        {
            InitializeComponent();

            _maTL = maTL;
            _currentRelativePath = currentImagePath;
            _newRelativePath = null;
            _newSourcePath = null;

            pboHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            txtDuongDan.ReadOnly = true;

            // Liên kết sự kiện
            this.Load += frmHinhAnh_Load;
            btnThem.Click += btnThem_Click;
            btnThayDoi.Click += btnThayDoi_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
        }

        private void frmHinhAnh_Load(object sender, EventArgs e)
        {
            LoadImageToPictureBox(_currentRelativePath);
            UpdateButtonStates();
        }

        #endregion

        #region XỬ LÝ SỰ KIỆN NÚT (CHỌN/XÓA ẢNH)

        private void btnThem_Click(object sender, EventArgs e)
        {
            SelectNewImage();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            SelectNewImage();
        }

        private void SelectNewImage()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn hình ảnh cho tài liệu";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _newSourcePath = ofd.FileName;

                    try
                    {
                        // 1. Tạo đường dẫn TƯƠNG ĐỐI (để lưu vào DB)
                        string extension = Path.GetExtension(_newSourcePath);
                        _newRelativePath = Path.Combine(IMAGE_DIRECTORY, _maTL + extension);

                        // 2. Tải ảnh mới vào PictureBox (từ đường dẫn TUYỆT ĐỐI NGUỒN)
                        LoadImageToPictureBox(_newSourcePath);

                        // 3. Cập nhật UI
                        txtDuongDan.Text = _newRelativePath + " (Chưa lưu)";
                        btnLuu.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hình ảnh này khỏi tài liệu?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (pboHinhAnh.Image != null)
                {
                    pboHinhAnh.Image.Dispose();
                    pboHinhAnh.Image = pboHinhAnh.ErrorImage;
                }

                txtDuongDan.Text = "(Ảnh đã bị xóa - Chưa lưu)";

                // Đặt đường dẫn mới là null (báo hiệu cho btnLuu)
                _newRelativePath = null;
                _newSourcePath = null;

                btnLuu.Enabled = true;
                UpdateButtonStates();
            }
        }

        #endregion

        #region XỬ LÝ LƯU THAY ĐỔI (FILE VÀ DB)

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. XỬ LÝ FILE VẬT LÝ

                // Trường hợp 1: Chọn ảnh mới (Thêm mới hoặc Thay đổi)
                if (_newSourcePath != null && _newRelativePath != null)
                {
                    // 1a. Xóa ảnh cũ (nếu có) khỏi thư mục Project
                    DeleteImageFile(_currentRelativePath);

                    // 1b. Sao chép ảnh mới vào thư mục Project
                    CopyNewImage(_newSourcePath, _newRelativePath);
                }
                // Trường hợp 2: Xóa ảnh (và không chọn ảnh mới)
                else if (_newSourcePath == null && _currentRelativePath != null)
                {
                    // 1a. Xóa ảnh cũ
                    DeleteImageFile(_currentRelativePath);
                }

                // 2. CẬP NHẬT CSDL
                // _newRelativePath (là "Resources\img\bookcovers\...") hoặc null
                if (_bll.CapNhatDuongDanAnh(_maTL, _newRelativePath))
                {
                    MessageBox.Show("Cập nhật hình ảnh thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi khi cập nhật đường dẫn ảnh vào CSDL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region HÀM BỔ TRỢ (FILE, LOAD, UI)

        // Hàm sao chép file ảnh mới vào thư mục Project
        private void CopyNewImage(string sourcePath, string relativeDestPath)
        {
            // Lấy đường dẫn tuyệt đối của thư mục Project
            string destPath = GetFullAbsolutePath(relativeDestPath);

            string directory = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.Copy(sourcePath, destPath, true);
        }

        // Hàm xóa file ảnh cũ
        private void DeleteImageFile(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            string fullPath = GetFullAbsolutePath(relativePath);
            if (File.Exists(fullPath))
            {
                try
                {
                    if (pboHinhAnh.Image != null)
                    {
                        pboHinhAnh.Image.Dispose();
                        pboHinhAnh.Image = null;
                    }
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Không thể xóa file cũ: " + ex.Message);
                }
            }
        }

        // Hàm tải ảnh vào PictureBox
        private void LoadImageToPictureBox(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                pboHinhAnh.Image = pboHinhAnh.ErrorImage;
                txtDuongDan.Text = "(Không có ảnh)";
                _currentRelativePath = null;
                return;
            }

            string absolutePath = path;

            // 1. Kiểm tra: là tương đối (từ DB) hay tuyệt đối (từ OpenFileDialog)?
            if (!Path.IsPathRooted(path))
            {
                _currentRelativePath = path; // Lưu đường dẫn tương đối
                absolutePath = GetFullAbsolutePath(path); // Chuyển sang tuyệt đối (Project)
            }
            // Nếu là tuyệt đối (do user mới chọn), _currentRelativePath giữ nguyên giá trị cũ.

            if (string.IsNullOrEmpty(absolutePath) || !File.Exists(absolutePath))
            {
                pboHinhAnh.Image = pboHinhAnh.ErrorImage;
                txtDuongDan.Text = "(Ảnh không tìm thấy)";
                _currentRelativePath = null;
                return;
            }

            try
            {
                // Dùng MemoryStream để tải ảnh mà không khóa file
                byte[] imageData = File.ReadAllBytes(absolutePath);
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pboHinhAnh.Image = Image.FromStream(ms);
                }

                // Hiển thị đường dẫn tương đối (nếu là ảnh từ DB)
                if (!string.IsNullOrEmpty(_currentRelativePath) && Path.IsPathRooted(path) == false)
                {
                    txtDuongDan.Text = _currentRelativePath;
                }
            }
            catch (Exception ex)
            {
                pboHinhAnh.Image = pboHinhAnh.ErrorImage;
                txtDuongDan.Text = "(Lỗi tải ảnh)";
                Console.WriteLine("Lỗi LoadImageToPictureBox: " + ex.Message);
            }
        }

        // === ĐIỀU CHỈNH CỐT LÕI ===
        // Chuyển đường dẫn tương đối sang đường dẫn tuyệt đối (trỏ về Project)
        private string GetFullAbsolutePath(string relativePath)
        {
            // 1. Lấy thư mục thực thi (VD: .../bin/Debug/)
            string startupPath = Application.StartupPath;

            // 2. Đi ngược 2 cấp để lấy thư mục gốc của Project
            // (...\bin\Debug\ -> ...\bin\ -> ...\LibraryManagerApp\)
            string projectPath = Path.GetFullPath(Path.Combine(startupPath, @"..\..\"));

            // 3. Kết hợp thư mục Project với đường dẫn tương đối
            // (VD: ...\LibraryManagerApp\ + Resources\img\bookcovers\...)
            return Path.Combine(projectPath, relativePath);
        }

        // Cập nhật trạng thái các nút
        private void UpdateButtonStates()
        {
            bool coAnhHienTai = !string.IsNullOrEmpty(_currentRelativePath);

            btnThem.Enabled = !coAnhHienTai;
            btnThayDoi.Enabled = coAnhHienTai;
            btnXoa.Enabled = coAnhHienTai;

            btnLuu.Enabled = false;
        }

        #endregion
    }
}