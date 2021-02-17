namespace CookingSystem.Web.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Web.Models;
    using CookingSystem.Web.Models.Categories;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class CategoriesController : Controller
    {
        private ICategoryService categories;
        private IMapper mapper;

        public CategoriesController(ICategoryService ctegories, IMapper mapper)
        {
            this.categories = ctegories;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var category = this.mapper.Map<Category>(model);
            this.categories.CreateNew(category);

            return this.RedirectToAction("All", "Categories");
        }

        public IActionResult All()
        {
            var categoriesList = this.categories.Listing();

            var categoriesViewModel = categoriesList
                .Select(x =>this.mapper.Map<CategoryListingViewModel>(x))
                .ToList();

            return this.View(categoriesViewModel);
        }

        public IActionResult Delete(int id)
        {
            this.categories.DeleteCategory(id);

            return this.RedirectToAction("All", "Categories");
        }
    }
}
