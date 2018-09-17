﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public StockController(ApplicationDbContext context,
            IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Stock
        public ActionResult Index()
        {
            IEnumerable<Stock> stocks = _context.Stocks.OrderBy(s => s.Name);
            return View(stocks);
        }

        // GET: Stock/Details/5
        public ActionResult Details(int id)
        {
            Stock stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound($"Сорян, склад с id {id} сгорел");
            }

            return View(stock);
        }


        public IActionResult Download(string id)
        {
            try
            {
                string filePath = Path.Combine(_environment.ContentRootPath, $"Files/stock_{id}.pdf");
                string fileType = "application/pdf";
                string fileName = $"stock_{id}.pdf";
                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Книга про склад {id} не найдена");
                }
                return PhysicalFile(filePath, fileType, fileName);
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
                return View("404");
            }
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Stock stock)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stock/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stock/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}