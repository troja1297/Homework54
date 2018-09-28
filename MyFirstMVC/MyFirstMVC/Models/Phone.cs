using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstMVC.Models
{
    public class Phone
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "укажи название телефона")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "длина названия от 1 до 50")]
        [Remote(action: "ValidateName", controller: "Validation", ErrorMessage = "не ругайся")]
        public string Name { get; set; }
        
        [Required]
        [Range(1.0, 1600.0)]
        public double Price { get; set; }

        //[Range(typeof(DateTime), "1900-01-01 00:00:00.228", "2100-01-01 00:00:00.228")]
        public DateTime AssemblyDate { get; set; }

        public Company Company { get; set; }
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<PhoneOnStock> PhoneOnStocks { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
