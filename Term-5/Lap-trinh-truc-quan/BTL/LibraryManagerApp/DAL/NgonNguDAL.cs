using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class NgonNguDAL
    {
        // Hàm READ chính: Lấy tất cả Ngôn ngữ
        public List<NgonNguDTO> GetAllNgonNguDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from nn in db.tNgonNgus
                            select new NgonNguDTO
                            {
                                MaNN = nn.MaNN,
                                TenNN = nn.TenNN
                            };
                return query.ToList();
            }
        }
    }
}
