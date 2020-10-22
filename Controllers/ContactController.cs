using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Drinks_Self_Learn.Controllers
{
    public class ContactController : Controller
    {
        public ViewResult Index() // just returning the Contact page, nothing fancy here
        {
            return View();
        }
    }
}
