using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class ShoppingCartItem // holds the properties of each item in the shopping cart
    {
        public int ShoppingCartItemId { get; set; } //Primary Key
        public Drink Drink { get; set; } //Foreign Key (one-to-many relation)
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; } //Guid from the session, set in ShoppingCart.cs, this is NOT the primary key


    }
}
