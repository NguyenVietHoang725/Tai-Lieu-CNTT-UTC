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
    internal class NxbBLL
    {
        private NxbDAL _dal = new NxbDAL();

        // READ
        public List<NxbDTO> LayTatCaNxb()
        {
            return _dal.GetAllNxbDTO();
        }

        public NxbDTO LayChiTietNxb(string maNXB)
        {
            return _dal.GetNxbByMaNXB(maNXB);
        }

        // CREATE (Bao gồm logic sinh mã)
        public string ThemNxb(NxbDTO model)
        {
            try
            {
                // 1. Sinh Mã NXB (MaNXB = NXB[MaQg]-[###])
                string newMaNxb = _dal.GenerateNewMaNxb(model.MaQG);

                if (string.IsNullOrEmpty(newMaNxb))
                {
                    return null; // Lỗi sinh mã
                }

                // 2. Gán mã và thực hiện Insert
                model.MaNXB = newMaNxb;
                if (_dal.InsertNxb(model))
                {
                    return newMaNxb;
                }
                return string.Empty; // Lỗi DB khi Insert
            }
            catch (Exception ex)
            {
                // Bắt lỗi RAISERROR từ SP (ví dụ: Đã đạt giới hạn 999)
                Console.WriteLine("Lỗi nghiệp vụ/hệ thống khi thêm NXB: " + ex.Message);
                return null;
            }
        }

        // UPDATE
        public bool CapNhatNxb(NxbDTO model)
        {
            // Logic nghiệp vụ (Ví dụ: Kiểm tra tên NXB mới có trùng không, ...)
            return _dal.UpdateNxb(model);
        }

        // DELETE
        public bool XoaNxb(string maNXB)
        {
            // Logic nghiệp vụ (Ví dụ: Kiểm tra NXB này có đang phát hành tài liệu nào không)
            return _dal.DeleteNxb(maNXB);
        }
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetNxbFields();
        }

        public List<NxbDTO> TimKiemNxb(List<SearchFilter> filters)
        {
            // Đảm bảo NxbDAL có hàm SearchNxb
            return _dal.SearchNxb(filters);
        }
    }
}
