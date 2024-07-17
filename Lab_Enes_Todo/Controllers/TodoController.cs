using Lab_Enes_Todo.DB;
using Lab_Enes_Todo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using System.Linq;

namespace Lab_Enes_Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DBConnect _context;

        public TodoController(DBConnect context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTodos()
        {
            var todos = _context.todos.ToList();
            if (todos.Count == 0)
            {
                return NotFound("No Todos Exist");
            }
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoById(int id)
        {
            var todo = _context.todos.Find(id);
            if (todo == null)
            {
                return NotFound($"Todo with {id} is not found");
            }
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
            return Ok("todo created");
        }

        [HttpPut]
        public IActionResult EditTodo(Todo newTodo)
        {
            if (newTodo == null)
            {
                return BadRequest("todo data is invalid");
            }

            var todo = _context.todos.Find(newTodo.Id);
            if (todo == null)
            {
                return NotFound("todo not found");
            }
            todo.Description = newTodo.Description;
            todo.Title = newTodo.Title;
            todo.IsCompleted = newTodo.IsCompleted;
            _context.SaveChanges();
            return Ok("todo updated");
        }

        [HttpDelete]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _context.todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.todos.Remove(todo);
            _context.SaveChanges();
            return Ok("todo deleted");
        }
    }
}
