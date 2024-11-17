using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;

        public ProductViewComponent(HarmicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbProducts.Where(m => (bool)m.IsActive).Where(m => (bool)m.IsNew);

            return await Task.FromResult<IViewComponentResult>
                (View(items.OrderByDescending(m => m.ProductId).ToList()));
        }
    }
}
