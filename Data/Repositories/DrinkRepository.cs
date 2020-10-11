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
            _appDbContext = appDbContext;
        }

        public IEnumerable<Drink> Drinks => _appDbContext.Drinks.Include(c => c.Category);

        public IEnumerable<Drink> PreferredDrinks => _appDbContext.Drinks.Where(p => p.IsPreferredDrink).Include(c => c.Category);

        public Drink GetDrinkById(int drinkId) => _appDbContext.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        
        ////

        //public async void AddDrink(Drink drink)
        //{
        //    _appDbContext.Add(drink);
        //    await _appDbContext.SaveChangesAsync();
        //}

        //public async void DelDrink(int id)
        //{
        //    var drink = await _appDbContext.Drinks.FindAsync(id);
        //    _appDbContext.Remove(drink);
        //    await _appDbContext.SaveChangesAsync();
        //}

        //public bool DrinkExists(int id)
        //{
        //    return _appDbContext.Drinks.Any(e => e.DrinkId == id);
        //}

    }
}
