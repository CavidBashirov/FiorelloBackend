using FiorelloBackend.Helpers.Responses;
using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using FiorelloBackend.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiorelloBackend.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;

        public BasketService(IHttpContextAccessor httpContextAccessor,
                             IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;

        }

        public void AddBasket(int id, Product product)
        {
            List<BasketVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                basket.Add(new BasketVM { Id = product.Id, Count = 1 });
            }
            else
            {
                existProduct.Count++;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public async Task<DeleteBasketResponse> DeleteItem(int id)
        {
            List<decimal> grandTotal = new();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
           
            BasketVM basketItem = basket.FirstOrDefault(m => m.Id == id);

            basket.Remove(basketItem);

            foreach (var item in basket)
            {
                var product = await _productService.GetByIdAsync(item.Id);

                decimal productPrice = product.Price;

                decimal total = item.Count * productPrice;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return new DeleteBasketResponse
            {
                Count = basket.Sum(m => m.Count),
                GrandTotal = grandTotal.Sum()
            };

        }

        public async Task<List<BasketDetailVM>> GetBasketDatasAsync()
        {
            List<BasketVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            List<BasketDetailVM> basketDetailList = new();

            foreach (var item in basket)
            {
                Product existProduct = await _productService.GetByIdWithIncludesAsync(item.Id);

                basketDetailList.Add(new BasketDetailVM
                {
                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Description = existProduct.Description,
                    Price = existProduct.Price,
                    Count = item.Count,
                    Total = existProduct.Price * item.Count,
                    Category = existProduct.Category?.Name,
                    Image = existProduct.Images?.FirstOrDefault(m => m.IsMain).Image
                });

            }

            return basketDetailList;
        }

        public int GetCount()
        {
            List<BasketVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket.Sum(m=>m.Count);
        }

   
    }
}
