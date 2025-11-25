using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeFinances.Data;
using HomeFinances.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HomeFinances.Controllers
{
    [Authorize]
    public class SessionExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public SessionExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionExercises
        public async Task<IActionResult> Index()
        {
            
            var exercises = _context.SessionExercises
                .Include(s => s.ExerciseType)
                .Include(s => s.TrainingSession)
                .Where(s => s.TrainingSession.UserId == GetUserId());
            return View(await exercises.ToListAsync());
        }

        // GET: SessionExercises/Create
        public IActionResult Create()
        {
            SetupViewBag();
            return View();
        }

        // POST: SessionExercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionExercise sessionExercise)
        {
            
            var userSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == sessionExercise.TrainingSessionId && s.UserId == GetUserId());

            if (userSession == null) ModelState.AddModelError("", "Nieprawidłowa sesja.");

            if (ModelState.IsValid)
            {
                _context.Add(sessionExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetupViewBag(sessionExercise);
            return View(sessionExercise);
        }

        // GET: SessionExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sessionExercise = await _context.SessionExercises
                .Include(s => s.TrainingSession) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sessionExercise == null) return NotFound();

           
            if (sessionExercise.TrainingSession.UserId != GetUserId()) return NotFound();

            SetupViewBag(sessionExercise);
            return View(sessionExercise);
        }

        // POST: SessionExercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionExercise sessionExercise)
        {
            if (id != sessionExercise.Id) return NotFound();

            var isMySession = await _context.TrainingSessions
                .AnyAsync(s => s.Id == sessionExercise.TrainingSessionId && s.UserId == GetUserId());

            if (!isMySession) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExerciseExists(sessionExercise.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetupViewBag(sessionExercise);
            return View(sessionExercise);
        }

        // GET: SessionExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sessionExercise = await _context.SessionExercises
                .Include(s => s.ExerciseType)
                .Include(s => s.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sessionExercise == null) return NotFound();

           
            if (sessionExercise.TrainingSession.UserId != GetUserId()) return NotFound();

            return View(sessionExercise);
        }

        // POST: SessionExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionExercise = await _context.SessionExercises
                .Include(s => s.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);

            
            if (sessionExercise != null && sessionExercise.TrainingSession.UserId == GetUserId())
            {
                _context.SessionExercises.Remove(sessionExercise);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Metody pomocnicze
        private void SetupViewBag(SessionExercise ex = null)
        {
            var userSessions = _context.TrainingSessions
                .Where(s => s.UserId == GetUserId())
                .Select(s => new { Id = s.Id, Desc = $"{s.StartDateTime:g} - {s.EndDateTime:t}" })
                .OrderByDescending(s => s.Id)
                .ToList();

            ViewData["TrainingSessionId"] = new SelectList(userSessions, "Id", "Desc", ex?.TrainingSessionId);
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", ex?.ExerciseTypeId);
        }

        private bool SessionExerciseExists(int id)
        {
            return _context.SessionExercises.Any(e => e.Id == id);
        }
    }
}