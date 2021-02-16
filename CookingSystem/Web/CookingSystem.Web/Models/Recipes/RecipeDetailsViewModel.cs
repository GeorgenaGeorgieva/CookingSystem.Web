﻿using CookingSystem.Web.Models.Images;
using System.Collections.Generic;

namespace CookingSystem.Web.Models.Recipes
{
    public class RecipeDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        public string ContentIngredients { get; set; }

        public string PreparationMehtod { get; set; }

        public ICollection<ImageViewModel> Images {get; set;}
    }
}
