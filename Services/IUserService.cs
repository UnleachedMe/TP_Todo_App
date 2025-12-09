using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.Services
{
    public interface IUserService
    {
        User? Authenticate(string username, string password);

    }
}
