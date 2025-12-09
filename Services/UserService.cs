using GitDemoToDoApp.Data;
using GitDemoToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GitDemoToDoApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            // On cherche l'utilisateur par son nom
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Si l'utilisateur n'existe pas, on retourne null
            if (user == null)
                return null;

            // On hashe le mot de passe fourni et on le compare avec celui en base
            var hashedPassword = HashPassword(password);

            if (user.PasswordHash == hashedPassword)
            {
                return user; // Authentification réussie
            }

            return null; // Mot de passe incorrect
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
