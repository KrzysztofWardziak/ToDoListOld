using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListOld.Models;

namespace ToDoListOld.Repositories
{
   public interface IToDoRepository
    {
        IQueryable<ToDo> GetAllTasks();
        ToDo GetTask(int todoId);
        int AddTask(ToDo todo);
        void UpdateTask(ToDo todo);
        void DeleteTask(int id);
    }
}
