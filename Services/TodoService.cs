using GitDemoToDoApp.Models;
using GitDemoToDoApp.Repositories;

namespace GitDemoToDoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public IEnumerable<TodoItem> GetAllTodos()
        {
            return _todoRepository.GetAll();
        }

        public TodoItem GetTodoById(int id)
        {
            return _todoRepository.GetById(id);
        }

        public void CreateTodo(TodoItem todo)
        {
            _todoRepository.Add(todo);
        }

        public bool UpdateTodo(TodoItem todo)
        {
            return _todoRepository.Update(todo);
        }

        public bool DeleteTodo(int id)
        {
            return _todoRepository.Delete(id);
        }
    }
}
