namespace CookingSystem.Services
{
    using CookingSystem.Data.Models;
    using System.Collections.Generic;

    public interface IArticleService
    {
        ICollection<Article> Listing();
        void Create(Article model);
        Article FindById(int id);
        bool Exist(int id);
        void Delete(int id);
    }
}