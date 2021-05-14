using System.ComponentModel.DataAnnotations;

namespace CookingSystem.Web.Models.Articles
{
    public class ArticleEditDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Съсържание")]
        public string Content { get; set; }
    }
}
