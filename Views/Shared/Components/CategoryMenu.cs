using Drinks_Self_Learn.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Views.Shared.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Categories.OrderBy(p => p.CategoryName);
            
            return View(categories);
        }
    }
}
