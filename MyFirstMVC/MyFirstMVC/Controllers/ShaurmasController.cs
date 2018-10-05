using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstMVC.Models;
using MyFirstMVC.ViewModels;

namespace MyFirstMVC.Controllers
{
    public class Sizes
    {
        public int Size { get; set; }
        public string SizeName { get; set; }
    }

    public enum SortOrder
    {
        TypeAsc,
        TypeDesc,
        PriceAsc,
        PriceDesc,
        SizeAsc,
        SizeDesc,
        CompanyAsc,
        CompanyDesc
    }

    public class ShaurmasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShaurmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shaurmas
        public async Task<IActionResult> Index(
            string type,
            double? priceFrom,
            double? priceTo,
            int? size,
            int? companyId,
            SortOrder sortOrder = SortOrder.TypeAsc)
        {
            IQueryable<Shaurma> shaurmas = _context.Shaurmas.Include(s => s.Company);

            if (!string.IsNullOrWhiteSpace(type))
            {
                shaurmas = shaurmas.Where(s => s.Type.Contains(type));
            }

            if (priceFrom.HasValue)
            {
                shaurmas = shaurmas.Where(s => s.Price >= priceFrom.Value);
            }

            if (priceTo.HasValue)
            {
                shaurmas = shaurmas.Where(s => s.Price <= priceTo.Value);
            }

            if (size.HasValue)
            {
                ShaurmaSize shaurmaSize = (ShaurmaSize) size.Value;
                shaurmas = shaurmas.Where(s => s.ShaurmaSize == shaurmaSize);
            }

            if (companyId.HasValue)
            {
                shaurmas = shaurmas.Where(s => s.CompanyId == companyId.Value);
            }

            ViewBag.TypeSort = sortOrder == SortOrder.TypeAsc ? SortOrder.TypeDesc : SortOrder.TypeAsc;
            ViewBag.PriceSort = sortOrder == SortOrder.PriceAsc ? SortOrder.PriceDesc : SortOrder.PriceAsc;
            ViewBag.SizeSort = sortOrder == SortOrder.SizeAsc ? SortOrder.SizeDesc : SortOrder.SizeAsc;
            ViewBag.CompanySort = sortOrder == SortOrder.CompanyAsc ? SortOrder.CompanyDesc : SortOrder.CompanyAsc;

            switch (sortOrder)
            {
                case SortOrder.TypeAsc:
                    shaurmas = shaurmas.OrderBy(s => s.Type);
                    break;
                case SortOrder.TypeDesc:
                    shaurmas = shaurmas.OrderByDescending(s => s.Type);
                    break;
                case SortOrder.PriceAsc:
                    shaurmas = shaurmas.OrderBy(s => s.Price);
                    break;
                case SortOrder.PriceDesc:
                    shaurmas = shaurmas.OrderByDescending(s => s.Price);
                    break;
                case SortOrder.SizeAsc:
                    shaurmas = shaurmas.OrderBy(s => s.ShaurmaSize);
                    break;
                case SortOrder.SizeDesc:
                    shaurmas = shaurmas.OrderByDescending(s => s.ShaurmaSize);
                    break;
                case SortOrder.CompanyAsc:
                    shaurmas = shaurmas.OrderBy(s => s.Company.Name);
                    break;
                case SortOrder.CompanyDesc:
                    shaurmas = shaurmas.OrderByDescending(s => s.Company.Name);
                    break;
            }

            List<Sizes> sizes = new List<Sizes>();

            foreach (ShaurmaSize s in (ShaurmaSize[])Enum.GetValues(typeof(ShaurmaSize)))
            {
                sizes.Add(new Sizes()
                {
                    Size = (int)s,
                    SizeName = s.ToString()
                });
            }

            ViewData["Size"] = new SelectList(sizes, "Size", "SizeName");

            ShaurmaIndexViewModel model = new ShaurmaIndexViewModel()
            {
                Shaurma = await shaurmas.ToListAsync(),
                Type = type,
                CompanyId = companyId,
                Size = size,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                Sizes = new SelectList(sizes, "Size", "SizeName"),
                Companies = new SelectList(_context.Companies, "Id", "Name")
            };

            return View(model);
        }

        // GET: Shaurmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shaurma = await _context.Shaurmas
                .Include(s => s.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shaurma == null)
            {
                return NotFound();
            }

            return View(shaurma);
        }

        // GET: Shaurmas/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");

            List<Sizes> sizes = new List<Sizes>();

            foreach (ShaurmaSize s in (ShaurmaSize[])Enum.GetValues(typeof(ShaurmaSize)))
            {
                sizes.Add(new Sizes()
                {
                    Size = (int)s,
                    SizeName = s.ToString()
                });
            }

            ViewData["Size"] = new SelectList(sizes, "Size", "SizeName");
            return View();
        }

        // POST: Shaurmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Price,ShaurmaSize,CompanyId,Id")] Shaurma shaurma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shaurma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", shaurma.CompanyId);
            return View(shaurma);
        }

        // GET: Shaurmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shaurma = await _context.Shaurmas.FindAsync(id);
            if (shaurma == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", shaurma.CompanyId);
            return View(shaurma);
        }

        // POST: Shaurmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Type,Price,ShaurmaSize,CompanyId,Id")] Shaurma shaurma)
        {
            if (id != shaurma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shaurma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShaurmaExists(shaurma.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", shaurma.CompanyId);
            return View(shaurma);
        }

        // GET: Shaurmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shaurma = await _context.Shaurmas
                .Include(s => s.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shaurma == null)
            {
                return NotFound();
            }

            return View(shaurma);
        }

        // POST: Shaurmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shaurma = await _context.Shaurmas.FindAsync(id);
            _context.Shaurmas.Remove(shaurma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShaurmaExists(int id)
        {
            return _context.Shaurmas.Any(e => e.Id == id);
        }
    }
}
