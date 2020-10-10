using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Drink> PreferredDrinks { get; set; }
    }
}
