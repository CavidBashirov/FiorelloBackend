using FiorelloBackend.Data;
using FiorelloBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = await _context.Categories.ToListAsync();
            return View(query);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category existCategory = await _context.Categories.FirstOrDefaultAsync(m=>m.Name.Trim()==category.Name.Trim());

            if(existCategory is not null)
            {
                ModelState.AddModelError("Name", "This name is already exists");
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            _context.Categories.Remove(dbCategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
