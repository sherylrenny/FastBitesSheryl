using FastBites.Models;

namespace FastBites.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext db)
        {
            db.Database.EnsureCreated();

            if (db.Categories.Any()) return;

            var burgers = new Category { Name = "Burgers" };
            var sides = new Category { Name = "Sides" };
            var drinks = new Category { Name = "Drinks" };

            db.Categories.AddRange(burgers, sides, drinks);
            db.SaveChanges();

            db.Products.AddRange(
                new Product { Name = "Classic Burger", Description = "Beef patty, lettuce, tomato, sauce", Price = 4.99m, CategoryId = burgers.Id, ImageUrl = "/images/burger1.jpg" },
                new Product { Name = "Cheese Burger", Description = "Beef patty with cheese", Price = 5.49m, CategoryId = burgers.Id, ImageUrl = "/images/burger2.jpg" },
                new Product { Name = "Fries (Large)", Description = "Crispy seasoned fries", Price = 2.49m, CategoryId = sides.Id, ImageUrl = "/images/fries.jpg" },
                new Product { Name = "Cola", Description = "Chilled soft drink", Price = 1.99m, CategoryId = drinks.Id, ImageUrl = "/images/cola.jpg" }
            );

            db.SaveChanges();
        }
    }
}

