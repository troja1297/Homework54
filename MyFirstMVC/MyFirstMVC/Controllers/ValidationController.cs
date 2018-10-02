using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ApplicationDbContext context;

        public ValidationController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [AcceptVerbs("Get")]
        public IActionResult ValidateName(string name)
        {
            if (context.Phones.Any(p => p.Name == name))
            {
                return Json(false);
            }

            return Json(true);
        }

    }
}