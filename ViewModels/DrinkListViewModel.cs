using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.ViewModels
{
    public class DrinkListViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }
        public string CurrentCategory { get; set; }
    }
}
