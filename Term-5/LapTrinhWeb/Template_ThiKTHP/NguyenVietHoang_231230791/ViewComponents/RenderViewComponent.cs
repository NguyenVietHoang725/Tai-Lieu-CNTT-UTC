using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVietHoang_231230791.Models;

namespace NguyenVietHoang_231230791.ViewComponents
{
    // Tên class thường đặt là [Ten]ViewComponent
    public class RenderViewComponent : ViewComponent
    {
        //// 1. Khai báo DbContext
        //// [TEN_CONTEXT] lấy trong file Models/Context.cs (VD: QlhangHoaContext)
        //private readonly [TEN_CONTEXT] _context;

        //// 2. Constructor (Dependency Injection)
        //public RenderViewComponent([TEN_CONTEXT] context)
        //{
        //    _context = context;
        //}

        //// 3. Hàm InvokeAsync (Bắt buộc phải có tên này)
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    // [TEN_BANG_CAN_LAY]: Tên bảng trong Context (VD: LoaiHangs, Categories...)
        //    // ToListAsync(): Dùng để lấy toàn bộ danh sách bất đồng bộ
        //    var duLieu = await _context.[TEN_BANG_CAN_LAY].ToListAsync();

        //    // "RenderData": Là tên file View sẽ tạo ở Bước 2
        //    return View("RenderData", duLieu);
        //}
    }

}
