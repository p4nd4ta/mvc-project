using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.ViewModels;
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

        public DrinkController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository; //dependency injection
        }

        public ViewResult List(string category) //get the parameter
        {
            string _category = category;
            IEnumerable<Drink> drinks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category)) //if there is no parameter, just show all drinks
            {
                drinks = _drinkRepository.Drinks.OrderBy(n => n.DrinkId);
                currentCategory = "All drinks";
            }
            else // process the 2 categories
            {
                if (string.Equals("Alcoholic",_category,StringComparison.OrdinalIgnoreCase)) //get the drinks based on the category passed
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p => p.Name); //query the DB through the Repository to get the Alcoholic Drinks
                }
                else
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(p => p.Name); //query the DB through the Repository to get the Non-alcoholic Drinks
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
    }
}
