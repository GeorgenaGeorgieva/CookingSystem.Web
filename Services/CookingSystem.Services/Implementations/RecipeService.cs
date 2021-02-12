namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Recipes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RecipeService : IRecipeService
    {
        private CookingSystemDbContext context;

        public RecipeService(CookingSystemDbContext context)
        {
            this.context = context;
        }

        public ICollection<Recipe> Listing()
        => this.context
            .Recipes
            .Select(x => new Recipe
            {
                Id = x.Id,
                Date = x.Date,
                Name = x.Name,
                Category = x.Category,
                Level = x.Level,
                Images = x.Images,
            })
            .ToList();


        public void Create(Recipe recipe)
        {
            this.context.Recipes.Add(recipe);
            this.context.SaveChanges();
        }

        public RecipeDetailsServiceModel FindById(int id)
        => this.context.Recipes
            .Where(x => x.Id == id)
            .Where(y => y.IsDeleted == false)
            .Select(x => new RecipeDetailsServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Date = x.Date.ToLongDateString(),
                Portion = x.Portion,
                Level = x.Level.ToString(),
                ContentIngredients = x.ContentIngredients,
                PreparationMehtod = x.PreparationMethod,
                Category = x.Category.Name,
                CookingTime = x.CookingTime,
            })
            .FirstOrDefault();


        public bool Exist(int id)
        => this.context.Recipes.Any(x => x.Id == id);
    }

                
}
