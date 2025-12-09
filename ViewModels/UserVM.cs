using System.ComponentModel.DataAnnotations;

namespace GitDemoToDoApp.ViewModels
{
    public class UserVM
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
