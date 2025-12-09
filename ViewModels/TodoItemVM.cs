using System.ComponentModel.DataAnnotations;

namespace GitDemoToDoApp.ViewModels
{
    public class TodoItemVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Le statut est obligatoire.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date d'échéance est obligatoire.")]
        public string DueDate { get; set; } = string.Empty;

        public bool IsOverdue { get; set; }

        public bool IsCompleted { get; set; }
    }
}
