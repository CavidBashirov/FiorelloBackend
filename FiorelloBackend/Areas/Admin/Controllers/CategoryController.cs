using FiorelloBackend.Data;
using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _appDbContext;

        public CategoryController(ICategoryService categoryService, AppDbContext appDbContext)
        {
           _categoryService = categoryService;
           _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
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

            Category existCategory = await _categoryService.GetByNameAsync(category.Name);

            if(existCategory is not null)
            {
                ModelState.AddModelError("Name", "This name is already exists");
                return View();
            }

            await _categoryService.CreateAsync(category);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category dbCategory = await _categoryService.GetByIdAsync(id,false);

            await _categoryService.DeleteAsync(dbCategory);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Category dbCategory = await _categoryService.GetByIdAsync(id, true);

            await _categoryService.SoftDeleteAsync(dbCategory);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetByIdWithoutTrackingAsync((int)id);

            if (category is null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category dbCategory = await _categoryService.GetByIdAsync((int)id,false);

            if (dbCategory is null) return NotFound();

            if(category.Name.Trim() == dbCategory.Name.Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            Category existCategory = await _categoryService.GetByNameAsync(category.Name);

            if (existCategory is not null)
            {
                ModelState.AddModelError("Name", "This name is already exists");
                return View(category);
            }

            //dbCategory.Name = category.Name;

            await _categoryService.EditAsync(category);

            return RedirectToAction(nameof(Index));
        }

        

        

    }
}
