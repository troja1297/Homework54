using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyFirstMVC.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
 
        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
 
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
 
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
    
    public class PhoneIndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public string Name { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }
        public PageViewModel PageViewModel { get; set; }
        
        public SelectList Companies { get; set; }
        public SelectList Categories { get; set; }
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
