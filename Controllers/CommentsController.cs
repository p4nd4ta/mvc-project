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
        private readonly UserManager<IdentityUser> _userManager;
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
                    UserName=user.UserName,
                    IdentityUser = user,
                    Approved=true,
                };

               await _commentsRepository.WriteComment(Nc);
                return RedirectToAction("Details", "Drink", new { id = dtVM.Drink.DrinkId });
            }
            return RedirectToAction("List", "Drink");//, new { id = dtVM.Drink.DrinkId });
        }

        // Administration bellow

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
           IEnumerable<Comments>CommentsList=_commentsRepository.GetAllComments;
            return View(CommentsList);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
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
                Drink = comment.Drink,
                IdentityUser = comment.IdentityUser
            };

            return View(comment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(CommentViewModel edVM)
        {
            if (ModelState.IsValid)
            {

                Comments edited = new Comments
                {
                    Id = edVM.Id,
                    CommentDate = edVM.CommentDate,
                    CommentText = edVM.CommentText,
                    Drink = edVM.Drink,
                    IdentityUser = edVM.IdentityUser
                };

                _commentsRepository.EditComment(edited);
                return RedirectToAction("Index");
            }

            return View(edVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
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
                Drink = comment.Drink,
                IdentityUser = comment.IdentityUser
            };

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
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
        public IActionResult ChangeStatus(int id)
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

        [HttpGet]
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

    }
}
