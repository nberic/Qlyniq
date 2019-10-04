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
    public class FilesController : Controller
    {
        private readonly QlyniqContext _context;

        public FilesController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var qlyniqContext = _context.Files.Include(f => f.Creator).Include(f => f.Patient);
            return View(await qlyniqContext.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .Include(f => f.Creator)
                .Include(f => f.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PatientId,CreatorId,CreationDate,Note")] Files files)
        {
            if (ModelState.IsValid)
            {
                _context.Add(files);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Employees, "Id", "FirstName", files.CreatorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", files.PatientId);
            return View(files);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Employees, "Id", "FirstName", files.CreatorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", files.PatientId);
            return View(files);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Name,PatientId,CreatorId,CreationDate,Note")] Files files)
        {
            if (id != files.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(files);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilesExists(files.Id))
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
            ViewData["CreatorId"] = new SelectList(_context.Employees, "Id", "FirstName", files.CreatorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", files.PatientId);
            return View(files);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .Include(f => f.Creator)
                .Include(f => f.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var files = await _context.Files.FindAsync(id);
            _context.Files.Remove(files);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilesExists(uint id)
        {
            return _context.Files.Any(e => e.Id == id);
        }
    }
}
