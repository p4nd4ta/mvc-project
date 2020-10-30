using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> Drinks { get; } //get all Drinks
        IEnumerable<Drink> SearchDrinks(string searchTerm); //get all Drinks
        IEnumerable<Drink> PreferredDrinks { get; } //get only the Preffered Drinks, that are shown on the homepage
        Drink GetDrinkById (int drinkId); //return drink object based on it's id

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration
    }
}
