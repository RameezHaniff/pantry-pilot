using Microsoft.EntityFrameworkCore;
using PantryPilot.Api.Entities;

namespace PantryPilot.Api.Data
{
    public class FoodDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId);

            modelBuilder.Entity<Ingredient>().HasData(
               new Ingredient { Id = 1, Name = "Cucumber" },
               new Ingredient { Id = 2, Name = "Olives" },
               new Ingredient { Id = 3, Name = "Lettuce" },
               new Ingredient { Id = 4, Name = "Meat" },
               new Ingredient { Id = 5, Name = "Tomato" },
               new Ingredient { Id = 6, Name = "Cheese" },
               new Ingredient { Id = 7, Name = "Dough" }
           );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Name = "Burger", Feeds = 1 },
                new Recipe { Id = 2, Name = "Pie", Feeds = 1 },
                new Recipe { Id = 3, Name = "Sandwich", Feeds = 1 },
                new Recipe { Id = 4, Name = "Pasta", Feeds = 2 },
                new Recipe { Id = 5, Name = "Salad", Feeds = 3 },
                new Recipe { Id = 6, Name = "Pizza", Feeds = 4 }
            );

            modelBuilder.Entity<RecipeIngredient>().HasData(
                // Burger
                new RecipeIngredient { RecipeId = 1, IngredientId = 4, Quantity = 1 }, // Meat
                new RecipeIngredient { RecipeId = 1, IngredientId = 3, Quantity = 1 }, // Lettuce
                new RecipeIngredient { RecipeId = 1, IngredientId = 5, Quantity = 1 }, // Tomato
                new RecipeIngredient { RecipeId = 1, IngredientId = 6, Quantity = 1 }, // Cheese
                new RecipeIngredient { RecipeId = 1, IngredientId = 7, Quantity = 1 }, // Dough

                // Pie
                new RecipeIngredient { RecipeId = 2, IngredientId = 7, Quantity = 2 }, // Dough
                new RecipeIngredient { RecipeId = 2, IngredientId = 4, Quantity = 2 }, // Meat

                // Sandwich
                new RecipeIngredient { RecipeId = 3, IngredientId = 7, Quantity = 1 }, // Dough
                new RecipeIngredient { RecipeId = 3, IngredientId = 1, Quantity = 1 }, // Cucumber

                // Pasta
                new RecipeIngredient { RecipeId = 4, IngredientId = 7, Quantity = 2 }, // Dough
                new RecipeIngredient { RecipeId = 4, IngredientId = 5, Quantity = 1 }, // Tomato
                new RecipeIngredient { RecipeId = 4, IngredientId = 6, Quantity = 2 }, // Cheese
                new RecipeIngredient { RecipeId = 4, IngredientId = 4, Quantity = 1 }, // Meat

                // Salad
                new RecipeIngredient { RecipeId = 5, IngredientId = 3, Quantity = 2 }, // Lettuce
                new RecipeIngredient { RecipeId = 5, IngredientId = 5, Quantity = 2 }, // Tomato
                new RecipeIngredient { RecipeId = 5, IngredientId = 1, Quantity = 1 }, // Cucumber
                new RecipeIngredient { RecipeId = 5, IngredientId = 6, Quantity = 2 }, // Cheese
                new RecipeIngredient { RecipeId = 5, IngredientId = 2, Quantity = 1 }, // Olives

                // Pizza
                new RecipeIngredient { RecipeId = 6, IngredientId = 7, Quantity = 3 }, // Dough
                new RecipeIngredient { RecipeId = 6, IngredientId = 5, Quantity = 2 }, // Tomato
                new RecipeIngredient { RecipeId = 6, IngredientId = 6, Quantity = 3 }, // Cheese
                new RecipeIngredient { RecipeId = 6, IngredientId = 2, Quantity = 1 }  // Olives
            );
        }
    }

}
