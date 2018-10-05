using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyFirstMVC.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
    }

    public class DetailsViewModel
    {
        public Phone Phone { get; set; }
        public Feedback Feedback { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
    }

    public class ShaurmaIndexViewModel
    {
        public IEnumerable<Shaurma> Shaurma { get; set; }
        public string Type { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public int? Size { get; set; }
        public int? CompanyId { get; set; }

        public SelectList Sizes { get; set; }
        public SelectList Companies { get; set; }
    }
}
