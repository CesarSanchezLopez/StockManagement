using StockManagement.Api.Core.Entities;
using StockManagement.Api.Core.Interfaces;
using StockManagement.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.Api.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockDbContext _context;

        public ProductRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); // Guarda los cambios
        }

        public async Task AddRangeAsync(IEnumerable<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync(); // Guarda los cambios
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetByStateAsync(ProductState state)
        {
            return await _context.Products
                .Where(p => p.State == state)
                .ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(); // Guarda los cambios
        }
    }
}
