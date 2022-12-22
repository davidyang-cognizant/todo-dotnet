using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface iTodoService
    {
    Task<List<Todo>> GetList();

    Task<Todo> AddTodo(Todo todo);

    Task<Todo> GetTodoById(int id);

    Task<Todo> EditTodo(TodoDto todo);

    Task<Todo> DeleteTodo(int id);
        
    }
}