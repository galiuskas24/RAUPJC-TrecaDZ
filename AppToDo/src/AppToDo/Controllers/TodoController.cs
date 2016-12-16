using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppToDo.Interfaces;
using AppToDo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppToDo.Controllers
{
    public class TodoController : Controller
    {
        private ITodoRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        private int i = 0;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var item = _repository.GetActive(Guid.Parse(currentUser.Id));

            return View(item);
        }

        public async Task<IActionResult> GetCompleted()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var items = _repository.GetCompleted(Guid.Parse(currentUser.Id));

            return View(items);
        }

        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            _repository.MarkAsCompleted(id, Guid.Parse(currentUser.Id));

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddTodoViewModel m)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var item = new TodoItem(m.Text, Guid.Parse(currentUser.Id));
                _repository.Add(item);
                return RedirectToAction("Index");
            }

            return View("Add", m);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            _repository.Remove(id, Guid.Parse(currentUser.Id));
            return RedirectToAction("GetCompleted");
        }

    }
}
