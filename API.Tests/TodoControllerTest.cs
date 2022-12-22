using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Moq;

namespace API.Tests
{
    [TestClass]
    public class TodoControllerTest
    {
        [TestMethod]
        public async Task GetTodosAsyncTest()
        {
            var data = new List<Todo>
            {
                new Todo {Task = "Walk the dog"},
                new Todo {Task = "Play games"},
                new Todo {Task = "Nap"},
            };

            var mock = new Mock<iTodoService>();

            mock.Setup(x => x.GetList()).ReturnsAsync(data);
            var controller = new TodoController(mock.Object);

            var result = await controller.GetTodosAsync();
            var items = (result as OkObjectResult).Value as List<Todo>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, items.Count());
            Assert.AreEqual("Walk the dog", items[0].Task);
        }

        [TestMethod]
        public async Task GetTodoByIdTest()
        {
            var data = new Todo
            {
                Id = 12093,
                Task = "Walk the dog"
            };

            var mock = new Mock<iTodoService>();

            mock.Setup(x => x.GetTodoById(data.Id)).ReturnsAsync(data);
            var controller = new TodoController(mock.Object);

            var result = await controller.GetTodoById(data.Id);
            var item = (result as OkObjectResult).Value as Todo;

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Id, item.Id);
            Assert.AreEqual("Walk the dog", item.Task);
        }

         [TestMethod]
        public async Task AddTodoTest()
        {
           
            var todoDto = new TodoDto
            {
                Id = 12093,
                Task = "Walk the dog"
            };
            var todo = new Todo {
                Id = 12093,
                Task = "Walk the dog"
            };
           
            var mock = new Mock<iTodoService>();
            mock.Setup(x => x.AddTodo(todo)).ReturnsAsync(todo);

            var controller = new TodoController(mock.Object);
            var result = await controller.AddTodo(todoDto);

            var statusCode = (result as ObjectResult).StatusCode;
            var returnedTodo = (result as ObjectResult).Value as Todo;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, statusCode);
        }

        [TestMethod]
        public async Task EditToDoTest()
        {
            var testId = 12093;
            var todoDto = new TodoDto
            {
                Id = 12093,
                Task = "Walk the dog"
            };
            var data = new Todo
            {
                Id = (int)todoDto.Id,
                Task = todoDto.Task
            };

            var mock = new Mock<iTodoService>();

            mock.Setup(x => x.EditTodo(todoDto)).ReturnsAsync(data);
            var controller = new TodoController(mock.Object);

            var result = await controller.EditTodo(testId, todoDto);
            var statusCode = (result as ObjectResult).StatusCode;
            var returnedTodo = (result as ObjectResult).Value as Todo;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, statusCode);
            Assert.AreEqual("Walk the dog", returnedTodo.Task);
        }

        [TestMethod]
        public async Task EditToDoBadRequestTest()
        {
            var testId = 14357;
            var todoDto = new TodoDto
            {
                Id = 12093,
                Task = "Walk the dog"
            };
            var data = new Todo
            {
                Id = (int)todoDto.Id,
                Task = todoDto.Task
            };

            var mock = new Mock<iTodoService>();

            mock.Setup(x => x.EditTodo(todoDto)).ReturnsAsync(data);
            var controller = new TodoController(mock.Object);

            var result = await controller.EditTodo(testId, todoDto);
            var statusCode = (result as BadRequestResult).StatusCode;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, statusCode);
        }

                [TestMethod]
        public async Task DeleteTodoTest()
        {
            var testId = 12093;
        
            var todo = new Todo
            {
                Id = 12093,
                Task = "Walk the dog"
            };

            var mock = new Mock<iTodoService>();

            mock.Setup(x => x.DeleteTodo(12093)).ReturnsAsync(todo);
            var controller = new TodoController(mock.Object);

            var result = await controller.DeleteTodo(testId);
            var statusCode = (result as OkObjectResult).StatusCode;
            var returnedTodo = (result as OkObjectResult).Value as Todo;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, statusCode);
            Assert.AreEqual("Walk the dog", returnedTodo.Task);
        }
    }
}