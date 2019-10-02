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
    public class DeansController : Controller
    {
        private readonly QlyniqContext _context;

        public DeansController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Deans
        public async Task<IActionResult> Index()
        {
            var qlyniqContext = _context.Deans.Include(d => d.Employee).Include(d => d.Office);
            return View(await qlyniqContext.ToListAsync());
        }

        // GET: Deans/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deans = await _context.Deans
                .Include(d => d.Employee)
                .Include(d => d.Office)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deans == null)
            {
                return NotFound();
            }

            return View(deans);
        }

        // GET: Deans/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name");
            return View();
        }

        // POST: Deans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OfficeId,EmployeeId")] Deans deans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", deans.EmployeeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", deans.OfficeId);
            return View(deans);
        }

        // GET: Deans/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deans = await _context.Deans.FindAsync(id);
            if (deans == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", deans.EmployeeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", deans.OfficeId);
            return View(deans);
        }

        // POST: Deans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,OfficeId,EmployeeId")] Deans deans)
        {
            if (id != deans.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeansExists(deans.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", deans.EmployeeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", deans.OfficeId);
            return View(deans);
        }

        // GET: Deans/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deans = await _context.Deans
                .Include(d => d.Employee)
                .Include(d => d.Office)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deans == null)
            {
                return NotFound();
            }

            return View(deans);
        }

        // POST: Deans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var deans = await _context.Deans.FindAsync(id);
            _context.Deans.Remove(deans);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeansExists(uint id)
        {
            return _context.Deans.Any(e => e.Id == id);
        }
    }
}
