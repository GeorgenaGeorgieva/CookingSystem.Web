using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CookingSystem.Data;
using CookingSystem.Data.Models;
using CookingSystem.Services;
using CookingSystem.Web.Models.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CookingSystem.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly CookingSystemDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IMapper mapper;
        private IRecipeService recipes;

        public ImagesController(CookingSystemDbContext context,
                                    IWebHostEnvironment webHostEnvironment,
                                    IMapper mapper,
                                    IRecipeService recipes)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.recipes = recipes;
        }

        [HttpGet]
        public IActionResult All()
        {
            var images = this.context
                .Images
                .Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return this.View(images);
        }

        [HttpGet]
        public IActionResult AddImage(int recipeId)
        {
            if (!this.recipes.Exist(recipeId))
            {
                return this.NotFound("There is no recipe with given id");
            }

            var imageModel = new ImageModel();
            imageModel.RecipeId = recipeId;

            return this.View(imageModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage([Bind("Id,Name,ImageFile,RecipeId")] ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(imageModel);
            }

            string wwwRootPath = this.webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            imageModel.Name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/img/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }

            var image = this.mapper.Map<Image>(imageModel);
            this.context.Add(image);
            await this.context.SaveChangesAsync();

            return RedirectToAction("Details", "Recipes", new { id = imageModel.RecipeId });
        }
    }
}
