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

namespace Drinks_Self_Learn.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly AppDbContext _context;

        public AdminOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrdersAdmin
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var order = _context.Orders
                        .Include(s => s.OrderLines)
                        .AsNoTracking();

            return View(await _context.Orders.ToListAsync());
        }

        // GET: OrdersAdmin/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {

            var order = await _context.Orders.Include(s => s.OrderLines)
                        .FirstOrDefaultAsync(m => m.OrderId == id);

            var drinksList = new List<Drink>();
            var detailsList = new List<OrderDetail>();

            foreach (OrderDetail od in order.OrderLines)
            {
                drinksList.Add(await _context.Drinks.FirstOrDefaultAsync(m => m.DrinkId == od.DrinkId));
                detailsList.Add(await _context.OrderDetails.FirstOrDefaultAsync(m => m.DrinkId== od.Drink.DrinkId));
            }

            ViewData["Drinks"] = drinksList;
            ViewData["Details"] = detailsList;

            if (id == null)
            {
                return NotFound();
            }

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrdersAdmin/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrdersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,FirstName,LastName,AddressLine1,AddressLine2,ZipCode,State,Country,PhoneNumber,Email")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: OrdersAdmin/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
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
            return View(order);
        }
        // POST: OrdersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,FirstName,LastName,AddressLine1,AddressLine2,ZipCode,State,Country,PhoneNumber,Email")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [Authorize(Roles = "Administrator")]
        // GET: OrdersAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrdersAdmin/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
