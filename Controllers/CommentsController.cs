using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Drinks_Self_Learn.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly UserManager<IdentityUser> _userManager;//Dependency Injections
        public CommentsController(UserManager<IdentityUser> userManager, ICommentsRepository commentsRepository, IDrinkRepository drinkRepository)
        {
            _userManager = userManager;
            _commentsRepository = commentsRepository;
            _drinkRepository = drinkRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(DrinkDetailsViewModel dtVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                Drink dr = _drinkRepository.GetDrinkById(dtVM.Drink.DrinkId);

                if (dr == null)
                {
                    return NotFound();
                }

                Comments Nc = new Comments
                {
                    CommentDate = DateTime.Now,
                    CommentText = dtVM.NewCommentText,
                    Drink = dr,
                    UserName = user.UserName,
                    IdentityUser = user,
                    Approved = true, // !Important! change that to 'false' to make the approval system function properly, left to 'true' for testing purposes only
                };

                await _commentsRepository.WriteComment(Nc);
                return RedirectToAction("Details", "Drink", new { id = dtVM.Drink.DrinkId });
            }
            //return RedirectToAction("List", "Drink");
            return RedirectToAction("Details", "Drink", new { id = dtVM.Drink.DrinkId });
        }

        // === Administration bellow ===

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
           IEnumerable<Comments>CommentsList=_commentsRepository.GetAllComments;
            return View(CommentsList); //From here to the end it is the Admin Panel Functionality Only! Here, I am just retrieving all comments via the Repository
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult ListApproved()
        {
            IEnumerable<Comments> CommentsList = _commentsRepository.GetApprovedComments;
            return View(CommentsList); //Retrieving all Approved comments via the Repository
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult ListUnapproved()
        {
            IEnumerable<Comments> CommentsList = _commentsRepository.GetUnapprovedComments;
            return View(CommentsList); //Retrieving all UnApproved comments via the Repository
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id) //Retrieve the Comment by id and pass it to CommentVM for edditing by Admin
        {

            var comment = _commentsRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel edVM = new CommentViewModel
            {
                Id = comment.Id,
                CommentDate = comment.CommentDate,
                CommentText = comment.CommentText,
                Drink = comment.Drink,
                UserName = comment.UserName
                
            };

            return View(edVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(CommentViewModel edVM) //Get the data from the CommentVM, validate it, and persist to DB, using EditComment method from the Repository
        {
            if (ModelState.IsValid)
            {
                var drink = _drinkRepository.GetDrinkById(edVM.Drink.DrinkId);
                Comments edited = new Comments
                {
                    Id = edVM.Id,
                    CommentDate = edVM.CommentDate,
                    CommentText = edVM.CommentText,
                    UserName = edVM.UserName,
                    Drink = drink,
                };

                _commentsRepository.EditComment(edited);
                return RedirectToAction("Index");
            }

            return View(edVM);
        }

        [HttpPost] //Interactive Edit, Used When a Drink Details page is viewed by Admin User
        [ValidateAntiForgeryToken] //DIFF: Redirects the user to the same page it has been previously
        [Authorize(Roles = "Administrator")]
        public IActionResult EditInt(CommentViewModel edVM)
        {
            if (ModelState.IsValid)
            {

                var drink = _drinkRepository.GetDrinkById(edVM.Drink.DrinkId);

                Comments edited = new Comments
                {
                    Id = edVM.Id,
                    CommentDate = edVM.CommentDate,
                    CommentText = edVM.CommentText,
                    UserName = edVM.UserName,
                    Drink = drink,
                };


                _commentsRepository.EditComment(edited);
                return RedirectToAction("Details", "Drink", new { id = edited.Drink.DrinkId });
            }

            return View(edVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id) // Delete Comment by ID
        {

            var comment = _commentsRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel edVm = new CommentViewModel
            {
                Id = comment.Id,
                CommentDate = comment.CommentDate,
                CommentText = comment.CommentText,
                UserName = comment.UserName,
                Drink = comment.Drink,
            };

            return View(edVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id) //Delete the Comment, using the method from the Repository
        {
            

            var comment = _commentsRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            _commentsRepository.DeleteComment(comment);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeStatus(int id) //Change comment status, using the method from the Repository
        {


            var comment = _commentsRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            if (comment.Approved == true)
            {
                _commentsRepository.ChangeApprovalState(comment,false);
            }
            else
            {
                _commentsRepository.ChangeApprovalState(comment, true);
            }

            return RedirectToAction("Index");
        }

        [HttpGet] //Interactive Status Change, Used When a Drink Details page is viewed by Admin User
        [Authorize(Roles = "Administrator")] //DIFF: Redirects the user to the same page it has been previously
        public IActionResult ChangeStatusInt(int id)
        {


            var comment = _commentsRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            if (comment.Approved == true)
            {
                _commentsRepository.ChangeApprovalState(comment, false);
            }
            else
            {
                _commentsRepository.ChangeApprovalState(comment, true);
            }

            return RedirectToAction("Details","Drink",new { id = comment.Drink.DrinkId});
        }


        [HttpPost] //Interactive Delete, Used When a Drink Details page is viewed by Admin User
        [ValidateAntiForgeryToken] //DIFF: Redirects the user to the same page it has been previously
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteInt(int id)
        {


            var comment = _commentsRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }
            var drId = comment.Drink.DrinkId;
            _commentsRepository.DeleteComment(comment);

            return RedirectToAction("Details", "Drink", new { id = drId});
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Details(int id) //Details View for a specific comment(by id)
        {
            var comment = _commentsRepository.GetCommentById(id);
            if (comment == null)
                return NotFound();

            CommentViewModel cmVM = new CommentViewModel
            {
                CommentDate = comment.CommentDate,
                CommentText = comment.CommentText,
                Drink = comment.Drink,
                Id = comment.Id,
                UserName = comment.UserName,
                IdentityUser = comment.IdentityUser
            };

            return View(cmVM);
        }
    }
}
