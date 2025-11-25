using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeFinances.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HomeFinances.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var stats = await _context.SessionExercises
                .Where(e => e.TrainingSession.UserId == userId && e.TrainingSession.StartDateTime >= fourWeeksAgo)
                .GroupBy(e => e.ExerciseType.Name)
                .Select(g => new ExerciseStatViewModel
                {
                    ExerciseName = g.Key,
                    ExecutionCount = g.Count(),
                    TotalReps = g.Sum(x => x.Series * x.Repetitions),
                    AvgLoad = g.Average(x => x.Load),
                    MaxLoad = g.Max(x => x.Load)
                })
                .ToListAsync();

            return View(stats);
        }
    }

    public class ExerciseStatViewModel
    {
        public string ExerciseName { get; set; }
        public int ExecutionCount { get; set; }
        public int TotalReps { get; set; }
        public double AvgLoad { get; set; }
        public double MaxLoad { get; set; }
    }
}