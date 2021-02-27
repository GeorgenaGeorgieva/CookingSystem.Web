using CookingSystem.Data.Models;
using CookingSystem.Services.Models.Recipes;
using System.Collections.Generic;

namespace CookingSystem.Services
{
    public interface IRecipeService
    {
        ICollection<Recipe> Listing();
        void Create(Recipe recipe);
        RecipeDetailsServiceModel FindById(int id);
        ICollection<Recipe> GetMyRecipes(string userId);
        bool Exist(int id);
        void Delete(int id);
        void Edit(RecipeEditServiceModel model);
    }
}
