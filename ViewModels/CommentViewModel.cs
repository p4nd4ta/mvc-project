using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Drinks_Self_Learn.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public virtual Drink Drink { get; set; }
    }
}
