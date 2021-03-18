namespace CookingSystem.Services.Implementations
{
    using CookingSystem.Data;
    using CookingSystem.Services.Models.Images;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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

        public void DeleteImage(int imgId)
        {
            if (!Exist(imgId))
            {
                throw new ArgumentException("There is no img with given Id.");
            }

            var img = this.context.Images.FirstOrDefault(x => x.Id == imgId);
            img.IsDeleted = true;
            this.context.SaveChanges();
        }

        private bool Exist(int imgId)
            => this.context.Images.Where(x => x.IsDeleted == false).Any(x => x.Id == imgId);
    }
}