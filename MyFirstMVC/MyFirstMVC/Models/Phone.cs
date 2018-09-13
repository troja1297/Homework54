using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<PhoneOnStock> Stocks { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
