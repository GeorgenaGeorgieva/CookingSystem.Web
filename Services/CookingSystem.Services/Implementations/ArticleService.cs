namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;

    public class ArticleService : IArticleService
    {
        private CookingSystemDbContext context;

        public ArticleService(CookingSystemDbContext context)
        {
            this.context = context;
        }

        public void Create(Article model)
        {
            this.context.Articles.Add(model);
            this.context.SaveChanges();
        }
    }
}
