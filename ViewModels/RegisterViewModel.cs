using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Drinks_Self_Learn.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        [Required]
        [Display(Name="User Name")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords Don't Match !")]
        public string ConfirmPassword { get; set; }
    }
}
