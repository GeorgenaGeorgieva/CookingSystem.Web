namespace CookingSystem.Web.Controllers
{
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Web.Data;
    using CookingSystem.Web.Models.Recipes;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RecipesController : Controller
    {
        private IRecipeService recipes;
        private IMapper mapper;
        private IUserService userService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public RecipesController(IRecipeService recipes, 
            IMapper mapper, IUserService userService, 
            UserManager<IdentityUser> userManager, 
            ApplicationDbContext context)
        {
            this.recipes = recipes;
            this.mapper = mapper;
            this.userService = userService;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var loggedUser = this.HttpContext.User.Identity.Name;
            var user = this.userService.GetUserByEmail(loggedUser);

            if(user == null || loggedUser == null)
            {
                return this.View();
            }

            if (user.IsCanceled)
            {
                return RedirectToAction("Index", "Identity/Account/Manage");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.userManager.GetUserId(HttpContext.User);
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var recipe = this.mapper.Map<Recipe>(model);
            recipe.UserId = userId;
            recipe.User = this.mapper.Map<User>(user);
            this.recipes.Create(recipe);

            return this.RedirectToAction("All", "Recipes");
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}
