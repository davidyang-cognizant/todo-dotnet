using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TodoService : iTodoService
    {

        private readonly DataContext _context;

        public TodoService(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<List<Todo>> GetList()
        {
            return await _context.Todo.ToListAsync();
        }

        public virtual async Task<Todo> AddTodo(Todo todo)
        {
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public virtual async Task<Todo> GetTodoById(int id)
        {
            return await _context.Todo.FindAsync(id);
        }

        public virtual async Task<Todo> EditTodo(TodoDto todo)
        {
            var updateTodo = await _context.Todo.FindAsync(todo.Id);
            updateTodo.Task = todo.Task;
            await _context.SaveChangesAsync();
           
            return updateTodo;
        }

        public virtual async Task<Todo> DeleteTodo(int id)
        {
            var deleteTodo = await _context.Todo.FindAsync(id);
            _context.Todo.Remove(deleteTodo);
            await _context.SaveChangesAsync();
            return deleteTodo;;
        }
    }
}