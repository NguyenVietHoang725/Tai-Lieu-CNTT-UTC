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
    internal class TacGiaBLL
    {
        private TacGiaDAL _dal = new TacGiaDAL();

        public List<TacGiaDTO> LayTatCaTacGia()
        {
            return _dal.GetAllTacGiaDTO();
        }

        public TacGiaDTO LayChiTietTacGia(string maTG)
        {
            return _dal.GetTacGiaByMaTG(maTG);
        }

        public List<tQuocGia> LayTatCaQuocGia()
        {
            return _dal.GetAllQuocGia();
        }

        public string ThemTacGia(TacGiaDTO model)
        {
            // 1. Kiểm tra nghiệp vụ (Ví dụ: kiểm tra trùng tên/quốc gia nếu cần)
            // Hiện tại, ta dựa vào SP để kiểm tra và sinh mã.

            // 2. Gọi DAL để sinh Mã TG
            // MaQG được lấy từ cboQuocGia (đã được gán vào model.MaQG)
            try
            {
                string newMaTG = _dal.GenerateNewMaTg(model.MaQG);

                if (string.IsNullOrEmpty(newMaTG))
                {
                    // Thất bại trong việc sinh mã
                    return string.Empty;
                }

                // 3. Gán Mã TG đã sinh vào model
                model.MaTG = newMaTG;

                // 4. Thực hiện Insert
                if (!_dal.InsertTacGia(model))
                {
                    return string.Empty; // Lỗi DB khi Insert
                }

                return newMaTG; // Trả về Mã TG nếu thành công
            }
            catch (Exception ex)
            {
                // Bắt lỗi RAISERROR từ SP
                Console.WriteLine("Lỗi nghiệp vụ/hệ thống khi thêm Tác giả: " + ex.Message);
                return null; // Trả về null để phân biệt lỗi hệ thống
            }
        }

        public bool CapNhatTacGia(TacGiaDTO model)
        {
            return _dal.UpdateTacGia(model);
        }

        public bool XoaTacGia(string maTG)
        {
            return _dal.DeleteTacGia(maTG);
        }
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetTacGiaFields();
        }

        public List<TacGiaDTO> TimKiemTacGia(List<SearchFilter> filters)
        {
            
            return _dal.SearchTacGia(filters);
        }
    }
}
