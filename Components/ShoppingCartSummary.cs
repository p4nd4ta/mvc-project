using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Components
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        /*
         * This ViewComponent is used to get the shopping cart indicator in the navigation bar with the products count
         * The view is under Views/Components/ShoppingCartSummary/Default.cshtml
         */

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart; //injecting shoppingCart to Constructor
        }

        public IViewComponentResult Invoke() //automatically used every time the component is invoked
        {
            var items = _shoppingCart.GetShoppingCartItems(); //get all items in the shopping cart, put them in local var because otherwise cannot be implicitly converted
            _shoppingCart.ShoppingCartItems = items; //assign them to the ShoppingCartItems Property

            var shoppingCartViewModel = new ShoppingCartViewModel
            { //passing the data to the viewmodel, will use this viewmodel to pass the data to Default.cstml and display the items count with "Model.ShoppingCart.ShoppingCartItems.Count"
                ShoppingCart = _shoppingCart, //pass the same object from above
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal() // get the total sum of money and assign it
            };

            return View(shoppingCartViewModel);
        }
    }
}
