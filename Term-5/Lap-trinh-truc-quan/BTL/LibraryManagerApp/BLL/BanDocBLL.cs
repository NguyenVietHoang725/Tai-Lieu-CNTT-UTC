using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.BLL
{
    internal class BanDocBLL
    {
        private BanDocDAL _dal = new BanDocDAL();

        public List<BanDocDTO> LayThongTinBanDoc()
        {
            return _dal.GetAllBanDocDTO();
        }

        public BanDocDTO LayChiTietBanDoc(string maBD)
        {
            return _dal.GetBanDocByMaBD(maBD);
        }

        public bool KiemTraTonTaiMaBD(string maBD)
        {
            return _dal.GetBanDocByMaBD(maBD) != null;
        }

        public bool ThemBanDoc(BanDocDTO model)
        {
            if (KiemTraTonTaiMaBD(model.MaBD))
            {
                return false;
            }

            return _dal.InsertBanDoc(model);
        }

        public bool CapNhatBanDoc(BanDocDTO model)
        {
            return _dal.UpdateBanDoc(model);
        }

        public bool XoaBanDoc(string maBD)
        {
            // TO DO: // Logic nghiệp vụ (ví dụ: kiểm tra xem Bạn Đọc có đang mượn sách không) có thể được thêm vào đây
            return _dal.DeleteBanDoc(maBD);
        }

        public List<BanDocDTO> TimKiemBanDoc(List<SearchFilter> filters)
        {
            // Có thể thêm logic nghiệp vụ như kiểm tra quyền truy cập ở đây
            return _dal.SearchBanDoc(filters);
        }
    }
}
