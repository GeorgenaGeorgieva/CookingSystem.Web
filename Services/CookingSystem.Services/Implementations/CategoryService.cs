namespace CookingSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Categories;

    public class CategoryService : ICategoryService
    {
        private CookingSystemDbContext context;

        public CategoryService(CookingSystemDbContext context)
        {
            this.context = context;
        }

        public void CreateNew(Category category)
        {
            if(category == null)
            {
                throw new ArgumentException("Category cannot be null.");
            }

            if(category.Name == null || category.Name.Length > 30)
            {
                throw new ArgumentException("Category's name cannot be null or more than 30 symbols.");
            }

            this.context
                .Categories
                .Add(category);

            this.context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id cannot be zero or negative number.");
            }

            if (!Exist(id))
            {
                throw new ArgumentException("There is no Category with given Id.");
            }

            var category = this.context
                .Categories
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            category.IsDeleted = true;

            this.context.SaveChanges();
        }

        public Category GetCategoryByName(string name)
        => this.context.Categories
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Name.ToLower() == name.ToLower())
            .FirstOrDefault();

        public IEnumerable<Category> Listing()
        => this.context
            .Categories
            .Where(x => x.IsDeleted == false)
            .Select(x => new Category
            {
                Id = x.Id,
                Name = x.Name,
                Recipes = x.Recipes
            })
            .ToList();

        public ICollection<CategoryListingServiceModel> ListingForDropdown()
        => this.context
            .Categories
            .Where(x => x.IsDeleted == false)
            .Select(x => new CategoryListingServiceModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        private bool Exist(int id)
        => this.context.Categories.Any(x => x.Id == id);
    }
}
