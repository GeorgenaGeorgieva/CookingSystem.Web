namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Comments;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CommentService : ICommentService
    {
        private CookingSystemDbContext context;

        public CommentService(CookingSystemDbContext context)
        {
            this.context = context;
        }

        public ICollection<CommentListingServiceModel> Listing(int recipeId)
        => this.context.Comments
            .Where(x => x.IsDeleted == false)
            .Where(x => x.RecipeId == recipeId)
            .Select(x => new CommentListingServiceModel
            {
                Id = x.Id,
                Author = x.Author,
                Date = x.Date.ToShortDateString(),
                UserId = x.UserId,
                UserName = x.User.UserName,
                Content = x.Content,
            })
            .ToList();

        public void Post(Comment comment)
        {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();
        }
    }
}
