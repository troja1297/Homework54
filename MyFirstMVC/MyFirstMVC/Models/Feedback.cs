using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Models
{
    public class Feedback : Entity
    {
        public string Author { get; set; }

        public string Message { get; set; }

        public int Rating { get; set; }

        public DateTime Date { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}
