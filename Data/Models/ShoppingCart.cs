using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; //injecting the DB context
        }
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session; //get access to the sessions

            var context = service.GetRequiredService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString(); //check with null-coalescing operator the session(Cookie) if it has CartId already(Is NOT NULL), if it IS NULL, generates a new ID

            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Drink drink, int amount) //Add item to cart
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null) // if we dont have this specific drink in the cart we add it with amount of 1
            {
                shoppingCartItem = new ShoppingCartItem //passing the properties of to the new object
                {
                    ShoppingCartId = ShoppingCartId,
                    Drink = drink,
                    Amount = 1
                };
                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else //else we increase the amount of the specific drink with 1
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Drink drink)
        {
            var shoppingCartItem =
                _appDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1) // if we have lets say 3x same drink, decreases ammount with 1
                {
                    shoppingCartItem.Amount --;
                    localAmount = shoppingCartItem.Amount;
                }
                else // else removes it entirely
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _appDbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems() //returns all items in the shopping cart
        {
            return ShoppingCartItems ?? // A null-coalescing operator, if ShoppingCartItems IS NOT null ,it will return it, if it IS null, will execute the "right hand", returning the items that are in the shoppingCart (based on the shopping cart ID)
                (ShoppingCartItems =
                    _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                        .Include(s => s.Drink)
                        .ToList());
        }

        public void ClearCart() //removes all items from the shopping cart(called after successful checkout)
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal() // return the total SUM of price of all the items currently in the cart
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Drink.Price * c.Amount).Sum();
            return total;
        }
    }
}
