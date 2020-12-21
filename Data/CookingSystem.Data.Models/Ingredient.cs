namespace CookingSystem.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public double Qunatity { get; set; }

        public Unit Unit { get; set; }

        public Recipe Recipe { get; set; }

        public int RecipeId { get; set; }
    }
}
