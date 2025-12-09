using GitDemoToDoApp.Data;
using GitDemoToDoApp.Models;

namespace GitDemoToDoApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem GetById(int id)
        {
            return _context.TodoItems.FirstOrDefault(t => t.Id == id);
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public bool Update(TodoItem item)
        {
            var todoFromDb = _context.TodoItems.FirstOrDefault(t => t.Id == item.Id);
            if (todoFromDb != null)
            {
                todoFromDb.Title = item.Title;
                todoFromDb.Description = item.Description;
                todoFromDb.Status = item.Status;
                todoFromDb.DueDate = item.DueDate;
                _context.SaveChanges();
                return true; // Succès
            }

            return false;
        }

        public bool Delete(int id)
        {
            var todoToDelete = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todoToDelete != null)
            {
                _context.TodoItems.Remove(todoToDelete);
                _context.SaveChanges();
                return true; // Succès
            }

            return false;
        }

    }
}
