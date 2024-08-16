using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManegment.Data;
using TaskManegment.Models;

namespace TaskManegment.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Notes.Include(n => n.Tasks);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.Tasks)
                .FirstOrDefaultAsync(m => m.NotesId == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewBag.TaskId = new SelectList(_context.Tasks, "TaskId", "TaskName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,NoteText,CreatedBy,CreatedOn,AssignedTo,UpdatedBy,UpdateDate")] Notes notes)
        {
 
            _context.Add(notes);
            await _context.SaveChangesAsync();
            ViewBag.TaskId = new SelectList(_context.Tasks, "TaskId", "TaskName", notes.TaskId);
            return RedirectToAction(nameof(Index)); 
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskName", "TaskId", notes.TaskId);
            return View(notes);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotesId,TaskId,NoteText,CreatedBy,CreatedOn,AssignedTo,UpdatedBy,UpdateDate")] Notes notes)
        {
            if (id != notes.NotesId)
            {
                return NotFound();
            }

           
            try
            {
                    _context.Update(notes);
                    await _context.SaveChangesAsync();
                    ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", notes.TaskId);

            }
             catch (DbUpdateConcurrencyException)
                   {
                    if (!NotesExists(notes.NotesId))
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

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.Tasks)
                .FirstOrDefaultAsync(m => m.NotesId == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notes = await _context.Notes.FindAsync(id);
            if (notes != null)
            {
                _context.Notes.Remove(notes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.NotesId == id);
        }
    }
}
