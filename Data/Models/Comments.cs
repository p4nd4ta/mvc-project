using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength (200)]
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string UserName { get; set; }
        public bool Approved { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public virtual Drink Drink { get; set; }
    }
}
