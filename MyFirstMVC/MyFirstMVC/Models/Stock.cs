using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<PhoneOnStock> Phones { get; set; }
    }

    public class PhoneOnStock
    {
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Quantity { get; set; }
    }
}
