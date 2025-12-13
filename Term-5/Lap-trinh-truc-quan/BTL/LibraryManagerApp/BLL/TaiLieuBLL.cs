using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.BLL
{
    internal class TaiLieuBLL
    {
        private TaiLieuDAL _dal = new TaiLieuDAL();
        private NxbBLL _nxbBll = new NxbBLL();
        private TheLoaiBLL _thlBll = new TheLoaiBLL();
        private DinhDangBLL _ddBll = new DinhDangBLL();
        private TacGiaBLL _tgBll = new TacGiaBLL();
        private NgonNguBLL _nnBll = new NgonNguBLL();

        // READ
        public List<TaiLieuDTO> LayTatCaTaiLieu()
        {
            return _dal.GetAllTaiLieuDTO();
        }

        public TaiLieuDTO LayChiTietTaiLieu(string maTL)
        {
            return _dal.GetTaiLieuByMaTL(maTL);
        }

        // Hàm hỗ trợ Load ComboBox Danh mục
        public List<NxbDTO> LayTatCaNxb() { return _nxbBll.LayTatCaNxb(); }
        public List<TheLoaiDTO> LayTatCaTheLoai() { return _thlBll.LayTatCaTheLoai(); }
        public List<DinhDangDTO> LayTatCaDinhDang() { return _ddBll.LayTatCaDinhDang(); }
        public List<TacGiaDTO> LayTatCaTacGia() { return _tgBll.LayTatCaTacGia(); }
        public TacGiaDTO LayChiTietTacGia(string maTG) { return _tgBll.LayChiTietTacGia(maTG); }
        public List<NgonNguDTO> LayTatCaNgonNgu() { return _nnBll.LayTatCaNgonNgu(); }

        // Hàm nghiệp vụ: Thêm mới Tài liệu
        public string ThemTaiLieu(TaiLieuDTO model)
        {
            // 1. Kiểm tra MaTK (người nhập) từ Session
            if (string.IsNullOrEmpty(model.MaTK))
            {
                throw new InvalidOperationException("Không tìm thấy Mã Tài Khoản người nhập (MaTK) trong phiên làm việc.");
            }

            // 2. Gọi DAL để sinh Mã TL (MaTL = TL[MaQG][YY]-[###])
            try
            {
                string newMaTL = _dal.GenerateNewMaTL(model.MaNXB);

                if (string.IsNullOrEmpty(newMaTL))
                {
                    return string.Empty; // Lỗi sinh mã không rõ nguyên nhân
                }

                // 3. Gán Mã TL đã sinh vào model
                model.MaTL = newMaTL;

                // 4. Thực hiện Insert Tài liệu chính
                if (!_dal.InsertTaiLieu(model))
                {
                    return string.Empty; // Lỗi DB khi Insert
                }

                return newMaTL;
            }
            catch (Exception ex)
            {
                // Bắt lỗi RAISERROR từ SP hoặc lỗi hệ thống khác
                Console.WriteLine("Lỗi nghiệp vụ/hệ thống khi thêm Tài liệu: " + ex.Message);
                return null; // Trả về null để báo hiệu lỗi hệ thống/nghiệp vụ
            }
        }

        // Hàm mới: Lưu danh sách tác giả (Bước 4)
        public bool LuuDanhSachTacGia(string maTL, List<TL_TGDTO> danhSachTacGia)
        {
            if (danhSachTacGia == null || danhSachTacGia.Count == 0)
            {
                return true; // Không có tác giả nào để thêm
            }

            try
            {
                foreach (var tacGia in danhSachTacGia)
                {
                    tacGia.MaTL = maTL; // Đảm bảo MaTL là mã vừa sinh ra
                    _dal.InsertTL_TG(tacGia);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL khi lưu danh sách tác giả: " + ex.Message);
                return false;
            }
        }

        // Hàm nghiệp vụ: Xóa Tài liệu
        public bool XoaTaiLieu(string maTL)
        {
            // Logic nghiệp vụ (Kiểm tra xem tài liệu có đang được mượn không,...)
            // Hiện tại, ta chỉ gọi DAL.
            return _dal.DeleteTaiLieu(maTL);
        }

        // Hàm nghiệp vụ: Cập nhật Tài liệu và Đồng bộ Tác giả
        public bool CapNhatTaiLieu(TaiLieuDTO model, List<TL_TGDTO> danhSachTacGiaMoi)
        {
            try
            {
                // 1. Cập nhật thông tin Tài liệu chính
                if (!_dal.UpdateTaiLieu(model))
                {
                    return false; // Lỗi cập nhật bảng chính
                }

                // 2. Xóa tất cả Tác giả đính kèm cũ
                if (!_dal.DeleteAllTacGiaByMaTL(model.MaTL))
                {
                    // Lỗi nghiêm trọng: Không thể xóa tác giả cũ
                    // Cần xem xét việc sử dụng Transaction ở đây
                    return false;
                }

                // 3. Thêm lại danh sách Tác giả mới (Sử dụng lại hàm LuuDanhSachTacGia)
                if (!LuuDanhSachTacGia(model.MaTL, danhSachTacGiaMoi))
                {
                    return false; // Lỗi khi thêm lại tác giả mới
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL khi cập nhật Tài liệu: " + ex.Message);
                return false;
            }
        }

        // Hàm nghiệp vụ: Cập nhật đường dẫn ảnh
        public bool CapNhatDuongDanAnh(string maTL, string imagePath)
        {
            // imagePath có thể là null (nếu người dùng Xóa ảnh)
            return _dal.UpdateImagePath(maTL, imagePath);
        }
        // Hàm tìm kiếm
        public List<TaiLieuDTO> TimKiemTaiLieu(List<SearchFilter> filters)
        {
            return _dal.SearchTaiLieu(filters);
        }

        // Hàm cung cấp Metadata cho UI (FrmTimKiem)
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetTaiLieuFields();
        }

        // [MỚI] Hàm lấy dữ liệu chuyên dụng cho xuất Excel
        public List<TaiLieuDTO> LayDuLieuXuatExcel()
        {
            // 1. Lấy danh sách tài liệu cơ bản
            List<TaiLieuDTO> listTaiLieu = _dal.GetAllTaiLieuDTO();

            // 2. Lấy toàn bộ danh sách tác giả đính kèm
            List<TL_TGDTO> listAllAuthors = _dal.GetAllAuthorLinks();

            // 3. Ghép chuỗi tác giả vào từng tài liệu (Xử lý trên RAM)
            foreach (var tl in listTaiLieu)
            {
                // Tìm các tác giả thuộc tài liệu này
                var authorsOfBook = listAllAuthors
                                    .Where(x => x.MaTL == tl.MaTL)
                                    .Select(x => $"{x.HoTenTG} ({x.VaiTro})"); // Format: Tên (Vai trò)

                // Nối lại thành 1 chuỗi, cách nhau dấu phẩy
                tl.TacGiaExcel = string.Join(", ", authorsOfBook);
            }

            return listTaiLieu;
        }
    }
}
