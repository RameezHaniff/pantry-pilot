using Microsoft.EntityFrameworkCore;
using PantryPilot.Api.Data;
using PantryPilot.Api.Entities;
using PantryPilot.Api.Models.Requests;
using PantryPilot.Api.Services;

namespace PantryPilot.Tests.Services
{
    public class FoodOptimizerServiceTests
    {
        private static FoodDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<FoodDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FoodDbContext(options);
        }

        [Fact]
        public async Task OptimizeAsync_ShouldThrow_WhenIngredientsAreMissing()
        {
            // Arrange
            using var db = CreateDbContext();

            var service = new FoodOptimizationService(db);

            var request = new OptimizationRequest
            {
                Ingredients = []
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => service.OptimizeAsync(request));

            // Assert
            Assert.Equal(
                "At least one ingredient must be provided.",
                exception.Message);
        }

        [Fact]
        public async Task OptimizeAsync_ShouldThrow_WhenQuantityIsNegative()
        {
            // Arrange
            using var db = CreateDbContext();

            var service = new FoodOptimizationService(db);

            var request = new OptimizationRequest
            {
                Ingredients =
                [
                    new IngredientQuantity
                {
                    IngredientId = 1,
                    Quantity = -1
                }
                ]
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => service.OptimizeAsync(request));

            // Assert
            Assert.Equal(
                "Ingredient quantities cannot be negative.",
                exception.Message);
        }

        [Fact]
        public async Task OptimizeAsync_ShouldThrow_WhenDuplicateIngredientsExist()
        {
            // Arrange
            using var db = CreateDbContext();

            var service = new FoodOptimizationService(db);

            var request = new OptimizationRequest
            {
                Ingredients =
                [
                    new IngredientQuantity
                {
                    IngredientId = 1,
                    Quantity = 5
                },
                new IngredientQuantity
                {
                    IngredientId = 1,
                    Quantity = 10
                }
                ]
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => service.OptimizeAsync(request));

            // Assert
            Assert.Equal(
                "Duplicate ingredients are not allowed.",
                exception.Message);
        }

        [Fact]
        public async Task OptimizeAsync_ShouldReturnExpectedRecipeResults()
        {
            // Arrange
            using var db = CreateDbContext();

            var cheese = new Ingredient
            {
                Id = 1,
                Name = "Cheese"
            };

            db.Ingredients.Add(cheese);

            db.Recipes.Add(
                new Recipe
                {
                    Id = 1,
                    Name = "Cheese Sandwich",
                    Feeds = 2,
                    RecipeIngredients =
                    [
                        new RecipeIngredient
                    {
                        Ingredient = cheese,
                        Quantity = 2
                    }
                    ]
                });

            await db.SaveChangesAsync();

            var service = new FoodOptimizationService(db);

            var request = new OptimizationRequest
            {
                Ingredients =
                [
                    new IngredientQuantity
                {
                    IngredientId = 1,
                    Quantity = 4
                }
                ]
            };

            // Act
            var result = await service.OptimizeAsync(request);

            // Assert
            Assert.Equal(4, result.MaxPeopleFed);

            Assert.Single(result.Recipes);

            var recipe = result.Recipes[0];

            Assert.Equal("Cheese Sandwich", recipe.Name);
            Assert.Equal(2, recipe.Quantity);

            Assert.Single(recipe.Ingredients);

            var ingredient = recipe.Ingredients[0];

            Assert.Equal("Cheese", ingredient.Name);
            Assert.Equal(2, ingredient.QuantityPerServing);
            Assert.Equal(4, ingredient.TotalUsed);
        }

        [Fact]
        public async Task OptimizeAsync_ShouldReturnUnusedIngredients()
        {
            // Arrange
            using var db = CreateDbContext();

            var cheese = new Ingredient
            {
                Id = 1,
                Name = "Cheese"
            };

            db.Ingredients.Add(cheese);

            db.Recipes.Add(
                new Recipe
                {
                    Id = 1,
                    Name = "Toast",
                    Feeds = 1,
                    RecipeIngredients =
                    [
                        new RecipeIngredient
                    {
                        Ingredient = cheese,
                        Quantity = 2
                    }
                    ]
                });

            await db.SaveChangesAsync();

            var service = new FoodOptimizationService(db);

            var request = new OptimizationRequest
            {
                Ingredients =
                [
                    new IngredientQuantity
                {
                    IngredientId = 1,
                    Quantity = 5
                }
                ]
            };

            // Act
            var result = await service.OptimizeAsync(request);

            // Assert
            Assert.Single(result.UnusedIngredients);

            var unused = result.UnusedIngredients[0];

            Assert.Equal("Cheese", unused.Name);
            Assert.Equal(1, unused.Quantity);
        }

        [Fact]
        public async Task OptimizeAsync_ShouldReturnEmptyResult_WhenNoRecipesCanBeMade()
        {
            // Arrange
            using var db = CreateDbContext();

            var cheese = new Ingredient
            {
                Id = 1,
                Name = "Cheese"
            };

            db.Ingredients.Add(cheese);

            db.Recipes.Add(
                new Recipe
                {
                    Id = 1,
                    Name = "Toast",
                    Feeds = 1,
                    RecipeIngredients =
                    [
                        new RecipeIngredient
                    {
                        Ingredient = cheese,
                        Quantity = 10
                    }
                    ]
                });

            await db.SaveChangesAsync();

            var service = new FoodOptimizationService(db);

            var request = new OptimizationRequest
            {
                Ingredients =
                [
                    new IngredientQuantity
                {
                    IngredientId = 1,
                    Quantity = 1
                }
                ]
            };

            // Act
            var result = await service.OptimizeAsync(request);

            // Assert
            Assert.Equal(0, result.MaxPeopleFed);
            Assert.Empty(result.Recipes);

            Assert.Single(result.UnusedIngredients);

            Assert.Equal(
                "Cheese",
                result.UnusedIngredients[0].Name);

            Assert.Equal(
                1,
                result.UnusedIngredients[0].Quantity);
        }

    }
}
