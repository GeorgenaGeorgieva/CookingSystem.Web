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
            this.context
                .Categories
                .Add(category);

            this.context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = this.context
                .Categories
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            category.IsDeleted = true;

            this.context.SaveChanges();
        }

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
    }
}
