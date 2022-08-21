using System.Collections.Generic;


namespace MyRecipes.Models
{
  public class CategoryUserRecipe
    {
      public int CategoryUserRecipeId { get; set; }
      public int CategoryId { get; set; }
      public int UserRecipeId { get; set; }
      public virtual Category Category { get; set; }
      public virtual UserRecipe UserRecipe { get; set; }
    }
}