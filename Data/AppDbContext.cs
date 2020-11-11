using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Identity;//
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;//
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.ViewModels;

namespace Drinks_Self_Learn.Data
{
    public class AppDbContext: IdentityDbContext<IdentityUser> // Inherit from this class to get the AspNetCore Identity features
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet <Drink> Drinks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
