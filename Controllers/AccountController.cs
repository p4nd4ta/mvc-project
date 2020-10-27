using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drinks_Self_Learn.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; //used for new user REGISTRATION
        private readonly SignInManager<IdentityUser> _signInManager; //used for user LOGIN
        // using the Built in AspNetCore Authorization and Identity

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager; // and injecting them
            _userManager = userManager;
        }

        public IActionResult Login(string returnURL)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnURL
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel); // if the model state is invalid, return the user to the same login view
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName); // assign to the user variable the user that is specified in the login form (by the entered username)

            if (user != null) // if the user, get from above doesn't exist - return an error
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, true); // here we check for the user/password combination from the login form
                if (result.Succeeded) // if they are correct, redirect the user to the url before, he/she were prompted to login, if it is empty, redirect to the homepage
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginViewModel.ReturnUrl);
                }

                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
            }
            ModelState.AddModelError("UserName", "Username/Password not found !"); // the error message for credentials missmatch or not existing user
            return View(loginViewModel);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = registerViewModel.UserName }; //here pass the values from the view model
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                var result2 = await _userManager.SetEmailAsync(user, registerViewModel.Email);

                if (result.Succeeded && result2.Succeeded) // check if the email and password were sucessfully written
                {
                    return RedirectToAction("Index", "Home");
                }
                else // pass an errorlist to the viewmodel with the AspNetCore Identity auto generated errors
                {
                    var errList = "";
                    var error = result.Errors.ToList();

                    foreach (var err in error)
                    {
                        errList += "<li>" + err.Description + "</li>";
                    }
                    ViewBag.ErrorMessages = errList;
                }
            }
            return View(registerViewModel);
        }


        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Logout() // sign out the user and redirect to homepage
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
