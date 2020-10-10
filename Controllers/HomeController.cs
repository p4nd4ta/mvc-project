using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Drinks_Self_Learn.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        public HomeController(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }
        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredDrinks = _drinkRepository.PreferredDrinks
            };

            return View(homeViewModel);
        }
    }
}
