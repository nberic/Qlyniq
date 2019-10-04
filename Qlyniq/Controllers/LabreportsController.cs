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
    public class LabreportsController : Controller
    {
        private readonly QlyniqContext _context;

        public LabreportsController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Labreports
        public async Task<IActionResult> Index()
        {
            var qlyniqContext = _context.Labreports.Include(l => l.Patient).Include(l => l.Recipient);
            return View(await qlyniqContext.ToListAsync());
        }

        // GET: Labreports/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labreports = await _context.Labreports
                .Include(l => l.Patient)
                .Include(l => l.Recipient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labreports == null)
            {
                return NotFound();
            }

            return View(labreports);
        }

        // GET: Labreports/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName");
            ViewData["RecipientId"] = new SelectList(_context.Employees, "Id", "FirstName");
            return View();
        }

        // POST: Labreports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecipientId,PatientId,AcceptedTime,SampledTime,Glucose,Urea,Creatine,Cholesterol,Helicobacter")] Labreports labreports)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labreports);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", labreports.PatientId);
            ViewData["RecipientId"] = new SelectList(_context.Employees, "Id", "FirstName", labreports.RecipientId);
            return View(labreports);
        }

        // GET: Labreports/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labreports = await _context.Labreports.FindAsync(id);
            if (labreports == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", labreports.PatientId);
            ViewData["RecipientId"] = new SelectList(_context.Employees, "Id", "FirstName", labreports.RecipientId);
            return View(labreports);
        }

        // POST: Labreports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,RecipientId,PatientId,AcceptedTime,SampledTime,Glucose,Urea,Creatine,Cholesterol,Helicobacter")] Labreports labreports)
        {
            if (id != labreports.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labreports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabreportsExists(labreports.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", labreports.PatientId);
            ViewData["RecipientId"] = new SelectList(_context.Employees, "Id", "FirstName", labreports.RecipientId);
            return View(labreports);
        }

        // GET: Labreports/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labreports = await _context.Labreports
                .Include(l => l.Patient)
                .Include(l => l.Recipient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labreports == null)
            {
                return NotFound();
            }

            return View(labreports);
        }

        // POST: Labreports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var labreports = await _context.Labreports.FindAsync(id);
            bool isInExaminations = await _context.Examinations.Where(e => e.LabReportId == id).AnyAsync();

            if (isInExaminations)
            {
                ViewData["DeletionError"] = $"The lab report cannot be deleted because it is referenced elsewhere in the database!";
                return View(labreports);
            }

            _context.Labreports.Remove(labreports);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabreportsExists(uint id)
        {
            return _context.Labreports.Any(e => e.Id == id);
        }
    }
}
