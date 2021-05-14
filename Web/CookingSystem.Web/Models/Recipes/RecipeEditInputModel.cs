namespace CookingSystem.Web.Models.Recipes
{
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Categories;
    using CookingSystem.Web.Models.Images;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CategoryId { get; set; }


        [Display(Name = "Degree of Difficulty")]
        public DifficultyLevel Level { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        [Required]
        [MaxLength(3000)]
        public string ContentIngredients { get; set; }

        [MaxLength(3000)]
        public string PreparationMehtod { get; set; }

        public ICollection<CategoryListingServiceModel> Categories { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}
