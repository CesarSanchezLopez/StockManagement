using StockManagement.Core.Enums;

namespace StockManagement.Api.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductionType { get; set; } // "Elaborado a mano", etc.
        public ProductState State { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
