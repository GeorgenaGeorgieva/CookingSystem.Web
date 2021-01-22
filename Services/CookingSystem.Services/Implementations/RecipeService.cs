namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RecipeService : IRecipeService
    {
        private CookingSystemDbContext context;

        public RecipeService(CookingSystemDbContext context)
        {
            this.context = context;
        }

        public void Create(Recipe recipe)
        {
            this.context.Recipes.Add(recipe);
            this.context.SaveChanges();
        }
    }
}
