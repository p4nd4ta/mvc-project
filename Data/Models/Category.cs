using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; } //Primary key
        public string CategoryName { get; set; }
        public string Descrition { get; set; }
        public List<Drink> Drinks { get; set; } // Make Entity Framework to understand the one-to-many relation between [dbo].[Categories].[categoryId] and [dbo].[Drinks].[categoryId]
    }
}
