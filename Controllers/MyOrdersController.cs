using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drinks_Self_Learn.Controllers
{
    [Authorize]
    public class MyOrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public MyOrdersController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var order = _context.Orders
                        .Include(s => s.OrderLines)
                        .AsNoTracking();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User); //Get the current user
            return View(await _context.Orders.OrderByDescending(o => o.OrderPlaced).Where(u => u.IdentityUser.Id == currentUser.Id).ToListAsync()); // Get the orders for the current user
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) //check if the ID that is passed is null, and return 404 if it is null
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var order = await _context.Orders.Include(s => s.OrderLines).Include(s => s.IdentityUser)
                        .FirstOrDefaultAsync(m => m.OrderId == id); // based on the ID, search the db context and get the order with the same id

            if (order == null) // some more validation checks
            {
                return NotFound();
            }

            if (order.IdentityUser.Id != currentUser.Id) // check if the order belongs to the user, preventing seeing other user's oredrs
            {
                return NotFound();
            }

            var drinksList = new List<Drink>();
            IEnumerable<OrderDetail> detailsList; // create an enumerable structure objects to hold the drinks and the details for the order(Amount, etc...)

            foreach (OrderDetail od in order.OrderLines) // a very different approach here...
            {
                drinksList.Add(await _context.Drinks.FirstOrDefaultAsync(m => m.DrinkId == od.DrinkId)); //iterate one-by-one and add the drinks to the list from above
                // SELECT TOP(1) D.* FROM Orders O JOIN OrderDetails OD ON OD.OrderId = O.OrderId JOIN Drinks D ON D.DrinkId = OD.DrinkId WHERE O.OrderId = @PassedParameter AND D.DrinkId = @PassedParameter
            }

            detailsList = _context.OrderDetails.Where(p => p.OrderId.Equals(order.OrderId)); //same thing as above, but here we get it done with a single query, retrieve the order details for the order with the same id
            // SELECT OD.* FROM Orders O JOIN OrderDetails OD ON OD.OrderId = O.OrderId JOIN Drinks D ON D.DrinkId = OD.DrinkId WHERE O.OrderId = @PassedParameter

            ViewData["DrinksClient"] = drinksList; // and to pass them to the view we use ViewData, I haven't created a ViewModel, showing this Implementation, not saying it is correct though
            ViewData["DetailsClient"] = detailsList;

            return View(order);
        }

    }
}
