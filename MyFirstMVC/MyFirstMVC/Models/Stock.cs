using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Models
{
    public class Stock : Entity
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        public IEnumerable<PhoneOnStock> PhoneOnStocks { get; set; }
    }
}
