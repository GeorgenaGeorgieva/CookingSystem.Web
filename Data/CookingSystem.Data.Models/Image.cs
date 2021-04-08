namespace CookingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    { 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public Recipe Recipe { get; set; }

        public int? RecipeId { get; set; }
    }
}
