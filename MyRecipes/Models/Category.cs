using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace MyRecipes.Models
{
  public class Category
  {
    public Category()
      {
        this.JoinEntities = new HashSet<CategoryUserRecipe>();
      }

    [Display(Name = "Category ID")]
    public int CategoryId { get; set; }

    [Display(Name = "Category")]
    public string Name { get; set; }

    public virtual ICollection<CategoryUserRecipe> JoinEntities { get; set; } //This reference allows us to access related UserRecipe(s) in controllers and views.
  }
}

        
 