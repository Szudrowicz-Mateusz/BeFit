using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeFinances.Data;
using HomeFinances.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HomeFinances.Controllers
{
    [Authorize]
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Filtrowanie po użytkowniku
            var sessions = await _context.TrainingSessions
                .Where(s => s.UserId == GetUserId())
                .OrderByDescending(s => s.StartDateTime)
                .ToListAsync();
            return View(sessions);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingSession session)
        {
           
            session.UserId = GetUserId();
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var session = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == GetUserId());

            if (session == null) return NotFound();
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainingSession session)
        {
            if (id != session.Id) return NotFound();
            session.UserId = GetUserId();
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _context.Update(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var session = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == GetUserId());
            if (session == null) return NotFound();
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.TrainingSessions.FindAsync(id);
            
            if (session != null && session.UserId == GetUserId())
            {
                _context.TrainingSessions.Remove(session);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}