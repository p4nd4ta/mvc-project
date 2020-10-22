using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; } //gets all categories

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration
    }
}
