using Microsoft.EntityFrameworkCore;
using Bakery.Data.Entities;


namespace Bakery.Website.Data
{
    public class BakeryDBContext : DbContext
    {
        public DbSet<Product> Products {  get; set; }
        public BakeryDBContext(DbContextOptions<BakeryDBContext> options) : base(options)
        {
        }
        public DbSet<Bakery.Data.Entities.Category> Category { get; set; } = default!;
        public DbSet<Bakery.Data.Entities.Order> Order { get; set; } = default!;
        public DbSet<Bakery.Data.Entities.Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
    }
}
