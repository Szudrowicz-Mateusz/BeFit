using System.ComponentModel.DataAnnotations;

namespace HomeFinances.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data rozpoczęcia jest wymagana")]
        [Display(Name = "Początek sesji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Data zakończenia jest wymagana")]
        [Display(Name = "Koniec sesji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }

        // Powiązanie z użytkownikiem
        public string UserId { get; set; }
        public virtual AppUser? User { get; set; }

        public virtual ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
    }
}