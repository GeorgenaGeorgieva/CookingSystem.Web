namespace CookingSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public Article()
        {
            this.Date = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
