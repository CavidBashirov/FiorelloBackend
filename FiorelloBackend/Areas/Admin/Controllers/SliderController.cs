using FiorelloBackend.Data;
using FiorelloBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDeleted)
                                                                .ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(slider);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(slider);
        }
    }
}
