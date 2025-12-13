using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System.Collections.Generic;

namespace LibraryManagerApp.BLL
{
    internal class NhanVienBLL
    {
        private NhanVienDAL _dal = new NhanVienDAL();

        public List<NhanVienDTO> LayThongTinNhanVien()
        {
            return _dal.GetAllNhanVienDTO();
        }

        public NhanVienDTO LayChiTietNhanVien(string maNV)
        {
            return _dal.GetNhanVienByMaNV(maNV);
        }

        public string SinhMaNVMoi()
        {
            return _dal.GenerateNewMaNV();
        }

        public bool KiemTraTonTaiMaNV(string maNV)
        {
            return _dal.GetNhanVienByMaNV(maNV) != null;
        }

        public bool ThemNhanVien(NhanVienDTO model)
        {
            // Trong trường hợp này, vì mã được sinh tự động, ta chỉ cần kiểm tra
            // lỗi của SP hoặc ràng buộc khóa ngoại (MaNV), nhưng thường không cần kiểm tra tồn tại mã.
            // Tuy nhiên, để đảm bảo tuyệt đối:
            if (KiemTraTonTaiMaNV(model.MaNV))
            {
                // Nếu mã sinh ra đã tồn tại (trường hợp hiếm) thì trả về false
                return false;
            }

            return _dal.InsertNhanVien(model);
        }

        public bool CapNhatNhanVien(NhanVienDTO model)
        {
            return _dal.UpdateNhanVien(model);
        }

        public bool XoaNhanVien(string maNV)
        {
            // Logic nghiệp vụ (nếu cần kiểm tra NV có liên kết tài khoản/sách mượn không)
            return _dal.DeleteNhanVien(maNV);
        }

        public List<NhanVienDTO> TimKiemNhanVien(List<SearchFilter> filters)
        {
            return _dal.SearchNhanVien(filters);
        }
        // Hàm cung cấp Metadata cho UI
        public List<FieldMetadata> GetSearchFields()
        {
            // BLL gọi hàm GetNhanVienFields() để cung cấp cấu hình tìm kiếm cho UI
            return SearchMetadata.GetNhanVienFields();
        }
        public List<string> LayDanhSachPhuTrach()
        {
            return _dal.GetDistinctPhuTrach();
        }
    }
}