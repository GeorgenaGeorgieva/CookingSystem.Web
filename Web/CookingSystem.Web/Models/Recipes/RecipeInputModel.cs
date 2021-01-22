namespace CookingSystem.Web.Models.Recipes
{
    using CookingSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeInputModel
    {
        public int Id { get; set; }


        [DataType(DataType.Date)]
        public DateTime PostedOn { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        public DifficultyLevel Level { get; set; }

        [Required]
        [MaxLength(3000)]
        public string ContentIngredients { get; set; }

        [MaxLength(500)]
        public string PreparationMethod { get; set; }

        public string UserId { get; set; }

        public ICollection<Image> ImageNames { get; set; } = new List<Image>();
    }
}
