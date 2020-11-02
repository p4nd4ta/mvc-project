using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.ViewModels
{
    public class DrinkViewModel
    {
        //[Required]
        public int DrinkId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string LongDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageThumbnailUrl { get; set; }
        [Required]
        public List<string> UrlsArr{ get; set; }
        [Required]
        public bool IsPreferredDrink { get; set; }
        [Required]
        public bool InStock { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int UrlCounter { get;set; }
        public virtual Category Category { get; set; }

    }
}
