namespace CookingSystem.Web.Controllers
{
    using AutoMapper;
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Services.Models.Recipes;
    using CookingSystem.Web.Data;
    using CookingSystem.Web.Models.Images;
    using CookingSystem.Web.Models.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipesController : Controller
    {
        private IRecipeService recipes;
        private IMapper mapper;
        private IUserService userService;
        private ICategoryService categories;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly CookingSystemDbContext appContext;
        private readonly IImageSevice images;

        public RecipesController(IRecipeService recipes,
            IMapper mapper, IUserService userService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            CookingSystemDbContext appContext,
            ICategoryService categories,
            IImageSevice images)
        {
            this.recipes = recipes;
            this.mapper = mapper;
            this.userService = userService;
            this.userManager = userManager;
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.appContext = appContext;
            this.categories = categories;
            this.images = images;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var loggedUser = this.HttpContext.User.Identity.Name;

            if (loggedUser == null)
            {
                return this.View();
            }

            var identityUser = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == loggedUser);
            var user = this.mapper.Map<User>(identityUser);

            if(!this.appContext.Users.Any(x => x.UserName == loggedUser))
            {
                this.appContext.Users.Add(user);
                this.appContext.SaveChanges();
            }

            var recipeViewModel = new RecipeViewModel
            {
                Categories = this.categories.ListingForDropdown()
            };

            return this.View(recipeViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(RecipeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.userManager.GetUserId(HttpContext.User);
            var recipe = this.mapper.Map<Recipe>(model);

            recipe.Level = Enum.Parse<DifficultyLevel>(model.Level);
            recipe.Date = DateTime.Now;
            recipe.UserId = userId;

            this.recipes.Create(recipe);

            return this.RedirectToAction("All", "Recipes");
        }

        [HttpGet]
        public IActionResult All()
        {
            var allRecipes = this.recipes.Listing();

            var recipesViewModel = allRecipes
                .Select(x => new RecipeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    Level = x.Level,
                    UserName = x.User.UserName,
                    MainImage = x.Images.Select(i => i.Name).FirstOrDefault()
                })
                .ToList();

            return this.View(recipesViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.recipes.Exist(id))
            {
                return this.NotFound("There is no recipe with given Id");
            }

            var recipeServiceModel = this.recipes.FindById(id);
            var recipe = this.mapper.Map<RecipeDetailsViewModel>(recipeServiceModel);
            var recipeImages = this.images.GetRecipeImages(id);
            recipe.Images = recipeImages.Select(x => this.mapper.Map<ImageViewModel>(x)).ToList();

            return this.View(recipe);
        }

        public IActionResult Delete(int id)
        {
            if (!this.recipes.Exist(id))
            {
                return this.NotFound("There is no recipe with given id.");
            }

            this.recipes.Delete(id);

            return this.RedirectToAction("All", "Recipes");
        }

        [HttpGet]
        [Authorize]
        public IActionResult MyRecipes()
        {
            var userId = this.userManager.GetUserId(HttpContext.User);

            if(userId == null)
            {
                return this.BadRequest("There is no user with Id = null.");
            }

            var myRecipes = this.recipes.GetMyRecipes(userId);

            var myRecipesViewModel = myRecipes
                .Select(x => new RecipeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    Level = x.Level,
                    UserName = x.User.UserName,
                    MainImage = x.Images.Select(i => i.Name).FirstOrDefault()
                })
                .ToList();

            return this.View(myRecipesViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.recipes.Exist(id))
            {
                return this.NotFound();
            }

            var recipeDetailsServiceModel = this.recipes.FindById(id);
            var recipeEditInputModel = this.mapper.Map<RecipeEditInputModel>(recipeDetailsServiceModel);

            recipeEditInputModel.Categories = this.categories.ListingForDropdown();
            recipeEditInputModel.Level = Enum.Parse<DifficultyLevel>(recipeDetailsServiceModel.Level);

            return this.View(recipeEditInputModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(RecipeEditInputModel model)
        {
            if (!this.recipes.Exist(model.Id))
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var recipeEditServiceModel = this.mapper.Map<RecipeEditServiceModel>(model);

            this.recipes.Edit(recipeEditServiceModel);

            //TODO: Images
            return this.RedirectToAction("Details", "Recipes", new { Id = model.Id });
        }
    }
}
