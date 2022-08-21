using System.Collections.Generic;

namespace MyRecipes.Models
{
  public class Category
    {
      public Category()
        {
            this.JoinEntities = new HashSet<CategoryUserRecipe>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryUserRecipe> JoinEntities { get; set; } //This reference allows us to access related UserRecipe(s) in controllers and views.
    }
}

        
 