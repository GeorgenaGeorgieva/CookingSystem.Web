namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Services.Models.Images;
    using System.Collections.Generic;
    using System.Linq;

    public class ImageService : IImageSevice
    {
        private CookingSystemDbContext context;

        public ImageService(CookingSystemDbContext context)
        {
            this.context = context;
        }

        public ICollection<ImageServiceModel> GetRecipeImages(int recipeId)
        => this.context.Images
            .Where(x => x.IsDeleted == false)
            .Where(x => x.RecipeId == recipeId)
            .Select(x => new ImageServiceModel
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();
    }
}