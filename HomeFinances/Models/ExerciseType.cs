using System.ComponentModel.DataAnnotations;

namespace HomeFinances.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa ćwiczenia")]
        [MaxLength(100, ErrorMessage = "Nazwa zbyt długa")]
        public string Name { get; set; }

        public virtual ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
    }
}