using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListOld.Models;
using ToDoListOld.Repositories;

namespace ToDoListOld.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public IActionResult Index()
        {
            var toDoes = _toDoRepository.GetAllTasks();
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
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now.ToString("D");
                _toDoRepository.AddTask(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var task = _toDoRepository.GetTask(id);
            return View(task);
        }

        [HttpPost]
        public IActionResult EditTask(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                _toDoRepository.UpdateTask(todo);
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        public IActionResult Details(int id)
        {
            var task = _toDoRepository.GetTask(id);
            return View(task);
        }

        public IActionResult Delete(int id)
        {
            _toDoRepository.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}
