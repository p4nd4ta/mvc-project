using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Drinks_Self_Learn.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminAccountsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly HttpContextAccessor _httpContextAccessor;
        public AdminAccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, HttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel vmodel)
        {
            if (vmodel.Id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(vmodel.Id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Email = vmodel.Email;
                user.UserName = vmodel.UserName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vmodel);
            }
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            var UserRequesting = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            ViewBag.DeleteIDCheck = UserRequesting.Id;

            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (UserRequesting.Id == user.Id)
            {
                return View("DeleteError");
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(EditUserViewModel vmodel)
        {

            if (vmodel.Id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(vmodel.Id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Email = vmodel.Email;
                user.UserName = vmodel.UserName;

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else return View(vmodel);
            }
        }
    }
}
