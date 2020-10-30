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

        public IEnumerable<Drink> Drinks => _appDbContext.Drinks.Include(c => c.Category); // gets all drinks with it's category
        // SELECT * FROM Drinks D JOIN Categories C ON D.CategoryId = C.CategoryId
        public IEnumerable<Drink> PreferredDrinks => _appDbContext.Drinks.Where(p => p.IsPreferredDrink).Include(c => c.Category); //gets only the preffered drinks with the category
        // SELECT * FROM Drinks D JOIN Categories C ON D.CategoryId = C.CategoryId WHERE D.IsPreferredDrink = 'true'
        public Drink GetDrinkById(int drinkId) => _appDbContext.Drinks.FirstOrDefault(p => p.DrinkId == drinkId); // get the drink based on it's DrinkId property
        // SELECT TOP(1) * FROM Drinks WHERE DrinkId = @PassedParameter

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration

        public IEnumerable<Drink> SearchDrinks(string searchTerm) => _appDbContext.Drinks.Where(d => d.Name.Contains(searchTerm)|| d.ShortDescription.Contains(searchTerm) || d.Category.CategoryName.Equals(searchTerm));

    }
}
