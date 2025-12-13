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
    internal class TheLoaiBLL
    {
        private TheLoaiDAL _dal = new TheLoaiDAL();

        // READ
        public List<TheLoaiDTO> LayTatCaTheLoai()
        {
            return _dal.GetAllTheLoaiDTO();
        }

        public TheLoaiDTO LayChiTietTheLoai(string maThL)
        {
            return _dal.GetTheLoaiByMaThL(maThL);
        }

        // CREATE (Bao gồm logic sinh mã)
        public string ThemTheLoai(TheLoaiDTO model)
        {
            try
            {
                // 1. Sinh Mã Thể loại (MaThL = THL[###])
                string newMaThL = _dal.GenerateNewMaThL();

                if (string.IsNullOrEmpty(newMaThL))
                {
                    return null; // Lỗi sinh mã
                }

                // 2. Gán mã và thực hiện Insert
                model.MaThL = newMaThL;
                if (_dal.InsertTheLoai(model))
                {
                    return newMaThL;
                }
                return string.Empty; // Lỗi DB khi Insert
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nghiệp vụ/hệ thống khi thêm Thể loại: " + ex.Message);
                return null;
            }
        }

        // UPDATE
        public bool CapNhatTheLoai(TheLoaiDTO model)
        {
            // Logic nghiệp vụ (Ví dụ: Kiểm tra tên Thể loại đã tồn tại chưa)
            return _dal.UpdateTheLoai(model);
        }

        // DELETE
        public bool XoaTheLoai(string maThL)
        {
            return _dal.DeleteTheLoai(maThL);
        }
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetTheLoaiFields();
        }

        public List<TheLoaiDTO> TimKiemTheLoai(List<SearchFilter> filters)
        {
            return _dal.SearchTheLoai(filters);
        }
    }
}
