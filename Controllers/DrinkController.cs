using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Controllers
{
    public class DrinkController:Controller
    {
        private readonly ICategoryRepository _categoryRepository; //we get the data for the categories through the interface for the repository
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public DrinkController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository, ICommentsRepository commentsRepository, UserManager<IdentityUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _commentsRepository = commentsRepository;
            _userManager = userManager;
            _drinkRepository = drinkRepository; //dependency injection
        }

        public ViewResult List(string category) //get the parameter
        {
            string _category = category;
            IEnumerable<Drink> drinks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category)) //if there is no parameter, just show all drinks
            {
                drinks = _drinkRepository.Drinks.OrderBy(n => n.DrinkId); // SELECT * FROM Drinks ORDER BY DrinkId
                currentCategory = "All drinks";
            }
            else // process the 2 categories
            {
                if (string.Equals("Alcoholic",_category,StringComparison.OrdinalIgnoreCase)) //get the drinks based on the category passed
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p => p.Name); //query the DB through the Repository to get the Alcoholic Drinks
                    // SELECT * FROM Drinks D JOIN Categories C ON D.CategoryId = C.CategoryId WHERE C.CategoryName ='Alcoholic' ORDER BY D.Name
                }
                else
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(p => p.Name); //query the DB through the Repository to get the Non-alcoholic Drinks
                    // SELECT * FROM Drinks D JOIN Categories C ON D.CategoryId = C.CategoryId WHERE C.CategoryName ='Non-alcoholic' ORDER BY D.Name
                }
                currentCategory = _category; // Used for the View, to display the current category of drinks to the user
            }

            var drinkListViewMidel = new DrinkListViewModel //create the ViewModel with the variables and return it
            {
                Drinks = drinks,
                CurrentCategory = currentCategory
            };

            return View(drinkListViewMidel);
        }

        [HttpGet]
        public IActionResult Search(string searchTerm) //Query the drinks table, display result to user basically
        {
            if (searchTerm == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.sTerm = searchTerm;
            var drinksListViewModel = new DrinkListViewModel
            { 
                Drinks = _drinkRepository.SearchDrinks(searchTerm) //Implemented in the DrinkRepository
            };
            return View(drinksListViewModel);
        }

        [HttpGet("/Drink/Details/{id:int}")]
        public async Task<IActionResult> Details(int id) // Check the IDrinkRepository Class for more information on the slides URL mechanism inner workings
        {
            IEnumerable<Comments> commentsList = await _commentsRepository.GetCommentsForDrink(id); //get Comments for the Drink
            var drink = _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound();
            }

            string[] imgUrls;
            string urls = drink.ImageSlideShowUrls;
            imgUrls = urls.Split(';'); //"Deserialize" the string from DB

            DrinkDetailsViewModel DdVM = new DrinkDetailsViewModel
            {
                Drink = drink,
                Comments = commentsList,
                ImgUrlsArr = imgUrls
            };

            return View(DdVM); //pass data to View
        }

    }
}
