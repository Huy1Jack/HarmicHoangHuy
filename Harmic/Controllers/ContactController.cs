using Harmic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    
    public class ContactController : Controller
    {
        private readonly HarmicContext _context;
        public ContactController (HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult contact(string name, string phone, string email, string massage)
        {
            TbContact contact = new TbContact();
            contact.Name = name;
            contact.Phone = phone;
            contact.Email = email;
            contact.Message = massage;
            contact.CreatedDate = DateTime.Now;
            _context.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
