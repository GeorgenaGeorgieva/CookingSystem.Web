namespace CookingSystem.Web.Controllers
{
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Services;
    using CookingSystem.Web.Models.Articles;
    using CookingSystem.Web.Models.Images;
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

        public ArticlesController(IArticleService articles,
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            this.articles = articles;
            this.userManager = userManager;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
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
            article.UserId = userId;
            article.Date = DateTime.UtcNow;

            this.articles.Create(article);

            return this.RedirectToAction("Index", "Home");
        }

        //private async Task<ArticleInputModel> AddImageToArticle(ArticleInputModel model, string folder, string wwwRootPath)
        //{
            

        //    return model;
        //}
    }
}
