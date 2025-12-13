using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library_Manager.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // SỬA ĐỔI: Kiểm tra Session "MaTk" (Mã Tài khoản)
            // Nếu MaTk không tồn tại, tức là chưa đăng nhập, chuyển hướng về Login.
            if (context.HttpContext.Session.GetString("MaTk") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Account" },
                        {"Action", "Login" }
                    }
                );
            }
        }
    }
}