namespace CookingSystem.Tests.Services
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Services.Implementations;
    using CookingSystem.Tests.Mocks;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class CategoryServiceTests
    {
        private CookingSystemDbContext context;
        private ICategoryService categoryService;

        [SetUp]
        public void Setup()
        {
            this.context = MockDbContext.GetContext();
            this.categoryService = new CategoryService(this.context);
        }

        [Test]
        public void CreateNewMethodShouldThrownIfModelIsNull()
        {
            Assert.That(() => this.categoryService.CreateNew(null),
                Throws.ArgumentException.With.Message.EqualTo("Category cannot be null."));
        }

        [TestCase(null)]
        [TestCase("OneReallyLongNameThatIsLongerThan30Characters")]
        public void CreateNewMethodShouldThrownIfCategoryNameIsInvalid(string name)
        {
            var category = new Category { Id = 1, Name = name };

            Assert.That(() => this.categoryService.CreateNew(category),
                Throws.ArgumentException.With.Message.EqualTo("Category's name cannot be null or more than 30 symbols."));
        }

        [Test]
        public void CreateNewMethodShouldAddCategoryCorrectly()
        {
            var category = new Category { Id = 1, Name = "Fish" };

            this.categoryService.CreateNew(category);
            var allCategories = this.context.Categories.ToList();

            Assert.AreEqual(1, allCategories.Count);
        }

        [Test]
        public void DeleteCategoryMethodShuildWorkCorrectly()
        {
            this.context.Categories.AddRange(
                new Category { Id = 5, Name = "Dessert" },
                new Category { Id = 25, Name = "Fish" });

            this.context.SaveChanges();

            this.categoryService.DeleteCategory(5);
            var deletedCategory = this.context.Categories.Where(x => x.IsDeleted == true).FirstOrDefault();

            Assert.AreEqual(true, deletedCategory.IsDeleted);
        }

        [TestCase(-2)]
        [TestCase(0)]
        public void DeleteCategoryMethodShouldThrowIfIdIsNegativeOrZero(int id)
        {
            Assert.That(() => this.categoryService.DeleteCategory(id),
                Throws.ArgumentException.With.Message.EqualTo("Id cannot be zero or negative number."));
        }

        [Test]
        public void ListingMethodShouldReturnListWithCategories()
        {
            this.context.Categories.AddRange(
                new Category { Id = 1, Name = "Fish", IsDeleted = false },
                new Category { Id = 2, Name = "Main Dish", IsDeleted = true },
                new Category { Id = 3, Name = "Dessert", IsDeleted = false });
            this.context.SaveChanges();

            var categories = this.categoryService.Listing().ToList();

            Assert.AreEqual(2, categories.Count);
        }
    }
}
