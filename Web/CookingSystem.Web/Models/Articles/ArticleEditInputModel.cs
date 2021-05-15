namespace CookingSystem.Web.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    public class ArticleEditInputModel
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
