namespace CookingSystem.Services
{
    using CookingSystem.Services.Models.Images;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IImageSevice
    {
        ICollection<ImageServiceModel> GetRecipeImages(int recipeId);
        void DeleteImage(int imgId);
    }
}
