using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Drinks_Self_Learn.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDrinkRepository _drinkRepository; //to get the data about the drinks
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IDrinkRepository drinkRepository, ShoppingCart shoppingCart)
        {
            _drinkRepository = drinkRepository; // Dependency injections
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index() //Passing the values to the viewmodel
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items; // using a separate "items" variable, just to make the code look more understandable, still can be assigned directly though

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartVM);
        }

        public RedirectToActionResult AddToShoppingCart(int drinkId) //get the selected drink(the one clicked on) and add it to cart
        {
            var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId); //get the drink object from DB, with the ID from the selected drink id above
            if (selectedDrink != null)
            {
                _shoppingCart.AddToCart(selectedDrink, 1); //adds 1x the selectedDrink
            }
            return RedirectToAction("Index"); //redirects to the shoppingcart index page
        }

        public RedirectToActionResult RemoveFromShoppingCart(int drinkId) // same as above, but removes the drink instead of adding it
        {
            var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
            if (selectedDrink != null)
            {
                _shoppingCart.RemoveFromCart(selectedDrink);
            }

            return RedirectToAction("Index");
        }
    }
}
