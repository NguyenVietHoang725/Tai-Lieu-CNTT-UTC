using Microsoft.AspNetCore.Mvc;
using NguyenVietHoang_231230791.Models;

namespace NguyenVietHoang_231230791.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private readonly QlhangHoaContext _context;

        public RenderViewComponent(QlhangHoaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loaiHangs = _context.LoaiHangs.ToList(); 
            return View("RenderLoaiHang", loaiHangs);
        }
    }
}
