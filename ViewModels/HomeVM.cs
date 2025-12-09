using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.ViewModels
{
    public class HomeVM
    {
        // Vient de la Session
        public string Username { get; set; } = string.Empty;

        // Vient d'un Cookie
        public string Theme { get; set; } = "light";

        // Vient de TempData
        public string? LastLogout { get; set; }

        // Vient du TodoService (base de données)
        public IEnumerable<TodoItemVM> Todos { get; set; } = new List<TodoItemVM>();
    }
}
