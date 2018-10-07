using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstMVC.Models;
using MyFirstMVC.Services;
using MyFirstMVC.ViewModels;

namespace MyFirstMVC.Controllers
{
    public enum SortPhones
    {
        NameAsc,
        NameDesc,
        CompanyAsc,
        CompanyDesc,
        CategotyAsc,
        CategoryDesc,
        PriceAsc,
        PriceDesc
    }

    public class PhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PhoneValidator _phoneValidator;
        private readonly FeedbackValidator _feedbackValidator;

        public PhonesController(ApplicationDbContext context,
            PhoneValidator phoneValidator,
            FeedbackValidator feedbackValidator)
        {
            _context = context;
            _phoneValidator = phoneValidator;
            _feedbackValidator = feedbackValidator;
        }

         // GET: Shaurmas
        public async Task<IActionResult> Index(
            string name,
            double? priceFrom,
            double? priceTo,
            int? categoryId,
            int? companyId,
            int page = 1,
            SortPhones sortPhones = SortPhones.CompanyDesc)
        {

            IQueryable<Phone> phones = _context.Phones.Include(p => p.Category);

            if (!string.IsNullOrWhiteSpace(name))
            {
                phones = phones.Where(s => s.Name.Contains(name));
            }

            if (priceFrom.HasValue)
            {
                phones = phones.Where(s => s.Price >= priceFrom.Value);
            }

            if (priceTo.HasValue)
            {
                phones = phones.Where(s => s.Price <= priceTo.Value);
            }

            if (companyId.HasValue)
            {
                phones = phones.Where(s => s.CompanyId == companyId.Value);
            }
            
            if (categoryId.HasValue)
            {
                phones = phones.Where(s => s.CategoryId == categoryId.Value);
            }

            ViewBag.NameSort = sortPhones == SortPhones.NameAsc ? SortPhones.NameDesc : SortPhones.NameAsc;
            ViewBag.CompanySort = sortPhones == SortPhones.CompanyAsc ? SortPhones.CompanyDesc : SortPhones.CompanyAsc;
            ViewBag.CategorySort = sortPhones == SortPhones.CategotyAsc ? SortPhones.CategoryDesc : SortPhones.CategotyAsc;
            ViewBag.PriceSort = sortPhones == SortPhones.PriceAsc ? SortPhones.PriceDesc : SortPhones.PriceAsc;

            switch (sortPhones)
            {
                case SortPhones.NameAsc:
                    phones = phones.OrderBy(s => s.Name);
                    break;
                case SortPhones.NameDesc:
                    phones = phones.OrderByDescending(s => s.Name);
                    break;
                case SortPhones.PriceAsc:
                    phones = phones.OrderBy(s => s.Price);
                    break;
                case SortPhones.PriceDesc:
                    phones = phones.OrderByDescending(s => s.Price);
                    break;
                case SortPhones.CategotyAsc:
                    phones = phones.OrderBy(s => s.Category.Name);
                    break;
                case SortPhones.CategoryDesc:
                    phones = phones.OrderByDescending(s => s.Category.Name);
                    break;
                case SortPhones.CompanyAsc:
                    phones = phones.OrderBy(s => s.Company.Name);
                    break;
                case SortPhones.CompanyDesc:
                    phones = phones.OrderByDescending(s => s.Company.Name);
                    break;
            }
            int pageSize = 4;
            var count = await phones.CountAsync();
 
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            
            var items = await phones.Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
            PhoneIndexViewModel model = new PhoneIndexViewModel()
            {
                Phones = items,
                Name = name,
                PageViewModel = pageViewModel,
                CompanyId = companyId,
                CategoryId = categoryId,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                Categories = new SelectList(_context.Categories, "Id", "Name"),
                Companies = new SelectList(_context.Companies, "Id", "Name")
            };
            
            

            return View(model);
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Category)
                .Include(p => p.Feedbacks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            ViewBag.RateList = new SelectList(new[] { 1, 2, 3, 4, 5 });
            ViewBag.PhoneId = phone.Id;

            DetailsViewModel model = new DetailsViewModel()
            {
                Phone = phone,
                Feedback = new Feedback(),
                Feedbacks = _context.Feedbacks.Where(f => f.PhoneId == phone.Id)
            };
            return View(model);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Companies"] = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Company,Price,CategoryId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Companies"] = new SelectList(_context.Companies, "Id", "Name");
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Phone phone = await _context.Phones.FindAsync(id);

            if (phone == null)
            {
                return NotFound();
            }
            

            return View(GetEditModel(phone));
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            List<ErrorMessage> errors = _phoneValidator.Validate(phone);

            CheckErrors(errors);

            if (!ModelState.IsValid)
            {
                return View(GetEditModel(phone));
            }
            try
            {
                phone = _context.Phones.FirstOrDefault(c => c.Id == id);
                phone.Name = "Edited name";

                _context.Update(phone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(phone.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
            
        }

        private EditPhoneViewModel GetEditModel(Phone phone)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", phone.Id);
            ViewData["Companies"] = new SelectList(_context.Companies, "Id", "Name", phone.Id);
            return EditPhoneViewModel.Cast(phone);
        }


        // GET: Phones/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }


        // POST: Phones/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult CreateReview(Feedback feedback)
        {
            var errors = _feedbackValidator.Validate(feedback);

            CheckErrors(errors);

            if (!ModelState.IsValid)
            {
                int phoneId = feedback.PhoneId;
                ViewBag.RateList = new SelectList(new[] { 1, 2, 3, 4, 5 });
                ViewBag.PhoneId = phoneId;
                return View("Details", new DetailsViewModel()
                {
                    Phone = _context.Phones.Find(phoneId),
                    Feedback = new Feedback(),
                    Feedbacks = _context.Feedbacks.Where(f => f.PhoneId == phoneId)
                });
            }

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("Details", "Phones", new {id = feedback.PhoneId});
        }

        private void CheckErrors(List<ErrorMessage> errors)
        {
            foreach (ErrorMessage errorMessage in errors)
            {
                if (!errorMessage.IsValid)
                {
                    ModelState.AddModelError(errorMessage.FieldName, errorMessage.Message);
                }
            }
        }
    }
}
