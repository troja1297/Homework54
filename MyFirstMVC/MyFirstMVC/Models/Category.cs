using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public IEnumerable<Category> SubCategories { get; set; }

        public IEnumerable<Phone> Phones { get; set; }
    }
}
