namespace CookingSystem.Web.Models.Recipes
{
    using CookingSystem.Services.Models.Categories;
    using System.Collections.Generic;

    public class AllRecipesViewModel
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public ICollection<CategoryListingServiceModel> Categories { get; set; }

        public int CategoryId { get; set; }
    }
}
