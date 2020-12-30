namespace CookingSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Http;

    public class Image
    { 

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public bool IsDeleted { get; set; }

        public Recipe Recipe { get; set; }

        public int? RecipeId { get; set; }
    }
}
