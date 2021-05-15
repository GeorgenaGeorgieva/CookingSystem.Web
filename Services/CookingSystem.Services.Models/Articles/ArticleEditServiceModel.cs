namespace CookingSystem.Services.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    public class ArticleEditServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
