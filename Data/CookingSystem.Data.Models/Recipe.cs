namespace CookingSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        public Recipe()
        {
            this.Images = new List<Image>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        [MaxLength(500)]
        public string PreparationMethod { get; set; }

        public DifficultyLevel Level { get; set; }

        public bool IsDeleted { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        [Required]
        [MaxLength(3000)]
        public string ContentIngredients { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
