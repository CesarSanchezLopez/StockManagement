using StockManagement.Api.Core.Entities;
using StockManagement.Core.Enums;

namespace StockManagement.Api.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddProductAsync(Product product);
        Task AddProductsAsync(IEnumerable<Product> products);
        Task<IEnumerable<Product>> GetByStateAsync(ProductState state);
        Task MarkAsDefectiveAsync(Guid id);
        Task MarkAsShippedAsync(Guid id);
    }
}
