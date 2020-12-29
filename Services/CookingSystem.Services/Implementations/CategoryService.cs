namespace CookingSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CookingSystem.Data;
    using CookingSystem.Data.Models;

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

        public IEnumerable<Category> Listing()
        => this.context
            .Categories
            .Select(x => new Category
            {
                Id = x.Id,
                Name = x.Name,
                Recipes = x.Recipes
            })
            .ToList();
        
    }
}
