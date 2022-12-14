using Microsoft.AspNetCore.Mvc.Rendering; //need this for SelectList.
using Microsoft.AspNetCore.Mvc; 
using MyRecipes.Models;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() method below

using System;

using Microsoft.EntityFrameworkCore; //so we can use EntityState to modify UserRecipe(s).


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
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
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

    [HttpGet]
    public ActionResult Details(int id)
    { //below we first say thisUserRecipe is a list of all UserRecipe(s) in database, then we 'load' the joinEntities of UserRecipes by saying .Include the UserRecipe(s) property called JoinEntities (list of relationships of UserRecipes and their categories), then load the categories by .ThenInclude the join.Category. This will return list of UserRecipes with the Categories of the CatgoryUserRecipe(s).  Finally we say the one we want is the the UserRecipe with the UserRecipeId equalling id.... I think?... so we return the UserRecipe along with associated categories object(s). ASK BECKET ABOUT THIS.
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      var thisUserRecipe = _db.UserRecipes
        .Include(UserRecipe => UserRecipe.JoinEntities)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(UserRecipe => UserRecipe.UserRecipeId == id);
      char[] delimiterChars = { ',', '.', ':',};
      string textIngredients = thisUserRecipe.IngredientList;
      string[] words = textIngredients.Split(delimiterChars);
      ViewBag.text= words;
      return View(thisUserRecipe);
    }

    

    [HttpGet]
    public ActionResult Edit(int id)
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      // var recipeToDisplay = _db.UserRecipes.FirstOrDefault(UserRecipe => UserRecipe.UserRecipeId == id);
      var recipeToDisplay = _db.UserRecipes
        .Include(UserRecipe => UserRecipe.JoinEntities)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(UserRecipe => UserRecipe.UserRecipeId == id);
      return View(recipeToDisplay);
    }

    [HttpPost]
    public ActionResult Edit(UserRecipe recipeToEdit, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryUserRecipes.Add(new CategoryUserRecipe() { CategoryId = CategoryId, UserRecipeId = recipeToEdit.UserRecipeId});
      }
      _db.Entry(recipeToEdit).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
      var thisUserRecipe = _db.UserRecipes.FirstOrDefault(UserRecipe => UserRecipe.UserRecipeId == id);
      return View(thisUserRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var recipeToDelete = _db.UserRecipes.FirstOrDefault(UserRecipe => UserRecipe.UserRecipeId == id);
      _db.UserRecipes.Remove(recipeToDelete);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, ActionName("DeleteCategoryUserRecipe")]
    public ActionResult DetailsAgain(int joinId)
    {
      var joinEntry = _db.CategoryUserRecipes.FirstOrDefault(entry => entry.CategoryUserRecipeId == joinId);
      _db.CategoryUserRecipes.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }   
  }
}
