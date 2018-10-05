using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstMVC.Models;
using MyFirstMVC.Services;

namespace MyFirstMVC.Controllers
{
    public enum Status
    {
        Active,
        NotActive
    }

    public class Foo
    {
        public int Status { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly TransientObject transientObject1;
        private readonly TransientObject transientObject2;
        private readonly ScopedObject scopedObject1;
        private readonly ScopedObject scopedObject2;
        private readonly SingletonObject singletonObject1;
        private readonly SingletonObject singletonObject2;

        public HomeController(TransientObject transientObject1, TransientObject transientObject2, ScopedObject scopedObject1, ScopedObject scopedObject2, SingletonObject singletonObject1, SingletonObject singletonObject2)
        {
            this.transientObject1 = transientObject1;
            this.transientObject2 = transientObject2;
            this.scopedObject1 = scopedObject1;
            this.scopedObject2 = scopedObject2;
            this.singletonObject1 = singletonObject1;
            this.singletonObject2 = singletonObject2;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            ViewBag.Message = "Your application description page.";

            ViewBag.Int = 0;
            Foo foo = new Foo()
            {
                Status = 0
            };
            return View(foo);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            ViewBag.Transient1 = transientObject1.Guid;
            ViewBag.Scoped1 = scopedObject1.Guid;
            ViewBag.Singleton1 = singletonObject1.Guid;
            ViewBag.Transient2 = transientObject2.Guid;
            ViewBag.Scoped2 = scopedObject2.Guid;
            ViewBag.Singleton2 = singletonObject2.Guid;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
