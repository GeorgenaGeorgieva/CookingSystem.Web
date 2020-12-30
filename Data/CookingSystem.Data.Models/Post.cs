namespace CookingSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        public Recipe Recipe { get; set; }

        public int RecipeId { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
