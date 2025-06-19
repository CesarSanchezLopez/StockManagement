using Microsoft.EntityFrameworkCore;
using StockManagement.Api.Core.Entities;
using StockManagement.Core.Enums;

namespace StockManagement.Api.Infrastructure
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        // Configuración de entidades (si deseas ser explícito)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.ProductionType)
                      .IsRequired();

                entity.Property(p => p.State)
                      .IsRequired()
                      .HasConversion<int>(); // Guarda enum como int

                entity.Property(p => p.CreatedAt)
                      .IsRequired();
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(50);

                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Role).IsRequired(); // "Admin" o "User"
            });

            var adminId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    Email = "UserAdmin@gmail.com",
                    Username = "UserAdmin",
                    Password = "12345",//BCrypt.Net.BCrypt.HashPassword("Admin123!"), // usa BCrypt o tu método de hash
                    Role = Role.Admin
                },
                new User
                {
                    Id = userId,
                    Email = "UserCliente@gmail.com",
                    Username = "UserCliente",
                    Password = "12345",//BCrypt.Net.BCrypt.HashPassword("User123!"),
                    Role = Role.User
                }
            );
        }
    }
}
