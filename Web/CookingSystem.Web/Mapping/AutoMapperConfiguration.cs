namespace CookingSystem.Web.Mapping
{
    using System;
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Services.Models.Articles;
    using CookingSystem.Services.Models.Categories;
    using CookingSystem.Services.Models.Comments;
    using CookingSystem.Services.Models.Images;
    using CookingSystem.Services.Models.Recipes;
    using CookingSystem.Web.Models;
    using CookingSystem.Web.Models.Articles;
    using CookingSystem.Web.Models.Categories;
    using CookingSystem.Web.Models.Comments;
    using CookingSystem.Web.Models.Images;
    using CookingSystem.Web.Models.Recipes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<CategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<Category, CategoryListingViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<RecipeImagesModel, Image>()
            .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<IFormFile, Image>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<ImageServiceModel, ImageViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<RecipeInputModel, Recipe>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<IdentityUser, User>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.UserName));

            this.CreateMap<RecipeDetailsServiceModel, RecipeDetailsViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<RecipeDetailsServiceModel, RecipeEditInputModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<RecipeEditInputModel, RecipeEditServiceModel>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.Id));

            this.CreateMap<CommentListingServiceModel, CommentListingViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.Id));

            this.CreateMap<CommentInputModel, Comment>()
                .ForMember(x => x.Author, y => y.MapFrom(s => s.Author));

            this.CreateMap<ArticleInputModel, Article>()
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title));

            this.CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title));

            this.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title));

            this.CreateMap<Article, ArticleEditDetailsViewModel>()
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title));

            this.CreateMap<ArticleEditInputModel, ArticleEditServiceModel>()
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title));

        }
    }
}
