using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; } // So we should know to which shopping cart the items belong
        public decimal ShoppingCartTotal { get; set; } // The total price of the items inside
    }
}
