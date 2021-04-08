namespace CookingSystem.Web.Models.Recipes
{
    using CookingSystem.Web.Models.Categories;
    using CookingSystem.Web.Models.Comments;
    using CookingSystem.Web.Models.Images;
    using System.Collections.Generic;

    public class RecipeDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        public string ContentIngredients { get; set; }

        public string PreparationMehtod { get; set; }

        public string UserName { get; set; }

        public List<ImageViewModel> Images {get; set;}

        public List<CommentListingViewModel> Comments { get; set; }

    }
}
