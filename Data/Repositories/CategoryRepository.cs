using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; //injecting the DB context
        }

        public IEnumerable<Category> Categories => _appDbContext.Categories; //puts all categories from the database to a collection

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration
    }
}
