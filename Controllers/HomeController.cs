using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
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
            _drinkRepository = drinkRepository; //Dependency injection
        }
        public ViewResult Index() //get the preffered drinks through the collection, defined in the IDrinkRepository, pass them to the homeViewModel, and view them in the View with Html.PartialAsync
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredDrinks = _drinkRepository.PreferredDrinks
            };

            return View(homeViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        // Error Handling in Produdction environment

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpError(int id)
        {
            if (id == 404)
            {
                return this.View("404"); //if there is 404 error, we return the 404 error view, else call the exception hander from above
            }
            return Error();

        }
    }
}
