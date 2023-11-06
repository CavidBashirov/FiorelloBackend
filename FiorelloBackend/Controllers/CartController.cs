using FiorelloBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloBackend.Controllers
{
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        
        public CartController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index() => View(await _basketService.GetBasketDatasAsync());


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _basketService.DeleteItem(id);

            return Ok(data);
        }
    
    }
}
