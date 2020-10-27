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
            var userRoles = await _userManager.GetRolesAsync(user);
            //var userClaims = await _userManager.GetClaimsAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles
            };

            var result = await _userManager.IsLockedOutAsync(user);
            var IsPermanent = await _userManager.GetLockoutEndDateAsync(user);

            if (result)
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

        public async Task<IActionResult> LockUser(string id)
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

            if (result)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
                await _userManager.UpdateSecurityStampAsync(user);
            }

            return RedirectToAction("EditUser", new { id = id });
        }


        public async Task<IActionResult> Details(string id)
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
            var userRoles = await _userManager.GetRolesAsync(user);
            //var userClaims = await _userManager.GetClaimsAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles
            };

            var result = await _userManager.IsLockedOutAsync(user);
            var IsPermanent = await _userManager.GetLockoutEndDateAsync(user);

            if (result)
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
        
        // Roles

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> EditRole(string id)
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
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
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
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
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

        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.roleId = id;
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleVM = new UserRoleViewModel
                { 
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }

                model.Add(userRoleVM);
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;
                if ( (model[i].IsSelected) && !(await _userManager.IsInRoleAsync(user, role.Name)) )
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if ( !(model[i].IsSelected) && (await _userManager.IsInRoleAsync(user, role.Name)) )
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = id });
                }
            }

            return RedirectToAction("EditRole", new { id = id });
        }

        public async Task<IActionResult> DeleteRole(string id)
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

        public async Task<IActionResult> EditRolesInUser(string id)
        {
            ViewBag.userId = id;
            var user = await _userManager.FindByIdAsync(id);
            var AllRoles = _roleManager.Roles;
            if (user == null)
            {
                return NotFound();
            }

            var model = new List<EditRoleViewModel>();
            foreach (var role in AllRoles)
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
                var role = await _roleManager.FindByIdAsync(model[i].Id);

                IdentityResult result = null;
                if ((model[i].IsSelected) && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].IsSelected) && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditUser", new { id = id });
                }
            }

            return RedirectToAction("EditUser", new { id = id });
        }
    }
}
