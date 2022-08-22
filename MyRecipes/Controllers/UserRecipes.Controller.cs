using Microsoft.AspNetCore.Mvc.Rendering; //need this for SelectList.
using Microsoft.AspNetCore.Mvc; 
using MyRecipes.Models;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() method below
using Microsoft.EntityFrameworkCore; //so we can use EntityState to modify Item(s).


namespace MyRecipes.Controllers
{
  public class UserRecipesController : Controller
  {
    //a private and readonly field of type 'My RecipesContext' named _db
    private readonly MyRecipesContext _db;
    //below constructor sets value of _db property to My RecipesContext. we can due this bc of a 'dependency injection' we set up in startup.cs
    public UserRecipesController(MyRecipesContext db) 
    {
      _db = db;
    } 

    //below we are able to access all UserRecipe(s) in list form by usign LINQ ToList()
    //_db's value is db, which is an instance of DbContext class. It holds reference to te database.
    public ActionResult Index()
    {
      return View(_db.UserRecipes.ToList());
    }

    [HttpGet]
    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");//might put this in edit() so that user can add multiple categories not just ONE...at one time
      return View();
    }

    [HttpPost]
    public ActionResult Create(UserRecipe userRecipe, int CategoryId)
    {
      _db.UserRecipes.Add(userRecipe);
      _db.SaveChanges();
      if (CategoryId != 0)
      {
        _db.CategoryUserRecipes.Add(new CategoryUserRecipe() { CategoryId = CategoryId, UserRecipeId = userRecipe.UserRecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }


  }
}
