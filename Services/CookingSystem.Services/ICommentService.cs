namespace CookingSystem.Services
{
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Comments;
    using System.Collections.Generic;
    public interface ICommentService
    {
        ICollection<CommentListingServiceModel> Listing(int recipeId);
        void Post(Comment comment);
    }
}
