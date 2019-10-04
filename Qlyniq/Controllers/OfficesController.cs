using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Qlyniq.Models;

namespace Qlyniq.Controllers
{
    public class OfficesController : Controller
    {
        private readonly QlyniqContext _context;

        public OfficesController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offices.ToListAsync());
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offices == null)
            {
                return NotFound();
            }

            return View(offices);
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OfficeNumber,Budget")] Offices offices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offices);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices.FindAsync(id);
            if (offices == null)
            {
                return NotFound();
            }
            return View(offices);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Name,OfficeNumber,Budget")] Offices offices)
        {
            if (id != offices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficesExists(offices.Id))
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
            return View(offices);
        }

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offices == null)
            {
                return NotFound();
            }

            return View(offices);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var offices = await _context.Offices.FindAsync(id);
            _context.Offices.Remove(offices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficesExists(uint id)
        {
            return _context.Offices.Any(e => e.Id == id);
        }
    }
}
