using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TaskManegment.Data;
using TaskManegment.Models;

namespace TaskManegment.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AttachmentsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Attachments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attachments.Include(a => a.Tasks);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachments = await _context.Attachments
                .Include(a => a.Tasks)
                .FirstOrDefaultAsync(m => m.AttachmentId == id);
            if (attachments == null)
            {
                return NotFound();
            }

            return View(attachments);
        }

        // GET: Attachments/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId");
            return View();
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create([Bind("AttachmentId,TaskId,FilePath,UpdatedBy,UpdateDate")] Attachments attachments, IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                attachments.FilePath = "/uploads/" + file.FileName; // Adjust the path as per your requirement
            }

            _context.Add(attachments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", attachments.TaskId);
            
        }

        // GET: Attachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachments = await _context.Attachments.FindAsync(id);
            if (attachments == null)
            {
                return NotFound();
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", attachments.TaskId);
            return View(attachments);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttachmentId,TaskId,FilePath,CreatedBy,CreatedOn,AssignedTo,UpdatedBy,UpdateDate")] Attachments attachments)
        {
            if (id != attachments.AttachmentId)
            {
                return NotFound();
            }

             try
                {
                    _context.Update(attachments);
                    await _context.SaveChangesAsync();
                ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", attachments.TaskId);
               }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttachmentsExists(attachments.AttachmentId))
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
          
           

        // GET: Attachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachments = await _context.Attachments
                .Include(a => a.Tasks)
                .FirstOrDefaultAsync(m => m.AttachmentId == id);
            if (attachments == null)
            {
                return NotFound();
            }

            return View(attachments);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attachments = await _context.Attachments.FindAsync(id);
            if (attachments != null)
            {
                _context.Attachments.Remove(attachments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttachmentsExists(int id)
        {
            return _context.Attachments.Any(e => e.AttachmentId == id);
        }
    }
}
