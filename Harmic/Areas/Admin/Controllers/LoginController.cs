﻿using Harmic.Areas.Admin.Models;
using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HarmicContext _context;
        public LoginController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TbUser user)
        {
            if (user == null)
            {
                return NotFound();
            }

            // Mã hóa mật khẩu
            string pw = Function.MD5Password(user.Password);

            // Kiểm tra sự tồn tại của email và mật khẩu trong cơ sở dữ liệu
            var check = _context.TbUsers.Where(m => (m.UserName == user.UserName) && (m.Password == pw)).FirstOrDefault();

            if (check == null)
            {
                Function._Message = "Invalid UserName or Password!";
                return RedirectToAction("Index", "Login");
            }

            // Đăng nhập thành công
            Function._Message = string.Empty;
            Function._UserID = check.UserId;
            Function._UserName = check.UserName ?? string.Empty;
            Function._Email = check.Email ?? string.Empty;

            return RedirectToAction("Index", "Home");
        }

    }
}
