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

        bool Exist(int id);
    }
}
