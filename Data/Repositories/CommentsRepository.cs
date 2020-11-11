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
        public CommentsRepository(AppDbContext context) //Inject the dbContext
        {
            _context = context;
        }

        public IEnumerable<Comments> GetAllComments => _context.Comments.Include(d => d.Drink).OrderByDescending(c=>c.CommentDate).ToList();
        //SELECT * FROM Comments C INNER JOIN Drinks D ON D.DrinkId = C.DrinkId ORDER BY C.CommentDate DESC;
        public IEnumerable<Comments> GetUnapprovedComments => _context.Comments.Where(c => c.Approved == false).Include(d => d.Drink).OrderByDescending(c=>c.CommentDate).ToList();
        //SELECT * FROM Comments C INNER JOIN Drinks D ON D.DrinkId = C.DrinkId WHERE C.Approved = 'false' ORDER BY C.CommentDate DESC;
        public IEnumerable<Comments> GetApprovedComments => _context.Comments.Where(c => c.Approved == true).Include(d => d.Drink).OrderByDescending(c => c.CommentDate).ToList();
        //SELECT * FROM Comments C INNER JOIN Drinks D ON D.DrinkId = C.DrinkId WHERE C.Approved = 'true' ORDER BY C.CommentDate DESC;
        public Comments GetCommentById(int id) => _context.Comments.Include(d=>d.Drink).FirstOrDefault(c => c.Id == id);
        //SELECT TOP(1) * FROM Comments C INNER JOIN Drinks D ON D.DrinkId = C.DrinkId WHERE C.Id = @Passed_param
        public void ChangeApprovalState(Comments comment, bool aState)
        {
            comment.Approved = aState;
           _context.Comments.Update(comment);
            //UPDATE Comments C SET Approved = @aState WHERE C.Id = @Passed_Param_Comment.Id
            _context.SaveChanges();
        }

        public void DeleteComment(Comments comment)
        {
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                //DELETE FROM Comments C WHERE C.Id = @Passed_Param_Comment.Id
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
                //UPDATE Comments C SET CommnetText = @Passed_Param_CommentText WHERE C.Id = @Passed_Param_Comment.Id
                _context.SaveChanges();
            }
        }


        public async Task<Comments> WriteComment (Comments newComment)
        {
           await _context.Comments.AddAsync(newComment);
           await _context.SaveChangesAsync();
           return newComment;
        }
        public async Task<IEnumerable<Comments>> GetCommentsForDrink(int drinkId) => await _context.Comments.Where(c => c.Drink.DrinkId == drinkId).OrderByDescending(c => c.CommentDate).ToListAsync();
        //SELECT * FROM Comments C INNER JOIN Drinks D ON D.DrinkId = C.DrinkId WHERE C.DrinkId = @Passed_Param ORDER BY C.CommentDate DESC;
    }
}
