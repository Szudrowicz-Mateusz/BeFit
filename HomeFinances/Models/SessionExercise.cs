using System.ComponentModel.DataAnnotations;

namespace HomeFinances.Models
{
    public class SessionExercise
    {
        public int Id { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }
        public virtual TrainingSession? TrainingSession { get; set; }

        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }
        [Display(Name = "Typ ćwiczenia")]
        public virtual ExerciseType? ExerciseType { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Obciążenie musi być dodatnie do 1000kg")]
        [Display(Name = "Obciążenie (kg)")]
        public double Load { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Liczba serii musi być większa od 0, a mniejsza od 100")]
        [Display(Name = "Liczba serii")]
        public int Series { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Liczba powtórzeń musi być większa od 0, a mniejsza od 1000")]
        [Display(Name = "Powtórzenia w serii")]
        public int Repetitions { get; set; }
    }
}