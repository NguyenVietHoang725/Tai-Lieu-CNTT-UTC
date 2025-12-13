using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Library_Manager.Filters // Thay namespace cho phù hợp
{
    public class Authorization : ActionFilterAttribute
    {
        // Mảng chứa các vai trò được phép truy cập
        public string[] RequiredRoles { get; set; }

        public Authorization(string roles)
        {
            // Nhận chuỗi các vai trò, ví dụ "QTV,QLB" và tách thành mảng
            this.RequiredRoles = roles.Split(',');
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Lấy vai trò của người dùng từ Session
            var userRole = context.HttpContext.Session.GetString("UserRole");

            // 1. Kiểm tra xem người dùng đã đăng nhập chưa
            if (string.IsNullOrEmpty(userRole))
            {
                // Nếu chưa, chuyển hướng về trang Login
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "Controller", "Account" },
                        { "Action", "Login" }
                    });
                return; // Dừng thực thi
            }

            // 2. Quản trị viên (QTV) có quyền truy cập tất cả
            if (userRole == "QTV")
            {
                base.OnActionExecuting(context); // Cho phép truy cập
                return;
            }

            // 3. Kiểm tra xem vai trò của người dùng có nằm trong danh sách các vai trò được yêu cầu không
            if (!RequiredRoles.Contains(userRole))
            {
                //// Nếu không có quyền, chuyển hướng về trang chủ hoặc trang báo lỗi "Access Denied"
                //context.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary
                //    {
                //        { "Controller", "Home" },
                //        { "Action", "Index" }
                //        // Bạn có thể thêm một tham số để hiển thị thông báo "Không có quyền truy cập"
                //    });
                context.Result = new ViewResult { ViewName = "AccessDenied" };
                return;
            }

            // Nếu tất cả kiểm tra đều qua, cho phép thực thi Action
            base.OnActionExecuting(context);
        }
    }
}