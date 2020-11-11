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
    //This is nothing more than a Standard Scaffolded Controller with Views (Only the Views for it have been customized.) (also see SlidesValidation.js)
    //Also a Preview Action is added, showing the ViewModel in NewTab/Window before Saving to DB, for visual "Confirmation" from the user(also see SlidesValidation.js)
    //Check the IDrinkRepository Class for more information on the slides URL mechanism inner workings
    public class AdminController : Controller
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

            string urls = drink.ImageSlideShowUrls;
            List<string> imgUrls = urls.Split(';').ToList(); //Deserialize the Url Data

            DrinkViewModel dVM = new DrinkViewModel
            {
                DrinkId = drink.DrinkId,
                Name = drink.Name,
                ShortDescription = drink.ShortDescription,
                LongDescription = drink.LongDescription,
                Price = drink.Price,
                ImageThumbnailUrl = drink.ImageThumbnailUrl,
                IsPreferredDrink = drink.IsPreferredDrink,
                InStock = drink.InStock,
                CategoryId = drink.CategoryId,
                UrlsArr = imgUrls,
                Category = drink.Category,
            };

            return View(dVM);
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
            if (dVM.UrlsArr.Count != dVM.UrlCounter || dVM.UrlsArr.Count == 0) // Check if the array is held properly by the JS, and if it has any Urls
            {
                ViewBag.ErrorMessageUrls = "Check your slideshow URLs !";
                return View(dVM);
            }

            string urls = dVM.UrlsArr.Join(";"); //Serialize the Url Data with seperator ';' (URL reserved char)

            if (ModelState.IsValid)
            {
                Drink drink = new Drink
                {
                    Name = dVM.Name,
                    ShortDescription = dVM.ShortDescription,
                    LongDescription = dVM.LongDescription,
                    Price = dVM.Price,
                    ImageThumbnailUrl = dVM.ImageThumbnailUrl,
                    IsPreferredDrink = dVM.IsPreferredDrink,
                    InStock = dVM.InStock,
                    CategoryId = dVM.CategoryId,
                    ImageSlideShowUrls = urls,
                    Category=dVM.Category,
                };

                _context.Add(drink);
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


            string urls = drink.ImageSlideShowUrls;
            List<string> imgUrls = urls.Split(';').ToList(); // Deserialize the Urls

            DrinkViewModel dVM = new DrinkViewModel
            {
                DrinkId = drink.DrinkId,
                Name = drink.Name,
                ShortDescription = drink.ShortDescription,
                LongDescription = drink.LongDescription,
                Price = drink.Price,
                ImageThumbnailUrl = drink.ImageThumbnailUrl,
                IsPreferredDrink = drink.IsPreferredDrink,
                InStock = drink.InStock,
                CategoryId = drink.CategoryId,
                UrlsArr = imgUrls,
                Category = drink.Category,
            };

            return View(dVM);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,DrinkViewModel dVM)
        {
            if (id != dVM.DrinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                if (dVM.UrlsArr.Count != dVM.UrlCounter || dVM.UrlsArr.Count == 0)
                {
                    ViewBag.ErrorMessageUrls = "Check your slideshow URLs !";
                    return View(dVM);
                }

                string urls = dVM.UrlsArr.Join(";"); // Serialize the Urls Data

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
                    Category = dVM.Category,
                    ImageSlideShowUrls = urls,
                };


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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dVM.CategoryId);
            return View(dVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Preview(DrinkViewModel dVM) //Just a preview of the Changes you are about to make, a way of visual confirmation, the data is NOT persisted to DB here
        {
            if (dVM.UrlsArr.Count != dVM.UrlCounter || dVM.UrlsArr.Count == 0 )
            {
                ViewBag.ErrorMessageUrls = "Check your slideshow URLs !";
                return View(dVM);
            }
            //To undestand more on the front-end POST submit logic, please take a look at SlidesValidation.js, more specifically at the PreviewButton.click function
            //Check the IDrinkRepository Class for more information on the slides URL mechanism inner workings
            string urls = dVM.UrlsArr.Join(";"); //Serialize the URL Data

            if (ModelState.IsValid)
            {
                Drink drink = new Drink
                {
                    Name = dVM.Name,
                    ShortDescription = dVM.ShortDescription,
                    LongDescription = dVM.LongDescription,
                    Price = dVM.Price,
                    ImageThumbnailUrl = dVM.ImageThumbnailUrl,
                    IsPreferredDrink = dVM.IsPreferredDrink,
                    InStock = dVM.InStock,
                    CategoryId = dVM.CategoryId,
                    ImageSlideShowUrls = urls,
                    Category = dVM.Category,
                };

                DrinkDetailsViewModel ddVM = new DrinkDetailsViewModel
                {
                    Drink = drink,
                    ImgUrlsArr = urls.Split(';'), //Deserialize
                };
                
                return View(ddVM);
            }
            return RedirectToAction("Error","Home");
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
