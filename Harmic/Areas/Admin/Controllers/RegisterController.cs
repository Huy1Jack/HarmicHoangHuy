using Harmic.Areas.Admin.Models;
using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {

        private readonly HarmicContext _context;
        public RegisterController(HarmicContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Harmic.Models.TbAccount user)
        {
            if (user == null)
            {
                return NotFound();
            }

            //Kiểm tra sự tồn tại của email trong CSDL
            var check = _context.TbAccounts.Where(m => m.Email == user.Email).FirstOrDefault();
            if (check != null)
            {
                //Hiển thị thông báo, có thể làm cách khác
                Function._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "Register");

            }
            // Nếu không có thì thêm vào CSDL
            Function._MessageEmail = string.Empty;
            user.Password = Function.MD5Password(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");

        }
    }
}
