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
    internal class BanSaoBLL
    {
        private BanSaoDAL _dal = new BanSaoDAL();

        // READ (List)
        public List<BanSaoDTO> LayDanhSachBanSao(string maTL)
        {
            return _dal.GetBanSaoByMaTL(maTL);
        }

        // READ (Detail)
        public BanSaoDTO LayChiTietBanSao(string maBS)
        {
            return _dal.GetBanSaoByMaBS(maBS);
        }

        // CREATE
        public string ThemBanSao(BanSaoDTO model)
        {
            try
            {
                // 1. Sinh Mã Bản sao (MaBS)
                string newMaBS = _dal.GenerateNewMaBS(model.MaTL);
                if (string.IsNullOrEmpty(newMaBS))
                {
                    return null; // Lỗi sinh mã
                }

                // 2. Gán mã và thực hiện Insert
                model.MaBS = newMaBS;
                if (_dal.InsertBanSao(model))
                {
                    return newMaBS;
                }
                return string.Empty; // Lỗi DB khi Insert
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nghiệp vụ/hệ thống khi thêm Bản sao: " + ex.Message);
                return null; // Trả về null để báo hiệu lỗi SP/hệ thống
            }
        }

        // UPDATE
        public bool CapNhatBanSao(BanSaoDTO model)
        {
            return _dal.UpdateBanSao(model);
        }

        // DELETE
        public bool XoaBanSao(string maBS)
        {
            // Logic nghiệp vụ (Kiểm tra xem bản sao có đang được mượn không,...)
            // Hiện tại, ta dựa vào ràng buộc CSDL
            return _dal.DeleteBanSao(maBS);
        }
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetBanSaoFields();
        }

       
        // Hàm tìm kiếm có thêm tham số MaTL để lọc kép
        public List<BanSaoDTO> TimKiemBanSao(string maTL, List<SearchFilter> filters)
        {
            // Gọi DAL và truyền cả MaTL và filters
            return _dal.SearchBanSao(maTL, filters);
        }
    }
}
