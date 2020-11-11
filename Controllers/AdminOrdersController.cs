using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drinks_Self_Learn.Data;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Drinks_Self_Learn.Migrations;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace Drinks_Self_Learn.Controllers
{
    [Authorize(Roles = "Administrator")] // make sure only authorized users with the Administrator role can have access
    public class AdminOrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        // here we don't demostrate the Repository Design Pattern, we are directly access the Db Context

        public AdminOrdersController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context; // inject the AppDbContext
            _userManager = userManager;
        }

        // GET: OrdersAdmin
        public async Task<IActionResult> Index()
        {
            var order = _context.Orders
                        .Include(s => s.OrderLines) //include the OrderLines List since it holds the "order details" data for the order
                        .AsNoTracking();

            return View(await _context.Orders.OrderByDescending(o => o.OrderPlaced).ToListAsync()); // and return to the "Index" View all items, sorted descending by the order date
        }

        public async Task<IActionResult> IndexUser(string id)
        {
            var order = _context.Orders
                        .Include(s => s.OrderLines)
                        .AsNoTracking();
            ViewBag.CUID = await _userManager.FindByIdAsync(id);
            return View(await _context.Orders.OrderByDescending(o => o.OrderPlaced).Where(u => u.IdentityUser.Id == id).ToListAsync());
        }

        // GET: OrdersAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) //check if the ID that is passed is null, and return 404 if it is null
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(s => s.OrderLines).Include(s=> s.IdentityUser)
                        .FirstOrDefaultAsync(m => m.OrderId == id); // based on the ID, search the db context and get the order with the same id

            if (order == null) // some more validation checks
            {
                return NotFound();
            }

            var drinksList = new List<Drink>();
            IEnumerable<OrderDetail> detailsList; // create an enumerable structure objects to hold the drinks and the details for the order(Amount, etc...)

            foreach (OrderDetail od in order.OrderLines) // a very different approach here... I am not creating a seperate ViewModel, so i am using the ViewData to pass data to View, I know it is not a good practice, just demonstrating
            {
                drinksList.Add(await _context.Drinks.FirstOrDefaultAsync(m => m.DrinkId == od.DrinkId)); //iterate one-by-one and add the drinks to the list from above
                // SELECT TOP(1) D.* FROM Orders O JOIN OrderDetails OD ON OD.OrderId = O.OrderId JOIN Drinks D ON D.DrinkId = OD.DrinkId WHERE O.OrderId = @PassedParameter AND D.DrinkId = @PassedParameter
            }

            detailsList = _context.OrderDetails.Where(p => p.OrderId.Equals(order.OrderId)); //same thing as above, but here we get it done with a single query, retrieve the order details for the order with the same id
            // SELECT OD.* FROM Orders O JOIN OrderDetails OD ON OD.OrderId = O.OrderId JOIN Drinks D ON D.DrinkId = OD.DrinkId WHERE O.OrderId = @PassedParameter

            ViewData["Drinks"] = drinksList; // and to pass them to the view we use ViewData, I haven't created a ViewModel, showing this Implementation, not saying it is correct though
            ViewData["Details"] = detailsList;

            return View(order);
        }

        // GET: OrdersAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id) // code base same as Details action, only the action bellow for the delete itself is different
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(s => s.OrderLines)
                       .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var drinksList = new List<Drink>();
            IEnumerable<OrderDetail> detailsList;

            foreach (OrderDetail od in order.OrderLines)
            {
                drinksList.Add(await _context.Drinks.FirstOrDefaultAsync(m => m.DrinkId == od.DrinkId));
                // SELECT TOP(1) D.* FROM Orders O JOIN OrderDetails OD ON OD.OrderId = O.OrderId JOIN Drinks D ON D.DrinkId = OD.DrinkId WHERE O.OrderId = @PassedParameter AND D.DrinkId = @PassedParameter
            }

            detailsList = _context.OrderDetails.Where(p => p.OrderId.Equals(order.OrderId));
            // SELECT OD.* FROM Orders O JOIN OrderDetails OD ON OD.OrderId = O.OrderId JOIN Drinks D ON D.DrinkId = OD.DrinkId WHERE O.OrderId = @PassedParameter

            ViewData["Drinks"] = drinksList;
            ViewData["Details"] = detailsList;

            return View(order);
        }

        // POST: OrdersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) // delete the selected item by id
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order); //remove the order from the DB
            // DELETE FROM Orders WHERE OrderId = @PassedParameter
            await _context.SaveChangesAsync(); //save the changes
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        public async Task<IActionResult> Mark(int? id) // Mark the order as complete or not(basically changing a bool switch in the record for the order)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderProcessed == false ) //implement the "logic", also the OrderProcessed property is used in the Index View for visual feedback And the MyOrders, so the client can check the status of the order
            {
                order.OrderProcessed = true;
            }
            else
            {
                order.OrderProcessed = false;
            }
            
            _context.Update(order);
            //UPDATE Orders SET OrderProcessed = @'true' / @'false' WHERE OrderId = @PassedParameter
            await _context.SaveChangesAsync();

            return RedirectToAction("Details","AdminOrders", new { id = id });
            // redirect the user to the Details action for the order that was "Marked"(the same id), triggering a page refresh, so the view can be updated
        }
    }
}
