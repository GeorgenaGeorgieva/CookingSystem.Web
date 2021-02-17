namespace CookingSystem.Tests.Services
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Services.Implementations;
    using CookingSystem.Tests.Mocks;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RecipeServiceTests
    {
        private CookingSystemDbContext context;
        private IRecipeService recipeService;
        [SetUp]
        public void Setup()
        {
            this.context = MockDbContext.GetContext();
            this.recipeService = new RecipeService(this.context);
        }

        [TestCase(null)]
        [TestCase("OneReallyLongNameMoreThan30Symbols")]
        public void CreateRecipeMethodShouldThrowIfRecipeNameIsInvalid(string name)
        {
            var recipe = new Recipe
            {
                Id = 1,
                Name = name,
                Date = DateTime.Now,
                CategoryId = 1,
                CookingTime = 20,
                Portion = 4,
                Level = DifficultyLevel.Low,
                ContentIngredients = "onion, oil, salt",
                PreparationMethod = "abc"
            };

            Assert.That(() => this.recipeService.Create(recipe),
                Throws.ArgumentException.With.Message.EqualTo("Recipe name cannot be null or more thsn 30 characters."));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CreateRecipeMethodShouldThrowIfRecipeCookingTimeIsNegative(int time)
        {
            var recipe = new Recipe
            {
                Id = 1,
                Name = "Soup",
                Date = DateTime.Now,
                CategoryId = 1,
                CookingTime = time,
                Portion = 4,
                Level = DifficultyLevel.Low,
                ContentIngredients = "onion, oil, salt",
                PreparationMethod = "abc"
            };

            Assert.That(() => this.recipeService.Create(recipe),
                Throws.ArgumentException.With.Message.EqualTo("Cooking time be negative number or equal to zero."));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CreateRecipeMethodShouldThrowIfRecipePortionIsNegative(int portion)
        {
            var recipe = new Recipe
            {
                Id = 1,
                Name = "Soup",
                Date = DateTime.Now,
                CategoryId = 1,
                CookingTime = 20,
                Portion = portion,
                Level = DifficultyLevel.Low,
                ContentIngredients = "onion, oil, salt",
                PreparationMethod = "abc"
            };

            Assert.That(() => this.recipeService.Create(recipe),
                Throws.ArgumentException.With.Message.EqualTo("Portion cannot be negative number or equal to zero."));
        }

        [TestCase(null)]
        public void CreateRecipeMethodShouldThrowIfContentIngredientsAreNull(string ingredient)
        {
            var recipe = new Recipe
            {
                Id = 1,
                Name = "Soup",
                Date = DateTime.Now,
                CategoryId = 1,
                CookingTime = 20,
                Portion = 4,
                Level = DifficultyLevel.Low,
                ContentIngredients = ingredient,
                PreparationMethod = "abc"
            };

            Assert.That(() => this.recipeService.Create(recipe),
                Throws.ArgumentException.With.Message.EqualTo("Content Ingredients property cannot be null."));
        }

    }
}
