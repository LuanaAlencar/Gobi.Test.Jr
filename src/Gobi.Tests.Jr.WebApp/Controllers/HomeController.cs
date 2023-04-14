using Gobi.Test.Jr.Application;
using Gobi.Test.Jr.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Gobi.Tests.Jr.WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoItemService _todoItemService;

        public TodoController(TodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public IActionResult Index()
        {
            var items = _todoItemService.GetAll().OrderBy(i => i.Completed);
 
            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        public IActionResult Add(TodoItem todoItem)
        {
            _todoItemService.Add(todoItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _todoItemService.GetAll().Where(i => i.Id == id).FirstOrDefault();
            return View(new TodoItem() { Id = item.Id, Completed = item.Completed, Description = item.Description });
        }

        [HttpPost]
        public IActionResult Edit(TodoItem todoItem)
        {
            _todoItemService.Update(todoItem);

            return RedirectToAction("Index");
        }

               
        [HttpDelete]
        public IActionResult Delete(int todoItemId)
        {
            _todoItemService.Delete(todoItemId);
            return RedirectToAction("Index");
        }

        
    }

}

