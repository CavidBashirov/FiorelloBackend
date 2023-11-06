using FiorelloBackend.Data;
using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllWithImagesByTakeAsync(int take)
        {
            return await _context.Products.Where(m => !m.SoftDeleted)
                                          .Include(m => m.Images)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted && m.Id == id)
                                                              .Include(m => m.Images)
                                                              .Include(m => m.Category)
                                                              .FirstOrDefaultAsync();
        }
    }
}
