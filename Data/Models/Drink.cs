using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class Drink
    {
        public int DrinkId { get; set; } //Primary Key
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsPreferredDrink { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; } //Foreign Key
        public virtual Category Category { get; set; } // to make sure Entity Framework does understand the one-to-many relation correctly ([dbo].[Categories].[categoryId] and [dbo].[Drinks].[categoryId])
        // this table(Drinks) also participates in the many-to-many relation: Orders<->OrderDetails<->Drinks
    }
}
