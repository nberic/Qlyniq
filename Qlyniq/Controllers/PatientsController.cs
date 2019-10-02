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
    public class PatientsController : Controller
    {
        private readonly QlyniqContext _context;

        public PatientsController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SocialSecurityNumber,FirstName,LastName,BirthDate,Gender,HealthCareProvider")] Patients patients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patients);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients.FindAsync(id);
            if (patients == null)
            {
                return NotFound();
            }
            return View(patients);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,SocialSecurityNumber,FirstName,LastName,BirthDate,Gender,HealthCareProvider")] Patients patients)
        {
            if (id != patients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientsExists(patients.Id))
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
            return View(patients);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var patients = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientsExists(uint id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
