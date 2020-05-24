using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Models;
using Online_T_Shirt_Shop.Models.Enums;

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
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(c => new {c.ConsumerId, c.ProductId});
            modelBuilder.Entity<OrderProduct>().HasKey(o => new {o.OrderId, o.ProductId});

            modelBuilder.Entity<Product>().HasData(new []
            {
                new Product{
                    Id = 1,
                    Name = "King Card",
                    Description = "Normal T-Shirt",
                    Age = TShirtAge.Adult,
                    Sex = TShirtSex.Man,
                    ImagePath =
                        @"KingCardWhite.png"
                }
                
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
