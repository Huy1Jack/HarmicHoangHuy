using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            //kiểm tra trạng thái đăng nhập
            if (!Function.Islogin())
                return RedirectToAction("Index", "Login");
            return View();
        }
        public IActionResult Logout()
        {
            
            Function._UserName = string.Empty;
            Function._Email = string.Empty;
            Function._Message = string.Empty;
            Function._MessageEmail = string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }
}
