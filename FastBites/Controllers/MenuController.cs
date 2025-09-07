using FastBites.Data;
using FastBites.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastBites.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MenuController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index(int? categoryId, string? q)
        {
            var categories = await _db.Categories.Include(c => c.Products).ToListAsync();
            ViewBag.Categories = categories;

            var products = _db.Products.AsQueryable();

            if (categoryId.HasValue) products = products.Where(p => p.CategoryId == categoryId.Value);
            if (!string.IsNullOrEmpty(q)) products = products.Where(p => p.Name.Contains(q) || p.Description.Contains(q));

            return View(await products.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
