using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListOld.Models;

namespace ToDoListOld.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly Context _context;

        public ToDoRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<ToDo> GetAllTasks()
        {
            return _context.ToDoLists;
        }

        public ToDo GetTask(int todoId)
        {
            var task = _context.ToDoLists.FirstOrDefault(x => x.Id == todoId);
            return task;
        }

        public int AddTask(ToDo todo)
        {
            _context.ToDoLists.Add(todo);
            _context.SaveChanges();
            return todo.Id;
        }

        public void UpdateTask(ToDo todo)
        {
            _context.Attach(todo);
            _context.Entry(todo).Property("Title").IsModified = true;
            _context.Entry(todo).Property("Description").IsModified = true;
            _context.Entry(todo).Property("IsDone").IsModified = true;
            _context.Entry(todo).Property("ModifiedDate").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.ToDoLists.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                _context.ToDoLists.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
