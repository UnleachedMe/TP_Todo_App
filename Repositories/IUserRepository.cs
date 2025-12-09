using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
    }
}
