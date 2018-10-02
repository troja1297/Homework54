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
    public class PhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PhoneValidator _phoneValidator;

        public PhonesController(ApplicationDbContext context,
            PhoneValidator phoneValidator)
        {
            _context = context;
            _phoneValidator = phoneValidator;
        }

        // GET: Phones
        public async Task<IActionResult> Index(int? companyId, string name)
        {
            List<Company> companies = _context.Companies.ToList();

            //companies.Insert(0, new Company { Id = 0, Name = "Все" });

            var phones = _context.Phones.Include(p => p.Category).Include(p => p.Company).ToList();

            IndexViewModel ivm = new IndexViewModel();

            if (companyId.HasValue)
            {
                phones = phones.Where(p => p.Company.Id == companyId.Value).ToList();
                ivm.Company = companies.FirstOrDefault(c => c.Id == companyId.Value);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                phones = phones.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                ivm.Name = name;
            }

            ivm.Companies = companies;
            ivm.Phones = phones;
            
            return View(ivm);
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

            foreach (ErrorMessage errorMessage in errors)
            {
                if (!errorMessage.IsValid)
                {
                    ModelState.AddModelError(errorMessage.FieldName, errorMessage.Message);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(GetEditModel(phone));
            }
            try
            {
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

        public IActionResult CreateReview(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("Details", "Phones", new {id = feedback.PhoneId});
        }
    }
}
