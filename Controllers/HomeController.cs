using GitDemoToDoApp.Models;
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

            var model = new HomeVM
            {
                Username = username,
                Theme = Request.Cookies["Theme"] ?? "light",
                LastLogout = TempData["LastLogout"]?.ToString() ?? "Jamais",
                Todos = todos
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                _todoService.CreateTodo(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public IActionResult Edit(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TodoItem updatedTodo)
        {
            if (id != updatedTodo.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var success = _todoService.UpdateTodo(updatedTodo);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(updatedTodo);
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
