using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drinks_Self_Learn.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository; // inject the shopping cart model and Orders Repo Interface
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]

        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items; //gets the current shopping cart items and passes them

            if (_shoppingCart.ShoppingCartItems.Count == 0) //validity checks
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                order.OrderTotal = _shoppingCart.GetShoppingCartTotal(); //get the total price of the order and assign it to the order object
                _orderRepository.CreateOrder(order); //create the order using the interface
                _shoppingCart.ClearCart(); //delete the cart items
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete() // return a view to the the user with completion message
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your purchase !";
            return View();
        }
    }
}
