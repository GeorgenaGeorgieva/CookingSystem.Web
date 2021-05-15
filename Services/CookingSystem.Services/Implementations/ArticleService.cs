namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Articles;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticleService : IArticleService
    {
        private CookingSystemDbContext context;

        public ArticleService(CookingSystemDbContext context)
        => this.context = context;
        

        public void Create(Article model)
        {
            this.context.Articles.Add(model);
            this.context.SaveChanges();
        }

        public Article FindById(int id)
        => this.context.Articles
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Id == id)
            .Select(x => new Article
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Date = x.Date,
                User = x.User,
                UserId = x.UserId,
                Url = x.Url,
            })
            .FirstOrDefault();

        public bool Exist(int id)
        => this.context.Articles.Any(x => x.Id == id);

        public ICollection<Article> Listing()
        => this.context.Articles
            .Where(x => x.IsDeleted == false)
            .Select(x => new Article
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url
            })
            .ToList();

        public void Delete(int id)
        {
            if (!this.Exist(id))
            {
                throw new ArgumentException("There is no article with given id.");
            }

            var article = this.FindById(id);
            article.IsDeleted = true;

            this.context.SaveChanges();
        }

        public void Edit(ArticleEditServiceModel model)
        {
            if (!this.Exist(model.Id))
            {
                throw new ArgumentException("There is no article with given Id.");
            }

            if(String.IsNullOrEmpty(model.Title) || String.IsNullOrWhiteSpace(model.Title))
            {
                throw new ArgumentException("The 'Title' cannot be null.");
            }

            if(String.IsNullOrWhiteSpace(model.Content) || String.IsNullOrEmpty(model.Content))
            {
                throw new ArgumentException("The 'Content' cannot be null.");
            }

            var article = this.context.Articles.Where(x => x.IsDeleted == false).FirstOrDefault();
            article.Title = model.Title;
            article.Content = model.Content;

            this.context.SaveChanges();
        }
    }
}
