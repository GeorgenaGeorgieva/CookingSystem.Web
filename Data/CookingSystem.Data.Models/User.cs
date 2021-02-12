namespace CookingSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.IsSuspended = false;
            this.Recipes = new List<Recipe>();
            this.Comments = new List<Comment>();
            this.Articles = new List<Article>();
        }

        public bool? IsSuspended { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
