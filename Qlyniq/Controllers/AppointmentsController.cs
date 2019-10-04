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
    public class AppointmentsController : Controller
    {
        private readonly QlyniqContext _context;

        public AppointmentsController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var qlyniqContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
            return View(await qlyniqContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientFirstName,PatientLastName,PatientId,DoctorId,StartingTime")] Appointments appointments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "Name", appointments.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", appointments.PatientId);
            return View(appointments);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }

            if (appointments.PatientId != null)
            {
                appointments.PatientFirstName = _context.Patients.Where(p => p.Id == appointments.PatientId).FirstOrDefault().FirstName;
                appointments.PatientLastName = _context.Patients.Where(p => p.Id == appointments.PatientId).FirstOrDefault().LastName;
            }
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "Name", appointments.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", appointments.PatientId);
            return View(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,PatientFirstName,PatientLastName,PatientId,DoctorId,StartingTime")] Appointments appointments)
        {
            if (id != appointments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (appointments.PatientId != null)
                {
                    appointments.PatientFirstName = _context.Patients.Where(p => p.Id == appointments.PatientId).FirstOrDefault().FirstName;
                    appointments.PatientLastName = _context.Patients.Where(p => p.Id == appointments.PatientId).FirstOrDefault().LastName;
                }
                try
                {
                    _context.Update(appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.Id))
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
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "FirstName", appointments.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", appointments.PatientId);
            return View(appointments);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var appointments = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentsExists(uint id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
