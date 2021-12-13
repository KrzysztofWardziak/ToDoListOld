using System.Collections.Generic;
using System.Linq;
using Moq;
using ToDoListOld.Controllers;
using ToDoListOld.Models;
using ToDoListOld.Repositories;
using Xunit;

namespace ToDoListOld.Tests
{
    public class Tests
    {

       [Fact]
        public void CanGetAllTasks()
        {
            ToDo todo = new ToDo()
            {
                Description = "Test",
                Id = 1,
                IsDone = false,
                Title = "Test"
            };
            ToDo todo2 = new ToDo()
            {
                Description = "Test2",
                Id = 2,
                IsDone = false,
                Title = "Test2"
            };

            var todos = new List<ToDo>();
            todos.Add(todo);
            todos.Add(todo2);
            var todosV2 = todos.AsQueryable();

            var mock = new Mock<IToDoRepository>();
            mock.Setup(s => s.GetAllTasks()).Returns(todosV2);

            var returnedTasks = mock.Object.GetAllTasks();

            Assert.NotNull(returnedTasks);
        }

        [Fact]
        public void CanGetTaskById()
        {
            ToDo todo = new ToDo()
            {
                Description = "Test",
                Id = 1,
                IsDone = false,
                Title = "Test"
            };

            var mock = new Mock<IToDoRepository>();
            mock.Setup(s => s.GetTask(1)).Returns(todo);
            var returnedItem = mock.Object.GetTask(todo.Id);

            Assert.Equal(todo, returnedItem);
        }

        [Fact]
        public void CanDeleteTaskWithProperId()
        {
            ToDo todo = new ToDo()
            {
                Description = "Test",
                Id = 1,
                IsDone = false,
                Title = "Test"
            };

            var mock = new Mock<IToDoRepository>();
            mock.Setup(s => s.GetTask(1)).Returns(todo);
            mock.Setup(m => m.DeleteTask(todo.Id));

            mock.Object.DeleteTask(todo.Id);

            mock.Verify(s => s.DeleteTask(todo.Id));
        }

        [Fact]
        public void CanAddTask()
        {
            ToDo todo = new ToDo()
            {
                Description = "Test",
                Id = 1,
                IsDone = false,
                Title = "Test"
            };

            var mock = new Mock<IToDoRepository>();
            mock.Setup(m => m.AddTask(todo));
            mock.Setup(s => s.GetTask(1)).Returns(todo);

            var task = mock.Object.GetTask(todo.Id);

            Assert.Equal(todo, task);
        }

        [Fact]
        public void CanUpdateTask()
        {
            ToDo todo = new()
            {
                Description = "Test",
                Id = 1,
                IsDone = false,
                Title = "Test"
            };

            var mock = new Mock<IToDoRepository>();
            mock.Setup(s => s.AddTask(todo));
            todo.Id = 2;
            todo.Description = "Test2";
            todo.IsDone = true;
            todo.Title = "Test2";
            mock.Object.UpdateTask(todo);
            mock.Verify(x => x.UpdateTask(todo));
        }
    }
}