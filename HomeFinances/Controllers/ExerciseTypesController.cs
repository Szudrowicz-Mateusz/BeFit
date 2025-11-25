using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeFinances.Data;
using HomeFinances.Models;
using Microsoft.AspNetCore.Authorization;

namespace HomeFinances.Controllers
{
    // Wyświetlanie dostępne dla wszystkich
    public class ExerciseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerciseTypes.ToListAsync());
        }

        // Tylko Administrator może tworzyć/edytować/usuwać
        [Authorize(Roles = "Administrator")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(ExerciseType exerciseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exerciseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerciseType);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var type = await _context.ExerciseTypes.FindAsync(id);
            if (type == null) return NotFound();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, ExerciseType exerciseType)
        {
            if (id != exerciseType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(exerciseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerciseType);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var type = await _context.ExerciseTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (type == null) return NotFound();
            return View(type);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type = await _context.ExerciseTypes.FindAsync(id);
            if (type != null) _context.ExerciseTypes.Remove(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}