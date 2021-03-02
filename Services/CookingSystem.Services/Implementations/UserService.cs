namespace CookingSystem.Services.Implementations
{
    using AutoMapper;
    using CookingSystem.Data;
    using CookingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class UserService : IUserService
    {

        private readonly CookingSystemDbContext context;
        private IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        public UserService(CookingSystemDbContext context, 
                            IMapper mapper,
                            UserManager<IdentityUser> userManager)
        {
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = new List<User>();

            foreach (var user in this.context.Users)
            {
                var isAdmin = await this.userManager.IsInRoleAsync(user, "Administrator");
                if (!isAdmin)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public async Task<User> GetUserByEmail(string email)
        => await this.context.Users.Include(e => e.Email).FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetUserById(string id)
        => await this.context.Users.Include(e => e.Id).FirstOrDefaultAsync(x => x.Id == id);
    }
}
