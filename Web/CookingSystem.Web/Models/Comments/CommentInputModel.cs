namespace CookingSystem.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Author { get; set; }

        [Required]
        [MaxLength(250)]
        public string Content { get; set; }

        public int RecipeId { get; set; }
    }
}
