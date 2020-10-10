using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Descrition { get; set; }
        public List<Drink> Drinks { get; set; }
        //public string Description { get; internal set; }
    }
}
