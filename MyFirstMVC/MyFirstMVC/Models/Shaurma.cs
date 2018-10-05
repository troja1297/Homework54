using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Models
{
    public enum ShaurmaSize
    {
        Small,
        Medium,
        Large,
        Xxl
    }

    public class Shaurma : Entity
    {
        public string Type { get; set; }

        public double Price { get; set; }

        public ShaurmaSize ShaurmaSize { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
