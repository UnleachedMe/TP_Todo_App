using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);

    }
}
