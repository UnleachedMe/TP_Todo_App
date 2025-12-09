using GitDemoToDoApp.Mappers;
using GitDemoToDoApp.Services;
using GitDemoToDoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GitDemoToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoService _todoService;

        public HomeController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "User");

            var todos = _todoService.GetAllTodos();

            // Utilise le mapper pour convertir la liste de TodoItem en liste de TodoItemVM
            var todoViewModels = todos.ToViewModel();

            var model = new HomeVM
            {
                Username = username,
                Theme = Request.Cookies["Theme"] ?? "light",
                LastLogout = TempData["LastLogout"]?.ToString() ?? "Jamais",
                Todos = todoViewModels
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoItemVM vm)
        {
            if (ModelState.IsValid)
            {
                // Utilise le mapper pour convertir le ViewModel en TodoItem (fonction magique)
                var todoItem = vm.ToTodoItem();

                _todoService.CreateTodo(todoItem);
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null) return NotFound();

            // Utilise le mapper pour convertir le TodoItem en EditTodoViewModel
            var vm = todo.ToTodoItemViewModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TodoItemVM vm)
        {
            if (id != vm.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                // Utilise le mapper pour convertir le ViewModel en TodoItem
                var todoItem = vm.ToTodoItem();

                var success = _todoService.UpdateTodo(todoItem);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var success = _todoService.DeleteTodo(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
