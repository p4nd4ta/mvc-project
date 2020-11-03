using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Drinks_Self_Learn.Data.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly AppDbContext _context;
        public CommentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comments> GetAllComments => _context.Comments.Include(d => d.Drink).OrderByDescending(c=>c.CommentDate).ToList();

        public IEnumerable<Comments> GetUnapprovedComments => _context.Comments.Where(c => c.Approved == false).Include(d => d.Drink).OrderByDescending(c=>c.CommentDate).ToList();

        public IEnumerable<Comments> GetApprovedComments => _context.Comments.Where(c => c.Approved == true).Include(d => d.Drink).OrderByDescending(c => c.CommentDate).ToList();
        public Comments GetCommentById(int id) => _context.Comments.Include(d=>d.Drink).FirstOrDefault(c => c.Id == id);

        public void ChangeApprovalState(Comments comment, bool aState)
        {
            comment.Approved = aState;
           _context.Comments.Update(comment);
           _context.SaveChanges();
        }

        public void DeleteComment(Comments comment)
        {
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public void EditComment(Comments EditedComment)
        {

            Comments comment = _context.Comments.FirstOrDefault(c => c.Id == EditedComment.Id);
            if (comment != null)
            {
                comment.CommentText = EditedComment.CommentText;
                _context.Comments.Update(comment);
                _context.SaveChanges();
            }
        }


        public async Task<Comments> WriteComment (Comments newComment)
        {
            //Comments Comment = new Comments
            //{
            //    Id = newComment.Id,
            //    Drink = newComment.Drink,
            //    IdentityUser = newComment.IdentityUser,
            //    CommentDate = newComment.CommentDate,
            //    CommentText = newComment.CommentText,
            //    Approved = true,
            //};
           await _context.Comments.AddAsync(newComment);
           await _context.SaveChangesAsync();
           return newComment;
        }
        [AllowAnonymous]
        public async Task<IEnumerable<Comments>> GetCommentsForDrink(int drinkId) => await _context.Comments.Where(c => c.Drink.DrinkId == drinkId).OrderByDescending(c => c.CommentDate).ToListAsync(); //gets only the approved comments
        //public async Task<IEnumerable<Comments>> GetCommentsForDrinkApproved(int drinkId) => await _context.Comments.Where(c => c.Drink.DrinkId == drinkId && c.Approved == true).ToListAsync(); //gets only the approved comments

    }
}
