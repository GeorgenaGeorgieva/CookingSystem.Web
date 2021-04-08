namespace CookingSystem.Web.Controllers
{
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Web.Models.Articles;
    using CookingSystem.Web.Models.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
     
    public class ArticlesController : Controller
    {
        private IArticleService articles;
        private IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IUserService userService;

        public ArticlesController(IArticleService articles,
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IUserService userService)
        {
            this.articles = articles;
            this.userManager = userManager;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title, Content, Url, ImageFile")] ArticleInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var wwwRootPath = this.webHostEnvironment.WebRootPath;
            var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            var extension = Path.GetExtension(model.ImageFile.FileName);
            model.Url = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            var path = Path.Combine(wwwRootPath + "/img/art/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            var userId = this.userManager.GetUserId(this.HttpContext.User);
            var article = this.mapper.Map<Article>(model);

            article.Date = DateTime.UtcNow;
            article.UserId = userId;

            this.articles.Create(article);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var listOfArticles = this.articles
                .Listing()
                .Select(x => this.mapper.Map<ArticleViewModel>(x));

            return this.View(listOfArticles);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            if (!this.articles.Exist(id))
            {
                return this.BadRequest();
            }

            var article = this.articles.FindById(id);
            var articleDetailsViewModel = this.mapper.Map<ArticleDetailsViewModel>(article);
            articleDetailsViewModel.Date = article.Date.ToShortDateString();
            articleDetailsViewModel.UserName = article.User.UserName;

            return this.View(articleDetailsViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (!this.articles.Exist(id))
            {
                return this.NotFound("There is no article with given Id.");
            }

            this.articles.Delete(id);

            return this.RedirectToAction("All", "Articles");
        }
    }
}
