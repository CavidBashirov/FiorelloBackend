﻿using FiorelloBackend.Data;
using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using FiorelloBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiorelloBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public HomeController(AppDbContext context,
                              IProductService productService,
                              IBasketService basketService)
        {
            _context = context;
            _productService = productService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();
            List<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDeleted).ToListAsync();
            List<Product> products = await _productService.GetAllWithImagesByTakeAsync(8);
            List<Category> categories = await _context.Categories.Where(m => !m.SoftDeleted).ToListAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                SliderInfo = sliderInfo,
                Blogs = blogs,
                Products = products,
                Categories = categories
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();

            _basketService.AddBasket((int)id, product);

            return Ok();

        }
    }
}