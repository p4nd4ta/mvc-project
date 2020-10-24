using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; } //Primary Key
        public int OrderId { get; set; } //Foreign Key
        public int DrinkId { get; set; } //Foreign Key
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public virtual Drink Drink { get; set; } //
        public virtual Order Order { get; set; } // to make sure Entity Framework actually understands the relations here (Foreign Keys - OrderId and DrinkId from above (many-to-many relation: Orders<->OrderDetails<->Drinks))
    }
}
