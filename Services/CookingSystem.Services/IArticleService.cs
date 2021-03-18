namespace CookingSystem.Services
{
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Articles;

    public interface IArticleService
    {
        void Create(Article model);
    }
}