using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstMVC.Controllers
{
    public class ValidationController : Controller
    {
        private readonly List<string> BadWords = new List<string>()
        {
            "fuck",
            "dick",
            "bitch"
        };

        [AcceptVerbs("Get")]
        public IActionResult ValidateName(string name)
        {
            if(BadWords.Any(b => name.Contains(b, StringComparison.OrdinalIgnoreCase)))
                return Json(false);
            
            return Json(true);
        }

    }
}