namespace CookingSystem.Web.Models.Recipes
{
    using AutoMapper;
    using CookingSystem.Data.Models;
    using CookingSystem.Web.Models.Images;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Recipe title")]
        public string Name { get; set; }

        [Display(Name = "Cooking time /minutes/")]
        public int CookingTime { get; set; }

        [Display(Name = "Number of servings")]
        public int Portion { get; set; }

        [Display(Name = "Degree of Difficulty")]
        public string Level { get; set; }

        [Required]
        [MaxLength(3000)]
        [Display(Name = "Products (one per row) / Example: potatoes - 1 kg / *")]
        public string ContentIngredients { get; set; }

        [MaxLength(3000)]
        [Display(Name = "Method of preparation *")]
        public string PreparationMethod { get; set; }

        public string UserId { get; set; }
    }
}
