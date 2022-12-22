using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly iTodoService _toDoService;

        public TodoController( iTodoService toDoService)
        {
            _toDoService = toDoService;
        }


        [HttpGet]
        public async Task<ActionResult> GetTodosAsync()
        {   var results = await _toDoService.GetList();
            // return await _context.Todo.ToListAsync();
            return Ok(results);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodoById(int id)
        {
            return Ok(await _toDoService.GetTodoById(id));
        }

        [HttpPost("add")] // POST: api/todo/add
        public async Task<ActionResult> AddTodo(TodoDto todo)
        {   
            var t = new Todo
            {
                Task = todo.Task
            };
            return Created(nameof(Todo), await _toDoService.AddTodo(t));
        }

        [HttpPut("edit/{id}")] // POST: api/todo/add
        public async Task<ActionResult> EditTodo(int id, TodoDto todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            return Created(nameof(Todo), await _toDoService.EditTodo(todo));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTodo(int id){
            return Ok(await _toDoService.DeleteTodo(id));
        }
    }
}