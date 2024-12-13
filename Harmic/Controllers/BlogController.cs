using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class BlogController : Controller
    {
        static int? idblog;
        static string? aliasblog;
        private readonly HarmicContext _context;
        public BlogController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/blog/{alias}-{id}.html")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogs == null)
            {
                return NotFound();
            }
           
            var blog = await _context.TbBlogs.FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            idblog = id;
            aliasblog = blog.Alias;
            // Gán danh sách bình luận đúng kiểu vào ViewBag
            ViewBag.blogComment = await _context.TbBlogComments
                .Where(c => c.BlogId == id)
                .ToListAsync();

            return View(blog);
        }
        public IActionResult comment(string Name, string Phone, string Email, string Detail)
        {
            TbBlogComment comment = new TbBlogComment() { };
            comment.Name = Name;
            comment.Phone = Phone;
            comment.Email = Email;
            comment.Detail = Detail;
            comment.CreatedDate = DateTime.Now;
            comment.BlogId = idblog;
            comment.IsActive = true;
            _context.Add(comment);
            _context.SaveChanges();
            string url = $"/blog/{aliasblog}-{idblog}.html";
            return Redirect(url);
        }



    }
}
