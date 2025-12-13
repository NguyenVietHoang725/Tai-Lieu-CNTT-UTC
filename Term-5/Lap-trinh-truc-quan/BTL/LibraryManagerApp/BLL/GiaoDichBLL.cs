using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.BLL
{
    // DTO nội bộ (hoặc DTO riêng) để kiểm tra bản sao
    public class BanSaoKiemTraDTO
    {
        public string MaBS { get; set; }
        public string TenTL { get; set; }
        public string TrangThai { get; set; }
    }

    internal class GiaoDichBLL
    {
        private GiaoDichDAL _dal = new GiaoDichDAL();
        private TheBanDocDAL _theBanDocDAL = new TheBanDocDAL();
        private BanSaoDAL _banSaoDAL = new BanSaoDAL();
        private TaiLieuDAL _taiLieuDAL = new TaiLieuDAL(); // Cần thiết để lấy TenTL

        // READ
        public List<GiaoDichDTO> LayTatCaGiaoDich()
        {
            return _dal.GetAllGiaoDich();
        }

        public List<GiaoDich_BanSaoDTO> LayChiTietGiaoDich(string maGD)
        {
            return _dal.GetChiTietGiaoDich(maGD);
        }

        // CREATE (Flow chính)
        public string LapPhieuMuon(GiaoDichDTO giaoDich, List<GiaoDich_BanSaoDTO> chiTiet)
        {
            // BLL nên xử lý Transaction
            try
            {
                // 1. Sinh Mã GD
                string newMaGD = _dal.GenerateNewMaGD();
                giaoDich.MaGD = newMaGD;

                // 2. Lưu Giao dịch chính
                if (!_dal.InsertGiaoDich(giaoDich))
                {
                    throw new Exception("Không thể lưu Giao dịch chính.");
                }

                // 3. Lưu Chi tiết (Bảng phụ)
                foreach (var item in chiTiet)
                {
                    item.MaGD = newMaGD;
                    if (!_dal.InsertGiaoDich_BanSao(item))
                    {
                        throw new Exception($"Không thể lưu chi tiết Bản sao {item.MaBS}.");
                    }

                    // 4. Cập nhật trạng thái tBanSao
                    _banSaoDAL.UpdateTrangThaiBanSao(item.MaBS, "Không có sẵn");
                }

                return newMaGD;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Trả về null nếu thất bại
            }
        }

        // Hỗ trợ kiểm tra
        public TheBanDocDTO KiemTraTheBanDoc(string maTBD)
        {
            TheBanDocDTO the = _theBanDocDAL.GetTheBanDocForMuon(maTBD);
            if (the == null)
            {
                throw new Exception("Mã thẻ không tồn tại.");
            }
            if (the.TrangThai != "Hoạt động")
            {
                throw new Exception($"Thẻ đang ở trạng thái '{the.TrangThai}'. Không thể mượn.");
            }

            // (Thêm logic kiểm tra nợ sách quá hạn nếu cần)

            return the;
        }

        public BanSaoKiemTraDTO KiemTraBanSao(string maBS)
        {
            // 1. Lấy thông tin cơ bản của bản sao
            BanSaoDTO bs = _banSaoDAL.GetBanSaoByMaBS(maBS); // Dùng hàm GetDetail
            if (bs == null)
            {
                throw new Exception("Mã bản sao không tồn tại.");
            }
            if (bs.TrangThai != "Có sẵn")
            {
                throw new Exception($"Bản sao đang ở trạng thái '{bs.TrangThai}'. Không thể mượn.");
            }

            // 2. Lấy Tên Tài liệu
            TaiLieuDTO tl = _taiLieuDAL.GetTaiLieuByMaTL(bs.MaTL);

            return new BanSaoKiemTraDTO
            {
                MaBS = bs.MaBS,
                TrangThai = bs.TrangThai,
                TenTL = (tl != null) ? tl.TenTL : "Không tìm thấy Tên TL"
            };
        }

        // Hàm nghiệp vụ: Ghi nhận Trả sách
        public bool GhiNhanTra(string maGD, List<string> danhSachMaBSTra, bool traHet)
        {
            try
            {
                // 1. Cập nhật trạng thái Giao dịch chính (Nếu trả hết)
                if (traHet)
                {
                    _dal.UpdateGiaoDich(maGD, DateTime.Now.Date, "Đã trả");
                }
                // (Nếu không trả hết, TrangThai vẫn là "Đang mượn")

                // 2. Cập nhật từng chi tiết (Bảng phụ) và Bảng Bản sao
                foreach (string maBS in danhSachMaBSTra)
                {
                    // 2a. Cập nhật tGiaoDich_BanSao (TinhTrang = 1)
                    _dal.UpdateChiTietGiaoDich(maGD, maBS);

                    // 2b. Cập nhật tBanSao (TrangThai = "Có sẵn")
                    _banSaoDAL.UpdateTrangThaiBanSao(maBS, "Có sẵn");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL khi ghi nhận trả: " + ex.Message);
                return false;
            }
        }

        // Hàm nghiệp vụ: Xóa Giao dịch
        public bool XoaGiaoDich(string maGD)
        {
            try
            {
                // 1. Lấy danh sách chi tiết các bản sao đang mượn
                List<GiaoDich_BanSaoDTO> chiTiet = _dal.GetChiTietGiaoDich(maGD);

                // 2. Cập nhật trạng thái các bản sao về "Có sẵn"
                foreach (var item in chiTiet)
                {
                    // Chỉ cập nhật nếu bản sao đó chưa được trả (TinhTrang = 0)
                    if (item.TinhTrang == false)
                    {
                        _banSaoDAL.UpdateTrangThaiBanSao(item.MaBS, "Có sẵn");
                    }
                }

                // 3. Xóa Giao dịch chính (sẽ tự động xóa tGiaoDich_BanSao)
                return _dal.DeleteGiaoDich(maGD);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL khi xóa Giao dịch: " + ex.Message);
                return false;
            }
        }

        // Hỗ trợ Autocomplete cho GUI
        public List<string> LayDanhSachMaTBD()
        {
            return _theBanDocDAL.GetAllMaTBD();
        }

        public List<string> LayDanhSachMaBSCoSan()
        {
            return _banSaoDAL.GetAvailableMaBS();
        }

        // File: LibraryManagerApp.BLL/GiaoDichBLL.cs

        // ... (Các using và class GiaoDichBLL)

        // Hàm mới: Chuẩn bị dữ liệu in phiếu (Trả về DataTable hoặc List<DtoPhang>)
        // Để đơn giản và linh hoạt, ta trả về List các đối tượng DTO phẳng (tự định nghĩa hoặc dùng Dictionary)
        // Ở đây tôi đề xuất trả về DataTable trực tiếp để dễ đổ vào Report
        public System.Data.DataTable LayDuLieuInPhieu(string maGD)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            // Định nghĩa cột khớp với DataSet dtPhieuGiaoDich
            dt.Columns.Add("MaGD");
            dt.Columns.Add("NgayLap");
            dt.Columns.Add("TenNhanVien");
            dt.Columns.Add("MaTBD");
            dt.Columns.Add("TenBanDoc");
            dt.Columns.Add("MaBS");
            dt.Columns.Add("TenTaiLieu");
            dt.Columns.Add("TinhTrangSach");
            dt.Columns.Add("NgayHenTra");
            dt.Columns.Add("NgayTraThucTe");
            dt.Columns.Add("LoaiPhieu");

            try
            {
                // 1. Lấy thông tin Giao dịch chính
                GiaoDichDTO gd = LayTatCaGiaoDich().FirstOrDefault(g => g.MaGD == maGD);
                if (gd == null) return dt;

                // 2. Lấy chi tiết
                List<GiaoDich_BanSaoDTO> chiTiet = _dal.GetChiTietGiaoDich(maGD);

                // 3. Xác định loại phiếu (Mượn hay Trả)
                // Logic: Nếu có ít nhất 1 sách chưa trả -> Phiếu Mượn (hoặc đang mượn)
                // Nếu tất cả đã trả -> Phiếu Trả (hoặc in phiếu trả cho các sách vừa trả)
                // Để đơn giản: Dựa vào TrangThai của GD
                string loaiPhieu = (gd.TrangThai == "Đã trả") ? "PHIẾU TRẢ" : "PHIẾU MƯỢN";

                // 4. Duyệt qua từng sách và thêm vào DataTable
                foreach (var item in chiTiet)
                {
                    var row = dt.NewRow();

                    // Thông tin chung (lặp lại)
                    row["MaGD"] = gd.MaGD;
                    row["NgayLap"] = gd.NgayMuon.ToString("dd/MM/yyyy");
                    row["TenNhanVien"] = gd.HoTenNV;
                    row["MaTBD"] = gd.MaTBD;
                    row["TenBanDoc"] = gd.HoTenBD;

                    // Thông tin sách
                    row["MaBS"] = item.MaBS;
                    row["TenTaiLieu"] = item.TenTL;
                    row["TinhTrangSach"] = item.TinhTrang ? "Đã trả" : "Đang mượn";
                    row["NgayHenTra"] = gd.NgayHenTra.ToString("dd/MM/yyyy");

                    // NgayTraThucTe chỉ có nếu GD đã trả hoặc sách đó đã trả
                    // (Trong DB chưa lưu ngày trả riêng cho từng sách, tạm lấy ngày trả chung nếu có)
                    row["NgayTraThucTe"] = gd.NgayTra.HasValue ? gd.NgayTra.Value.ToString("dd/MM/yyyy") : "";

                    row["LoaiPhieu"] = loaiPhieu;

                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chuẩn bị dữ liệu in: " + ex.Message);
            }

            return dt;
        }
    }
}
