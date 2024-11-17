using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class ProductController : Controller
    {
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

			var productReviews = _context.TbProductReviews
	        .Where(m => m.ProductId == id && m.IsActive == true)
        	.ToList() ?? new List<TbProductReview>();

			ViewBag.productReviews = productReviews;

			ViewBag.productRelated = _context.TbProducts
                .Where(m => m.ProductId != id && m.CategoryProductId == product.CategoryProductId)
                .OrderByDescending(m => m.ProductId).ToList();
            return View(product);
        }
    } 
}
