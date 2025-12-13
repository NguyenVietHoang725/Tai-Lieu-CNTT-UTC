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
    internal class DinhDangBLL
    {
        private DinhDangDAL _dal = new DinhDangDAL();

        // READ
        public List<DinhDangDTO> LayTatCaDinhDang()
        {
            return _dal.GetAllDinhDangDTO();
        }

        public DinhDangDTO LayChiTietDinhDang(string maDD)
        {
            return _dal.GetDinhDangByMaDD(maDD);
        }

        // CREATE (Bao gồm logic sinh mã)
        public string ThemDinhDang(DinhDangDTO model)
        {
            try
            {
                // 1. Sinh Mã Định dạng (MaDD = DD[###])
                string newMaDD = _dal.GenerateNewMaDD();

                if (string.IsNullOrEmpty(newMaDD))
                {
                    return null; // Lỗi sinh mã
                }

                // 2. Gán mã và thực hiện Insert
                model.MaDD = newMaDD;
                if (_dal.InsertDinhDang(model))
                {
                    return newMaDD;
                }
                return string.Empty; // Lỗi DB khi Insert
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nghiệp vụ/hệ thống khi thêm Định dạng: " + ex.Message);
                return null;
            }
        }

        // UPDATE
        public bool CapNhatDinhDang(DinhDangDTO model)
        {
            return _dal.UpdateDinhDang(model);
        }

        // DELETE
        public bool XoaDinhDang(string maDD)
        {
            return _dal.DeleteDinhDang(maDD);
        }
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetDinhDangFields();
        }

        public List<DinhDangDTO> TimKiemDinhDang(List<SearchFilter> filters)
        {
            return _dal.SearchDinhDang(filters);
        }
    }
}
