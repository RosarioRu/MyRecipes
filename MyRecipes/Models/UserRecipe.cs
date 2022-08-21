using System.Collections.Generic;

namespace MyRecipes.Models
{
    public class UserRecipe
    {
      public UserRecipe()
      {
        this.JoinEntities = new HashSet<CategoryUserRecipe>;
      }

      public int UserRecipeId { get; set; }
      public string RecipeName { get; set; }
      public string IngredientList { get; set; }
      public int RecipeRating { get; set; }

      public virtual ICollection<CategoryUserRecipe> JoinEntities { get; }
    }


    }
}
