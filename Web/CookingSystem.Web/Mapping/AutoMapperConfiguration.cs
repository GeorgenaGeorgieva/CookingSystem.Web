namespace CookingSystem.Web.Mapping
{
    using System;
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Web.Models;
    using CookingSystem.Web.Models.Categories;

    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<CategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<Category, CategoryListingViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));
        }
    }
}
