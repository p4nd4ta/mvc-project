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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Drinks_Self_Learn.Controllers
{
    [Authorize(Roles = "Administrator")] // make sure only authorized users with the Administrator role can have access
    public class AdminAccountsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly HttpContextAccessor _httpContextAccessor; //Dependency Injection, playing around with the HttpContextAccessor
        public AdminAccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, HttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // === Users Management ===
        public IActionResult Index()
        {
            var users = _userManager.Users; //get list of all users
            return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id); // Find User by Id

            if (user == null)
            {
                return NotFound();
            }
            var userRoles = await _userManager.GetRolesAsync(user); //get User's Roles
            var model = new EditUserViewModel //Assign them to the ViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles
            };

            var result = await _userManager.IsLockedOutAsync(user); // check if the user account is locked
            var IsPermanent = await _userManager.GetLockoutEndDateAsync(user); // get the last lockout end date from the DB

            if (result) // Used for Better visuals and logic in the View
            {
                ViewBag.LockOutStatusMessage = "Locked";
            }
            if(IsPermanent == DateTime.MaxValue)
            {
                ViewBag.LockOutStatusMessage = "Permanently Locked";
            }
            else
            {
                ViewBag.LockOutStatusMessage = "UnLocked";
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel vmodel)
        {
            // standard checks from the view model, get the email,username

            if (vmodel.Id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(vmodel.Id); //Check if user exists in DB

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Email = vmodel.Email;
                user.UserName = vmodel.UserName;

                var result = await _userManager.UpdateAsync(user); //Persist to DB

                if (result.Succeeded) //If saved Sucessfuly, redirect to Index
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors) //If not, pass and display the errors
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vmodel);
            }
        }

        public async Task<IActionResult> LockUser(string id) // Unlock User account if it is, locked by too many Auth Failures, OR Perma BAN the User
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

            var result = await _userManager.IsLockedOutAsync(user);

            if (result) // here we implement the permanent ban logic, set the Account Lockout Time to year 9999(DateTime.MaxValue), Or unban him if he is already banned, by setting the end date to DateTime.Now
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now); //Unban User
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue); //Perma Ban User
                await _userManager.UpdateSecurityStampAsync(user); //the Concurency Stamp is changed, so he's/her's session gets invalidated immediately on the next request
            }

            return RedirectToAction("EditUser", new { id = id });
        }


        public async Task<IActionResult> Details(string id) //Details For the User
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
            var userRoles = await _userManager.GetRolesAsync(user); // Get the roles for the user
            EditUserViewModel model = new EditUserViewModel // Create the VM
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles
            };

            var result = await _userManager.IsLockedOutAsync(user);
            var IsPermanent = await _userManager.GetLockoutEndDateAsync(user);

            if (result) // Used for Better visuals and logic in the View
            {
                ViewBag.LockOutStatusMessage = "Locked";
            }
            if (IsPermanent == DateTime.MaxValue)
            {
                ViewBag.LockOutStatusMessage = "Permanently Locked";
            }
            else
            {
                ViewBag.LockOutStatusMessage = "UnLocked";
            }


            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var UserRequesting = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name); // get the current user id
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

            if (UserRequesting.Id == user.Id) // and prevent from deleting itself
            {
                return View("DeleteError");
            }

            EditUserViewModel model = new EditUserViewModel //Create the ViewModel and pass it to the delete view
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(EditUserViewModel vmodel) // Just deleting the user from the POST request
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
        
        // === Roles Management ===

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles; //get list of all roles
            return View(roles);
        }

        public async Task<IActionResult> EditRole(string id) //get the name and users that participate in the role
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model) //set the name of role, "EditUsersInRole" action handles the assigment/revoking
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
                return View(model);
            }

        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model) //Create a new IdentityRole via the RoleManager we have injected
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
               IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditUsersInRole(string id) //From the Roles View, get all users in the DB and set them explicitly, if they participate or not in that role
        {
            ViewBag.roleId = id;
            var role = await _roleManager.FindByIdAsync(id); //get the role by roleId

            if (role == null)
            {
                return NotFound();
            }

            var model = new List<UserRoleViewModel>(); //create a list with userid, username, and is selected that will be used in the checkbox and logic for saving to DB

            foreach (var user in _userManager.Users)
            {
                var userRoleVM = new UserRoleViewModel
                { 
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name)) // check if the user is in this role and set the IsSelected accordingly
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }

                model.Add(userRoleVM); //add to the list of UserRoleViewModel from above
            }
            
            return View(model); // pass it to the view
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id) //From the Roles View, get all users in the DB and set them explicitly, if they participate or not in that role
        {
            var role = await _roleManager.FindByIdAsync(id); // get the role by roleId

            if (role == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++) // Iterate through the list of UserRolesVM to see which user is checked or not and do the respective action(add/remove from the role)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;
                if ( (model[i].IsSelected) && !(await _userManager.IsInRoleAsync(user, role.Name)) ) // check if the user is already in that role, and do the appropriate action
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if ( !(model[i].IsSelected) && (await _userManager.IsInRoleAsync(user, role.Name)) ) // check if the user is already in that role, and do the appropriate action
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue; // skip that model[i]
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1)) // if there are still unprocessed users in the list, go to the next one
                        continue;
                    else // else redirect to the editRole action, to see the new changes
                        return RedirectToAction("EditRole", new { id = id });
                }
            }

            return RedirectToAction("EditRole", new { id = id });
        }

        public async Task<IActionResult> DeleteRole(string id) // Get the Role by id and delete it
        {

            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles");
            }
           
        }

        public async Task<IActionResult> EditRolesInUser(string id) // in the View for the UserEdit, Edit the roles that this USER participates in (Logic is inverted from EditUsersInRole)
        {
            ViewBag.userId = id;
            var user = await _userManager.FindByIdAsync(id); // get the user by id from DB
            var AllRoles = _roleManager.Roles; // get list of all Roles
            if (user == null)
            {
                return NotFound();
            }

            var model = new List<EditRoleViewModel>();
            foreach (var role in AllRoles) // Itterate through the list of roles and determine if the current user IsSelected or not(is in the role or not) and set the property (IsSelected) respectively
            {
                var CRole = await _roleManager.FindByIdAsync(role.Id);
                var editRoleVM = new EditRoleViewModel
                {
                    Id = CRole.Id,
                    RoleName = CRole.Name
                };

                if (await _userManager.IsInRoleAsync(user, CRole.Name))
                {
                    editRoleVM.IsSelected = true;
                }
                else
                {
                    editRoleVM.IsSelected = false;
                }

                model.Add(editRoleVM);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRolesInUser(List<EditRoleViewModel> model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var role = await _roleManager.FindByIdAsync(model[i].Id); // Iterate through the list of EditRoleVM to see which role is checked or not and do the respective action(add/remove the user from the role)

                IdentityResult result = null;
                if ((model[i].IsSelected) && !(await _userManager.IsInRoleAsync(user, role.Name))) // check if the user is already in that role, and do the appropriate action
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].IsSelected) && (await _userManager.IsInRoleAsync(user, role.Name))) // check if the user is already in that role, and do the appropriate action
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue; // skip that model[i]
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1)) // if there are still unprocessed users in the list, go to the next one
                        continue;
                    else // else redirect to the editRole action, to see the new changes
                        return RedirectToAction("EditUser", new { id = id });
                }
            }

            return RedirectToAction("EditUser", new { id = id });
        }
    }
}
