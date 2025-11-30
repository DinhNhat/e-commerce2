using Bogus;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

public class SeedData
{
    public static List<Product> GenerateProducts(int count = 200)
    {
        // Define the categories your products will fall into
        var categories = new[] { "Chess", "Board Games", "Puzzles", "Card Games", "Accessories" };

        // 1. Create a "Faker" rule set for the Product class
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.ProductID, f => f.IndexFaker + 1) // Simple sequential ID
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence(5, 5)) // 5 to 10 words
            .RuleFor(p => p.Category, f => f.PickRandom(categories)) // Picks one of the defined categories
            .RuleFor(p => p.Price, f => f.Random.Decimal(5.00m, 500.00m)); // Random price between $5.00 and $500.00

        // 2. Generate the specified count of products
        return productFaker.Generate(count);
    }
    
    
    public static void EnsurePopulated(IApplicationBuilder app) {
            StoreDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<StoreDBContext>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.Products.Any()) {
                
                var mockProducts = SeedData.GenerateProducts(200);
                context.Products.AddRange(mockProducts);
                context.SaveChanges();
                
                /*context.Products.AddRange(
                    new Product {
                        Name = "Kayak", 
                        Description =
                            "A boat for one person",
                        Category = "Watersports", Price = 275
                    },
                    new Product {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports", Price = 48.95m
                    },
                    new Product {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer", Price = 19.50m
                    },
                    new Product {
                        Name = "Corner Flags",
                        Description =
                          "Give your playing field a professional touch",
                        Category = "Soccer", Price = 34.95m
                    },
                    new Product {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Category = "Soccer", Price = 79500
                    },
                    new Product {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Category = "Chess", Price = 16
                    },
                    new Product {
                        Name = "Unsteady Chair",
                        Description =
                          "Secretly give your opponent a disadvantage",
                        Category = "Chess", Price = 29.95m
                    },
                    new Product {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess", Price = 75
                    },
                    new Product {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Category = "Chess", Price = 1200
                    }
                );
                context.SaveChanges();*/
            }
    }
}