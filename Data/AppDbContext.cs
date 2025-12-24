using Microsoft.EntityFrameworkCore;
using zomage.Models;

namespace zomage.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "ZOMA BAFIX FOAM", Category = "ქაფით რეცხვა", Price = 9.78m, OldPrice = 9.78m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 2, Name = "ZOMA GLASS CLEANER", Category = "სამზარეულო", Price = 7.50m, OldPrice = 8.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 3, Name = "ZOMA FLOOR CLEAN", Category = "სამზარეულოს იატაკი", Price = 12.99m, OldPrice = 14.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 4, Name = "ZOMA DISH SOAP", Category = "სამზარეულო", Price = 5.99m, OldPrice = 6.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 5, Name = "ZOMA AUTO SHAMPOO", Category = "ავტოქიმია", Price = 15.00m, OldPrice = 18.00m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 6, Name = "ZOMA SANITIZER", Category = "საპნის და სადეზინფექციო საშუალებები", Price = 4.50m, OldPrice = 5.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 7, Name = "ZOMA MULTI SURFACE CLEANER", Category = "სამზარეულო", Price = 8.99m, OldPrice = 10.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 8, Name = "ZOMA OVEN CLEANER", Category = "სამზარეულო", Price = 11.50m, OldPrice = 13.00m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 9, Name = "ZOMA BATHROOM CLEANER", Category = "საპნის და სადეზინფექციო საშუალებები", Price = 9.25m, OldPrice = 10.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 10, Name = "ZOMA TOILET CLEANER", Category = "საპნის და სადეზინფექციო საშუალებები", Price = 6.75m, OldPrice = 7.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 11, Name = "ZOMA WINDOW CLEANER", Category = "სამზარეულო", Price = 7.25m, OldPrice = 8.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 12, Name = "ZOMA CAR WAX", Category = "ავტოქიმია", Price = 18.50m, OldPrice = 22.00m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 13, Name = "ZOMA TIRE CLEANER", Category = "ავტოქიმია", Price = 12.00m, OldPrice = 14.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 14, Name = "ZOMA INTERIOR CLEANER", Category = "ავტოქიმია", Price = 13.75m, OldPrice = 16.00m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 15, Name = "ZOMA LEATHER CLEANER", Category = "ავტოქიმია", Price = 16.50m, OldPrice = 19.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 16, Name = "ZOMA LIQUID SOAP", Category = "თხევადი საპონი", Price = 4.25m, OldPrice = 5.00m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 17, Name = "ZOMA HAND SOAP", Category = "თხევადი საპონი", Price = 5.50m, OldPrice = 6.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 18, Name = "ZOMA BODY WASH", Category = "თხევადი საპონი", Price = 7.99m, OldPrice = 9.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 19, Name = "ZOMA SHAMPOO", Category = "შამპუნი", Price = 8.50m, OldPrice = 10.00m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 20, Name = "ZOMA CONDITIONER", Category = "შამპუნი", Price = 8.75m, OldPrice = 10.25m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 21, Name = "ZOMA 2-IN-1 SHAMPOO", Category = "შამპუნი", Price = 9.99m, OldPrice = 11.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 22, Name = "ZOMA AIR FRESHENER", Category = "არომატიზატორები", Price = 6.25m, OldPrice = 7.50m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 23, Name = "ZOMA ROOM SPRAY", Category = "არომატიზატორები", Price = 5.75m, OldPrice = 6.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 24, Name = "ZOMA CAR FRESHENER", Category = "არომატიზატორები", Price = 4.99m, OldPrice = 5.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 25, Name = "ZOMA FABRIC FRESHENER", Category = "არომატიზატორები", Price = 7.50m, OldPrice = 8.99m, ImageUrl = "/images/products/product.svg" },
            new Product { Id = 26, Name = "ZOMA KITCHEN FLOOR CLEANER", Category = "სამზარეულოს იატაკი", Price = 10.25m, OldPrice = 12.00m, ImageUrl = "/images/products/product.svg" }
        );
    }
}

