namespace CookingSystem.Web.Mapping
{
    using System;
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Web.Models;
    using CookingSystem.Web.Models.Categories;
    using CookingSystem.Web.Models.Images;
    using CookingSystem.Web.Models.Recipes;
    using Microsoft.AspNetCore.Identity;

    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<CategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<Category, CategoryListingViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<ImageModel, Image>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));
            
            this.CreateMap<RecipeInputModel, Recipe>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<IdentityUser, User>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.UserName));
                
        }
    }
}
