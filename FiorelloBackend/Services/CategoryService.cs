using FiorelloBackend.Data;
using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Category category)
        {
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }

        public async Task ExtractAsync(Category category)
        {
            category.SoftDeleted = false;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetArchiveDatasAsync()
        {
            return await _context.Categories.IgnoreQueryFilters().Where(m=>m.SoftDeleted).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id, bool isTracking)
        {
            return isTracking ? await _context.Categories.FirstOrDefaultAsync(m => m.Id == id) : await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Category> GetByIdWithoutTrackingAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<Category> GetSoftDeletedDataById(int id)
        {
            return await _context.Categories.IgnoreQueryFilters().Where(m=>m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task SoftDeleteAsync(Category category)
        {
            category.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
