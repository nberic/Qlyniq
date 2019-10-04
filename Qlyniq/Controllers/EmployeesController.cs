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
    public class EmployeesController : Controller
    {
        private readonly QlyniqContext _context;

        public EmployeesController(QlyniqContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var qlyniqContext = _context.Employees.Include(e => e.DeanOffice).Include(e => e.Office);
            return View(await qlyniqContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.DeanOffice)
                .Include(e => e.Office)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DeanOfficeId"] = new SelectList(_context.Offices, "Id", "Name");
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OfficeId,SocialSecurityNumber,FirstName,LastName,BirthDate,Gender,IsMedicalWorker,MedicalTitle,IsDean,DeanOfficeId")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeanOfficeId"] = new SelectList(_context.Offices, "Id", "Name", employees.DeanOfficeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", employees.OfficeId);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["DeanOfficeId"] = new SelectList(_context.Offices, "Id", "Name", employees.DeanOfficeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", employees.OfficeId);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,OfficeId,SocialSecurityNumber,FirstName,LastName,BirthDate,Gender,IsMedicalWorker,MedicalTitle,IsDean,DeanOfficeId")] Employees employees)
        {
            if (id != employees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.Id))
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
            ViewData["DeanOfficeId"] = new SelectList(_context.Offices, "Id", "Name", employees.DeanOfficeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", employees.OfficeId);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.DeanOffice)
                .Include(e => e.Office)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var employees = await _context.Employees.FindAsync(id);

            bool isInAppointments = await _context.Appointments.Where(a => a.DoctorId == id).AnyAsync();
            bool isInExaminations = await _context.Examinations.Where(e => e.DoctorId == id).AnyAsync();
            bool isInFiles = await _context.Files.Where(f => f.CreatorId == id).AnyAsync();
            bool isInLabReports = await _context.Labreports.Where(lr => lr.RecipientId == id).AnyAsync();

            if (isInAppointments || isInExaminations || isInFiles || isInLabReports)
            {
                ViewData["DeletionError"] = $"The employee cannot be deleted because it is referenced elsewhere in the database!";
                return View(employees);
            }

            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(uint id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
