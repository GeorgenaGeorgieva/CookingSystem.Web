namespace CookingSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Image
    { 

        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public Recipe Recipe { get; set; }

        public int RecipeId { get; set; }
    }
}
