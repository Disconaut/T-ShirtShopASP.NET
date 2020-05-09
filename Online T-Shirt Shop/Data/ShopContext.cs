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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wish>().HasKey(w => new {w.ConsumerId, w.ProductVariantId});
            modelBuilder.Entity<CartItem>().HasKey(c => new {c.ConsumerId, c.ProductVariantId});
            modelBuilder.Entity<OrderProduct>().HasKey(o => new {o.OrderId, o.ProductVariantId});

            modelBuilder.Entity<Product>().HasData(new []
            {
                new Product{
                    Id = 1,
                    Name = "King Card",
                    Description = "Normal T-Shirt",
                    Age = TShirtAge.Adult,
                    Sex = TShirtSex.Man,
                    ImagePath =
                        @"E:\Coding\Online T-Shirt Shop\Online T-Shirt Shop\wwwroot\assets\img\product_img\KingCardWhite.png",
                    Keywords = Array.Empty<string>()
                }
                
            });

            modelBuilder.Entity<Consumer>().HasData(new[]
            {
                new Consumer()
                {
                    Id = "1",
                    UserName = "Relaxer",
                    Email = "relaxer@rlx.net",
                    EmailConfirmed = true
                },
            });


            modelBuilder.Entity<Order>().HasData(new[]
            {
                new Order
                {
                    Id = 1,
                    ConsumerId = "1",
                    Date = new DateTime(2020, 04, 03),
                    Submission = 29.99M
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
