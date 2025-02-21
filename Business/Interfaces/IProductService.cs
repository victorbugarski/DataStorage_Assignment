using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<Product?> CreateProductAsync(ProductRegistrationForm form);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product?>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(int id, ProductUpdateForm form);
        Task<bool> DeleteProductAsync(int id);
    }
}