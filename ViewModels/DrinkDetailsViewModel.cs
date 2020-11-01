using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Drinks_Self_Learn.ViewModels
{
    public class DrinkDetailsViewModel
    {
        public Drink Drink { get; set; }

        public string[] ImgUrlsArr{ get; set; }

        public IEnumerable<Comments> Comments { get; set; }
        public List<IdentityUser> Users { get; set; }
        [Required]
        [MaxLength(200)]
        public string NewCommentText { get; set; }

    }
}
