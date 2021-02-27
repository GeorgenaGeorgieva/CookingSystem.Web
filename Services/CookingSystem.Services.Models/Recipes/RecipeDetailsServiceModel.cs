namespace CookingSystem.Services.Models.Recipes
{
    public class RecipeDetailsServiceModel
    {
        public int Id {get; set;}

        public string Name { get; set; }

        public string Date { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public int CookingTime { get; set; }

        public int Portion { get; set; }

        public string ContentIngredients { get; set; }

        public string PreparationMehtod { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
