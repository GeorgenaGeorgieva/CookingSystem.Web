namespace CookingSystem.Services
{
    using System;
    using System.Collections.Generic;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Categories;

    public interface ICategoryService
    {
        void CreateNew(Category category);
        IEnumerable<Category> Listing();
        void DeleteCategory(int id);
        ICollection<CategoryListingServiceModel> ListingForDropdown();
    }
}
