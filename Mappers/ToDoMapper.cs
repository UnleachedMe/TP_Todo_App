using GitDemoToDoApp.Enums;
using GitDemoToDoApp.Models;
using GitDemoToDoApp.ViewModels;

namespace GitDemoToDoApp.Mappers
{
    public static class TodoMapper
    {
        // From TodoItem to TodoItemViewModel

        public static TodoItemVM ToTodoItemViewModel(this TodoItem todo)
        {
            return new TodoItemVM
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status.ToString(),
                DueDate = todo.DueDate.ToString("yyyy-MM-dd"),
                IsOverdue = todo.DueDate < DateTime.Now && todo.Status != ToDoStatus.Completed,
                IsCompleted = todo.Status == ToDoStatus.Completed
            };
        }

        // From list de TodoItem to list de TodoItemViewModel
        public static List<TodoItemVM> ToViewModel(this IEnumerable<TodoItem> todos)
        {
            return todos.Select(t => t.ToTodoItemViewModel()).ToList();
        }


        // From TodoVM to TodoItem

        public static TodoItem ToTodoItem(this TodoItemVM vm)
        {
            ToDoStatus status;
            if (!Enum.TryParse<ToDoStatus>(vm.Status, out status))
            {
                status = ToDoStatus.Pending; // Valeur par défaut si le parsing échoue
            }

            DateTime dueDate;
            if (!DateTime.TryParse(vm.DueDate, out dueDate))
            {
                dueDate = DateTime.Today; // Valeur par défaut si la date est invalide
            }

            return new TodoItem
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                Status = status,
                DueDate = dueDate
            };
        }
    }
}
