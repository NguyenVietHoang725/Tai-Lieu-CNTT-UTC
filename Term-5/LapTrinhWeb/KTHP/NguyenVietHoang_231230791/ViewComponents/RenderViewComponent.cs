using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVietHoang_231230791.Models;

namespace NguyenVietHoang_231230791.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private readonly VanTai2512V2Context _context;

        public RenderViewComponent(VanTai2512V2Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Truy vấn dữ liệu:
            // 1. Include: Để lấy thông tin Công Ty (dùng cho việc sắp xếp và hiển thị nếu cần)
            // 2. OrderByDescending: Sắp xếp giảm dần theo Tên Công Ty
            // 3. Take(6): Chỉ lấy 6 bản ghi
            var duLieu = await _context.Xes
                                .Include(x => x.MaCongTyNavigation) // Join với bảng CongTy
                                .OrderByDescending(x => x.MaCongTyNavigation.TenCongTy)
                                .Take(6)
                                .ToListAsync();

            return View("RenderData", duLieu);
        }
    }
}