using AutoMapper;
using CookingSystem.Data.Models;
using CookingSystem.Services;
using CookingSystem.Web.Models.Comments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingSystem.Web.Controllers
{
    public class CommentsController : Controller
    {
        private ICommentService comments;
        private IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public CommentsController(ICommentService comments, IMapper mapper, UserManager<IdentityUser> userManager) 
        {
            this.comments = comments;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult All(int recipeId)
        {
            var commentsServiceModel = this.comments.Listing(recipeId);

            return this.View();
        }

        [HttpGet]
        public IActionResult Add(int recipeId)
        {
            var commentViewModel = new CommentViewModel();
            commentViewModel.RecipeId = recipeId;
            
            return this.View(commentViewModel);
        }

        [HttpPost]
        public IActionResult Add(CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var comment = this.mapper.Map<Comment>(model);
            var userId = this.userManager.GetUserId(HttpContext.User);
            comment.UserId = userId;
            comment.Date = DateTime.UtcNow;
            comment.RecipeId = model.RecipeId;
            this.comments.Post(comment);

            return this.RedirectToAction("Details", "Recipes", new { Id = model.RecipeId});
        }
    }
}
