using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly AppDbContext _appDbContext;
        public DrinkRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; //injecting the DB context
        }

        public IEnumerable<Drink> Drinks => _appDbContext.Drinks.Include(c => c.Category); // gets all drinks with its category

        public IEnumerable<Drink> PreferredDrinks => _appDbContext.Drinks.Where(p => p.IsPreferredDrink).Include(c => c.Category); //gets only the preffered drinks with the category

        public Drink GetDrinkById(int drinkId) => _appDbContext.Drinks.FirstOrDefault(p => p.DrinkId == drinkId); // get the drink based on its DrinkId property

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration

    }
}
