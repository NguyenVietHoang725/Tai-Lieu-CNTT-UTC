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
    internal class TheBanDocBLL
    {
        private TheBanDocDAL _dal = new TheBanDocDAL();
        private BanDocDAL _bdDal = new BanDocDAL();

        public List<TheBanDocDTO> LayThongTinTheBanDoc()
        {
            return _dal.GetAllTheBanDocDTO();
        }

        public TheBanDocDTO LayChiTietTheBanDoc(string maTBD)
        {
            return _dal.GetTheBanDocByMaTBD(maTBD);
        }

        public List<BanDocChuaCoTheDTO> LayBanDocChuaCoThe()
        {
            return _bdDal.GetBanDocChuaCoThe();
        }

        public string ThemTheBanDoc(TheBanDocDTO model, out int errorStatus)
        {
            errorStatus = 0; // 0: Thành công

            // 1. Logic tính toán Ngày Hết Hạn (4 năm sau ngày cấp)
            if (model.NgayCap != null)
            {
                model.NgayHetHan = model.NgayCap.AddYears(4);
            }
            else
            {
                // Đảm bảo có NgayCap
                errorStatus = 4; // Mã lỗi tự định nghĩa: Ngày Cấp không hợp lệ
                return string.Empty;
            }

            // 2. Gọi DAL để sinh Mã TBD (MaTBD được sinh dựa trên MaBD)
            string maTBD = _dal.GenerateNewMaTBD(model.MaBD, out errorStatus);

            if (errorStatus != 0)
            {
                // Trả về lỗi nếu SP báo lỗi (1: MaBD không tồn tại, 2: Đã có thẻ, 3: Lỗi chiều dài)
                return string.Empty;
            }

            // 3. Gán Mã TBD đã sinh vào model
            model.MaTBD = maTBD;

            // 4. Thực hiện Insert
            if (!_dal.InsertTheBanDoc(model))
            {
                errorStatus = 99; // Lỗi hệ thống/DB khi Insert
                return string.Empty;
            }

            return maTBD; // Trả về Mã TBD nếu thành công
        }

        public bool CapNhatTheBanDoc(TheBanDocDTO model)
        {
            // Logic nghiệp vụ: Cần đảm bảo Ngày Hết Hạn >= Ngày Cấp.
            // (Thao tác này đã được ràng buộc trong ValidateInputs của GUI và ràng buộc DB)

            return _dal.UpdateTheBanDoc(model);
        }

        public bool XoaTheBanDoc(string maTBD)
        {
            // Logic nghiệp vụ: Cần kiểm tra Thẻ có đang ở trạng thái 'Hoạt động' không
            // Hoặc có ràng buộc mượn sách không.
            // Để đơn giản, hiện tại ta chỉ gọi DAL.
            return _dal.DeleteTheBanDoc(maTBD);
        }

        // Hàm tìm kiếm
        public List<TheBanDocDTO> TimKiemTheBanDoc(List<SearchFilter> filters)
        {
            return _dal.SearchTheBanDoc(filters);
        }

        // Hàm cung cấp Metadata cho UI (FrmTimKiem)
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetTheBanDocFields();
        }
    }
}
