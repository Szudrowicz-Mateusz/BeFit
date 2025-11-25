using HomeFinances.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeFinances.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<SessionExercise> SessionExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seedowanie Roli Administratora
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "admin-role-id",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

            // Seedowanie przykładowych ćwiczeń
            modelBuilder.Entity<ExerciseType>().HasData(
                new ExerciseType { Id = 1, Name = "Przysiady ze sztangą" },
                new ExerciseType { Id = 2, Name = "Wyciskanie na ławce płaskiej" },
                new ExerciseType { Id = 3, Name = "Martwy ciąg" },
                new ExerciseType { Id = 4, Name = "Wiosłowanie sztangą w opadzie" }
            );
        }
    }
}