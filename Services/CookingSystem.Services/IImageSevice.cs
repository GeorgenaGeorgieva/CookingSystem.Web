namespace CookingSystem.Services
{
    using CookingSystem.Services.Models.Images;
    using System.Collections.Generic;

    public interface IImageSevice
    {
        ICollection<ImageServiceModel> GetRecipeImages(int recipeId);
    }
}
