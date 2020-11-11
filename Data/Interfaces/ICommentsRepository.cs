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
    public interface ICommentsRepository //Interface for the Comments Repository
    {
        IEnumerable<Comments> GetAllComments { get; } //get list of all comments
        IEnumerable<Comments> GetUnapprovedComments { get; } //get list of all comments based on their approval status
        IEnumerable<Comments> GetApprovedComments { get; } //get list of all comments based on their approval status
        Comments GetCommentById(int id); // return single Commnets object by id
        Task<Comments> WriteComment(Comments newComment); // Create new Comments object and persist it to DB
        void DeleteComment(Comments comment); // Delete Comments object and persist it to DB
        void EditComment(Comments EditedComment); // Edit new Comments object and update in in the DB
        void ChangeApprovalState(Comments comment, bool aState); // Change approval state of comment and update it in the DB
        Task<IEnumerable<Comments>> GetCommentsForDrink(int drinkId); // get list of comments based on the DrinkId they are for

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration

    }
}
