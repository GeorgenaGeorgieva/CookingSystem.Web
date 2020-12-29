namespace CookingSystem.Services
{
    using System;
    using System.Collections.Generic;
    using CookingSystem.Data.Models;

    public interface ICategoryService
    {
        void CreateNew(Category category);
        IEnumerable<Category> Listing();
    }
}
