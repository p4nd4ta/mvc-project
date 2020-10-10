//using AspNetCore;
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

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }
    }
}
