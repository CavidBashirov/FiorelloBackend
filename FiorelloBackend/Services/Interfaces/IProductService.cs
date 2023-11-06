using FiorelloBackend.Models;

namespace FiorelloBackend.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllWithImagesByTakeAsync(int take);
        Task<Product> GetByIdWithIncludesAsync(int id);
    }
}
