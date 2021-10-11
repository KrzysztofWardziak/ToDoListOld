using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListOld.Models;

namespace ToDoListOld.Controllers
{
    public class ToDoController : Controller
    {
        private readonly Context _context;

        public ToDoController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var toDoes = _context.ToDoLists;
            return View(toDoes);
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            return View(new ToDo());
        }

        [HttpPost]
        public IActionResult AddTask(ToDo model)
        {
            model.CreatedDate = DateTime.Now.ToString("D");
            _context.ToDoLists.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var task = _context.ToDoLists.FirstOrDefault(x => x.Id == id);
            if (task == null)
                throw new Exception("Zadanie nie istnieje!");

            return View(task);
        }

        [HttpPost]
        public IActionResult EditTask(ToDo todo)
        {
            _context.Attach(todo);
            _context.Entry(todo).Property("Title").IsModified = true;
            _context.Entry(todo).Property("Description").IsModified = true;
            _context.Entry(todo).Property("IsDone").IsModified = true;
            _context.Entry(todo).Property("ModifiedDate").IsModified = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var task = _context.ToDoLists.FirstOrDefault(x => x.Id == id);
            if (task == null)
                throw new Exception("Zadanie nie istnieje!");

            return View(task);
        }

        public IActionResult Delete(int id)
        {
            var task = _context.ToDoLists.FirstOrDefault(x => x.Id == id);
            if (task == null)
                throw new Exception("Zadanie nie istnieje!");

            _context.ToDoLists.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
