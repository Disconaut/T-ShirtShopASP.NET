using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Models;

namespace Online_T_Shirt_Shop.Data
{
    public class ShopContext : IdentityDbContext<Consumer>
    {
        public ShopContext()
        {

        }

        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wish>().HasKey(w => new {w.ConsumerId, w.ProductVariantId});
            modelBuilder.Entity<CartItem>().HasKey(c => new {c.ConsumerId, c.ProductVariantId});
            modelBuilder.Entity<OrderProduct>().HasKey(o => new {o.OrderId, o.ProductVariantId});

            base.OnModelCreating(modelBuilder);
        }
    }
}
