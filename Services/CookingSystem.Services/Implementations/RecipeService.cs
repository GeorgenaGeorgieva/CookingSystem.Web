namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Recipes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            .Where(x => x.IsDeleted == false)
            .Select(x => new Recipe
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Level = x.Level,
                User = x.User,
                Images = x.Images,
            })
            .ToList();


        public void Create(Recipe recipe)
        {
            if (String.IsNullOrWhiteSpace(recipe.Name) || String.IsNullOrEmpty(recipe.Name) || recipe.Name.Length > 30)
            {
                throw new ArgumentException("Recipe name cannot be null or more thsn 30 characters.");
            }
            if (recipe.CookingTime <= 0)
            {
                throw new ArgumentException("Cooking time be negative number or equal to zero.");
            }
            if(recipe.Portion <= 0)
            {
                throw new ArgumentException("Portion cannot be negative number or equal to zero.");
            }
            if(String.IsNullOrEmpty(recipe.ContentIngredients) || String.IsNullOrWhiteSpace(recipe.ContentIngredients))
            {
                throw new ArgumentException("Content Ingredients property cannot be null.");
            }

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

        public void Delete(int id)
        {
            var recipe = this.context.Recipes.FirstOrDefault(x => x.Id == id);
            recipe.IsDeleted = true;

            this.context.SaveChanges();
        }

        public ICollection<Recipe> GetMyRecipes(string userId)
        => this.context.Recipes
            .Where(x => x.IsDeleted == false)
            .Where(x => x.UserId == userId)
            .Select(x => new Recipe
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Portion = x.Portion,
                User = x.User,
                Images = x.Images,
            })
            .ToList();

    }


}
