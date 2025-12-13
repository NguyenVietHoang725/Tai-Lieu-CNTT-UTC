using DeThi210702.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeThi210702.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private readonly OnlineShopContext _context;

        public RenderViewComponent(OnlineShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navItems = await _context.NavItems.ToListAsync();
            return View("RenderNavItem", navItems);
        }
    }

}
