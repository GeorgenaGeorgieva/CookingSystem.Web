namespace CookingSystem.Services.Models.Comments
{
    using CookingSystem.Data.Models;

    public class CommentListingServiceModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
