using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyRecipes.Models
{
  public class MyRecipesContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Category> Categories { get; set; }//adds a Category DbSet to MyRecipesContext.cs
    public DbSet<UserRecipe> UserRecipes { get; set; }
    public DbSet<CategoryUserRecipe> CategoryUserRecipes {get; set; }

    public MyRecipesContext(DbContextOptions options) : base(options) { }
    
    //below OnConfiguing enables lazy-loading
    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}