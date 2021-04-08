namespace CookingSystem.Services.Models.Articles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ArticleServiceModel
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
