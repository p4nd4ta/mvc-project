using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Interfaces
{
    public interface ICommentsRepository
    {
        IEnumerable<Comments> GetAllComments { get; }
        IEnumerable<Comments> GetUnapprovedComments { get; }
        IEnumerable<Comments> GetApprovedComments { get; }
        Comments GetCommentById(int id);
        Task<Comments> WriteComment(Comments newComment);
        void DeleteComment(Comments comment);
        void EditComment(Comments EditedComment);
        void ChangeApprovalState(Comments comment, bool aState);
        Task<IEnumerable<Comments>> GetCommentsForDrink(int drinkId);

    }
}
