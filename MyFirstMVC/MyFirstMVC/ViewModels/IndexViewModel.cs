using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
}
