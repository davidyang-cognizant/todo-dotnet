
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using API.Tests.TestResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace API.Tests
{
    [TestClass]
    public class TodoServiceTest
    {

        [TestMethod]
        public async Task GetAlltasks()
        {
            var data = new List<Todo>
            {
                new Todo {Task = "Walk the dog"},
                new Todo {Task = "Play games"},
                new Todo {Task = "Nap"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IAsyncEnumerable<Todo>>()
               .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
               .Returns(new TestDbAsyncEnumerator<Todo>(data.GetEnumerator()));

            mockSet.As<IQueryable<Todo>>()
           .Setup(m => m.Provider)
           .Returns(new TestDbAsyncQueryProvider<Todo>(data.Provider));

            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Todo).Returns(mockSet.Object);

            var service = new TodoService(mockContext.Object);
            var results = await service.GetList();

            Assert.AreEqual("Walk the dog", results[0].Task);
            Assert.AreEqual("Play games", results[1].Task);
            Assert.AreEqual("Nap", results[2].Task);
        }

        [TestMethod]
        public async Task GetTaskById()
        {
            var data = new Todo
            {
                Id = 12031,
                Task = "Walk the dog"
            };

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.Setup(t => t.FindAsync(It.IsAny<int>())).Returns(ValueTask.FromResult<Todo>(data));

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Todo).Returns(mockSet.Object);


            var service = new TodoService(mockContext.Object);
            var results = await service.GetTodoById(12031);

            Assert.AreEqual("Walk the dog", results.Task);
            Assert.AreEqual(12031, results.Id);
        }


        [TestMethod]
        public async Task AddTodo()
        {
            var todo = new Todo()
            {
                Task = "new task"
            };

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.Setup(m => m.AddAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Callback((Todo item, CancellationToken token) => { })
               .Returns((Todo item, CancellationToken toke) => ValueTask.FromResult((EntityEntry<Todo>)null));

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Todo).Returns(mockSet.Object);

            var service = new TodoService(mockContext.Object);
            var results = await service.AddTodo(todo);

            Assert.AreEqual("new task", results.Task);
        }

        [TestMethod]
        public async Task EditTodo()
        {
            var dto = new TodoDto
            {
                Id = 12031,
                Task = "Walks the dog"
            };

            var data = new Todo
            {
                Id = 12031,
                Task = "Walk the dog"
            };

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.Setup(t => t.FindAsync(It.IsAny<int>())).Returns(ValueTask.FromResult<Todo>(data));

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Todo).Returns(mockSet.Object);


            var service = new TodoService(mockContext.Object);
            var results = await service.EditTodo(dto);

            Assert.AreEqual(data.Task, results.Task);
            Assert.AreEqual(data.Id, results.Id);
        }

                [TestMethod]
        public async Task DeleteTodo()
        {
            var data = new Todo
            {
                Id = 12031,
                Task = "Walk the dog"
            };

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.Setup(t => t.FindAsync(It.IsAny<int>())).Returns(ValueTask.FromResult<Todo>(data));
            mockSet.Setup(m => m.Remove(It.IsAny<Todo>())).Returns((EntityEntry<Todo>)null);

    

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Todo).Returns(mockSet.Object);


            var service = new TodoService(mockContext.Object);
            var results = await service.DeleteTodo(data.Id);

            Assert.AreEqual("Walk the dog", results.Task);
            Assert.AreEqual(12031, results.Id);
        }

    }
}