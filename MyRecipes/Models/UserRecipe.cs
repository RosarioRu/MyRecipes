using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace MyRecipes.Models
{
  public class UserRecipe
  {
    public UserRecipe()
    {
      this.JoinEntities = new HashSet<CategoryUserRecipe>();
    }

    [Display(Name = "Recipe ID")]
    public int UserRecipeId { get; set; }

    [Display(Name = "Recipe Name")]
    public string RecipeName { get; set; }

    [Display(Name = "Ingredients")]
    public string IngredientList { get; set; }

    [Display(Name = "0-5 Star Rating")]
    public int RecipeRating { get; set; }

    public virtual ICollection<CategoryUserRecipe> JoinEntities { get; }
  }
}

 