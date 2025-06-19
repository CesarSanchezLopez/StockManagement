using StockManagement.Api.Core.Entities;
using StockManagement.Api.Core.Interfaces;
using StockManagement.Core.Enums;

namespace StockManagement.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddProductAsync(Product product)
        {
            ValidateProduct(product);
            await _repository.AddAsync(product);
        }

        public async Task AddProductsAsync(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                ValidateProduct(product);
            }

            await _repository.AddRangeAsync(products);
        }

        public async Task<IEnumerable<Product>> GetByStateAsync(ProductState state)
        {
            return await _repository.GetByStateAsync(state);
        }

        public async Task MarkAsDefectiveAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentException("Producto no encontrado");

            if (product.State != ProductState.Disponible)
                throw new InvalidOperationException("Solo se pueden marcar como defectuosos los productos disponibles");

            product.State = ProductState.Defectuoso;
            await _repository.UpdateAsync(product);
        }

        public async Task MarkAsShippedAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentException("Producto no encontrado");

            if (product.State != ProductState.Disponible)
                throw new InvalidOperationException("Solo se pueden despachar productos disponibles");

            product.State = ProductState.Salido;
            await _repository.UpdateAsync(product);
        }

        private void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("El nombre del producto es obligatorio");

            if (string.IsNullOrWhiteSpace(product.ProductionType))
                throw new ArgumentException("El tipo de elaboración es obligatorio");

            if (product.ProductionType != "Elaborado a mano" &&
                product.ProductionType != "Elaborado a mano y máquina")
                throw new ArgumentException("Tipo de elaboración inválido");

            if (!Enum.IsDefined(typeof(ProductState), product.State))
                throw new ArgumentException("Estado del producto inválido");
        }
    }
}
