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
    public class ExaminationsController : Controller
    {
        private readonly QlyniqContext _context;

        public ExaminationsController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Examinations
        public async Task<IActionResult> Index()
        {
            var qlyniqContext = _context.Examinations.Include(e => e.Diagnosis).Include(e => e.Doctor).Include(e => e.File).Include(e => e.LabReport).Include(e => e.Patient);
            return View(await qlyniqContext.ToListAsync());
        }

        // GET: Examinations/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examinations = await _context.Examinations
                .Include(e => e.Diagnosis)
                .Include(e => e.Doctor)
                .Include(e => e.File)
                .Include(e => e.LabReport)
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examinations == null)
            {
                return NotFound();
            }

            return View(examinations);
        }

        // GET: Examinations/Create
        public IActionResult Create()
        {
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "Id", "Code");
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "FirstName");
            ViewData["FileId"] = new SelectList(_context.Files, "Id", "Name");
            ViewData["LabReportId"] = new SelectList(_context.Labreports, "Id", "Id");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName");
            return View();
        }

        // POST: Examinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DoctorId,StartingTime,FileId,DiagnosisId,Therapy,IsEmergency,LabReportId")] Examinations examinations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examinations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "Id", "Code", examinations.DiagnosisId);
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "FirstName", examinations.DoctorId);
            ViewData["FileId"] = new SelectList(_context.Files, "Id", "Name", examinations.FileId);
            ViewData["LabReportId"] = new SelectList(_context.Labreports, "Id", "Id", examinations.LabReportId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", examinations.PatientId);
            return View(examinations);
        }

        // GET: Examinations/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examinations = await _context.Examinations.FindAsync(id);
            if (examinations == null)
            {
                return NotFound();
            }
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "Id", "Code", examinations.DiagnosisId);
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "FirstName", examinations.DoctorId);
            ViewData["FileId"] = new SelectList(_context.Files, "Id", "Name", examinations.FileId);
            ViewData["LabReportId"] = new SelectList(_context.Labreports, "Id", "Id", examinations.LabReportId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", examinations.PatientId);
            return View(examinations);
        }

        // POST: Examinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,PatientId,DoctorId,StartingTime,FileId,DiagnosisId,Therapy,IsEmergency,LabReportId")] Examinations examinations)
        {
            if (id != examinations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examinations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExaminationsExists(examinations.Id))
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
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "Id", "Code", examinations.DiagnosisId);
            ViewData["DoctorId"] = new SelectList(_context.Employees, "Id", "FirstName", examinations.DoctorId);
            ViewData["FileId"] = new SelectList(_context.Files, "Id", "Name", examinations.FileId);
            ViewData["LabReportId"] = new SelectList(_context.Labreports, "Id", "Id", examinations.LabReportId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", examinations.PatientId);
            return View(examinations);
        }

        // GET: Examinations/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examinations = await _context.Examinations
                .Include(e => e.Diagnosis)
                .Include(e => e.Doctor)
                .Include(e => e.File)
                .Include(e => e.LabReport)
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examinations == null)
            {
                return NotFound();
            }

            return View(examinations);
        }

        // POST: Examinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var examinations = await _context.Examinations.FindAsync(id);
            _context.Examinations.Remove(examinations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExaminationsExists(uint id)
        {
            return _context.Examinations.Any(e => e.Id == id);
        }
    }
}
