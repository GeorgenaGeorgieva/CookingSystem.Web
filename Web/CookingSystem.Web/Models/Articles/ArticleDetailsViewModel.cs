namespace CookingSystem.Web.Models.Articles
{
    using CookingSystem.Web.Models.Images;

    public class ArticleDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public string UserName { get; set; }

        public string Url { get; set; } 
    }
}
