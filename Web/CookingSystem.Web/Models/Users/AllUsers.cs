using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CookingSystem.Web.Models.Users
{
    public class AllUsers
    {
        public List<IdentityUser> Users { get; set; }
    }
}
