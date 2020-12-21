namespace CookingSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        public Recipe()
        {
            this.Ingredients = new List<Ingredient>();
            this.Images = new List<Image>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        [MaxLength(500)]
        public string PreparationMethod { get; set; }

        public DifficultyLevel Level { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
