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
using Drinks_Self_Learn.ViewModels;
using Microsoft.EntityFrameworkCore.Internal;

namespace Drinks_Self_Learn.Controllers
{
    [Authorize (Roles = "Administrator")] // make sure only authorized users with the Administrator role can have access
    public class AdminController : Controller // this is nothing more than a Standard Scaffolded Controller with Views (Only the Views for it have been customized.)
    {
        private readonly AppDbContext _context;
        // here we don't demostrate the Repository Design Pattern, we are directly access the Db Context
        public AdminController(AppDbContext context)
        {
            _context = context; // inject the AppDbContext
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Drinks.Include(d => d.Category);
            // SELECT * FROM Drinks D JOIN Categories C ON D.CategoryId = C.CategoryId
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DrinkId == id);
            //SELECT TOP(1) * FROM Drinks D JOIN Categories C ON D.CategoryId = C.CategoryId WHERE D.drinkId = @PassedParameter
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinkViewModel dVM)
        {
            if (dVM.UrlsArr.Count != 2)
            {
                ViewBag.ErrorMessageUrls = "Check your slideshow URLs !";
                return View(dVM);
            }

            string urls = dVM.UrlsArr.Join(";");

            if (ModelState.IsValid)
            {
                Drink drink = new Drink
                {
                    DrinkId = dVM.DrinkId,
                    Name = dVM.Name,
                    ShortDescription = dVM.ShortDescription,
                    LongDescription = dVM.LongDescription,
                    Price = dVM.Price,
                    ImageThumbnailUrl = dVM.ImageThumbnailUrl,
                    IsPreferredDrink = dVM.IsPreferredDrink,
                    InStock = dVM.InStock,
                    CategoryId = dVM.CategoryId,
                    ImageSlideShowUrls = urls,

                };
                await _context.SaveChangesAsync();
                //INSERT INTO Drinks (...) VALUES(...)
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dVM.CategoryId);
            return View(dVM);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", drink.CategoryId);
            return View(drink);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrinkId,Name,ShortDescription,Price,ImageThumbnailUrl,IsPreferredDrink,InStock,CategoryId,LongDescription,ImageSlideShowUrls")] Drink drink)
        {
            if (id != drink.DrinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    // UPDATE Drinks SET ... ... WHERE DrinkId = @PassedParameter
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.DrinkId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", drink.CategoryId);
            return View(drink);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DrinkId == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
            // DELETE FROM Drinks WHERE DrinkId = @PassedParameter
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.DrinkId == id);
        }
    }
}
