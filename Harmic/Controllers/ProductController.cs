using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Harmic.Controllers
{
    public class ProductController : Controller
    {
        static int? idproduct;
        static string? aliasproduct;
        private readonly HarmicContext _context;
        public ProductController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/product/{alias}-{id}.html")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }
            var product = await _context.TbProducts.Where(m => (bool)m.IsActive).Where(m => (bool)m.IsNew)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            idproduct = id;
            aliasproduct = product.Alias;
            var productReviews = _context.TbProductReviews
	        .Where(m => m.ProductId == id && m.IsActive == true)
        	.ToList() ?? new List<TbProductReview>();

			ViewBag.productReviews = productReviews;

			ViewBag.productRelated = _context.TbProducts
                .Where(m => m.ProductId != id && m.CategoryProductId == product.CategoryProductId)
                .OrderByDescending(m => m.ProductId).ToList();
            return View(product);
        }
        public IActionResult comment(string Name, string Phone, string Email, string Detail)
        {
            TbProductReview comment = new TbProductReview() { };
            comment.Name = Name;
            comment.Phone = Phone;
            comment.Email = Email;
            comment.Detail = Detail;
            comment.CreatedDate = DateTime.Now;
            comment.ProductId = idproduct;
            comment.IsActive = true;
            _context.Add(comment);
            _context.SaveChanges();
            string url = $"/product/{aliasproduct}-{idproduct}.html";
            return Redirect(url);
        }
    } 
}
