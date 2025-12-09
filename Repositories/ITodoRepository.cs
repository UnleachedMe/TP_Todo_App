using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.Repositories
{
    public interface ITodoRepository
    {
        List<TodoItem> GetAll();
        TodoItem GetById(int id);
        void Add(TodoItem item);
        bool Update(TodoItem item);
        bool Delete(int id);
    }
}
