using Microsoft.EntityFrameworkCore;
using PantryPilot.Api.Data;
using PantryPilot.Api.Entities;
using PantryPilot.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantryPilot.Tests.Services
{
    public class IngredientServiceTests
    {
        private static FoodDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<FoodDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FoodDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoIngredientsExist()
        {
            // Arrange
            using var db = CreateDbContext();

            var service = new IngredientService(db);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllIngredients()
        {
            // Arrange
            using var db = CreateDbContext();

            db.Ingredients.AddRange(
                new Ingredient
                {
                    Id = 1,
                    Name = "Cheese"
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Bread"
                });

            await db.SaveChangesAsync();

            var service = new IngredientService(db);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldMapIngredientPropertiesCorrectly()
        {
            // Arrange
            using var db = CreateDbContext();

            db.Ingredients.Add(
                new Ingredient
                {
                    Id = 1,
                    Name = "Cheese"
                });

            await db.SaveChangesAsync();

            var service = new IngredientService(db);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Single(result);

            var ingredient = result[0];

            Assert.Equal(1, ingredient.Id);
            Assert.Equal("Cheese", ingredient.Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMultipleIngredientsWithCorrectData()
        {
            // Arrange
            using var db = CreateDbContext();

            db.Ingredients.AddRange(
                new Ingredient
                {
                    Id = 1,
                    Name = "Cheese"
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Bread"
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "Tomato"
                });

            await db.SaveChangesAsync();

            var service = new IngredientService(db);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count);

            Assert.Contains(result, x =>
                x.Id == 1 &&
                x.Name == "Cheese");

            Assert.Contains(result, x =>
                x.Id == 2 &&
                x.Name == "Bread");

            Assert.Contains(result, x =>
                x.Id == 3 &&
                x.Name == "Tomato");
        }
    }
}
