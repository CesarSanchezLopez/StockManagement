using StockManagement.Api.Core.Entities;
using StockManagement.Core.Enums;

namespace StockManagement.Api.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByStateAsync(ProductState state);
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task AddRangeAsync(IEnumerable<Product> products);
        Task UpdateAsync(Product product);
    }
}
