using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAllTodos();
        TodoItem GetTodoById(int id);
        void CreateTodo(TodoItem todo);
        bool UpdateTodo(TodoItem todo);
        bool DeleteTodo(int id);
    }
}
