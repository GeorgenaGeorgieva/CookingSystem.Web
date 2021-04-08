namespace CookingSystem.Web.Controllers
{
    using CookingSystem.Web.Data;
    using CookingSystem.Web.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin, Manager")]
    public class UsersController : Controller
    {
        private ApplicationDbContext context;
        private UserManager<IdentityUser> userManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult All()
        {
            var users = this.context.Users.ToList();

            var allUsersModel = new AllUsers
            {
                Users = users
            };

            return View(allUsersModel);
        }

        public IActionResult Delete(string id)
        {
            var user = this.context.Users.Where(x => x.Id == id).FirstOrDefault();


            if (user == null)
            {
                return this.NotFound();
            }

            this.context.Users.Remove(user);
            this.context.SaveChanges();

            return this.RedirectToAction("All", "Users");
        }
    }
}
