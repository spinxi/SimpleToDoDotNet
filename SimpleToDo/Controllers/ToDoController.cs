using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleToDo.Controllers.Data;
using SimpleToDo.Controllers.Entities;

namespace SimpleToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly DataContext _context;

        public ToDoController(DataContext context)
        {
            _context = context;
        }
        // Get All Todo
        [HttpGet]
        public async Task<ActionResult<List<ToDoModel>>> GetToDo()
        {
            var toDos = await _context.SimpleToDo
                            .Select(todo => new ToDoModel
                            {
                                Id = todo.Id,
                                Name = todo.Name,
                                Status = todo.Status,
                            })
                            .ToListAsync();

            return Ok(toDos);
        }
        //Get Current Todo From DB
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> GetToDoById(int id)
        {
            var toDos = await _context.SimpleToDo.FindAsync(id);
            // If can't be found, return NotFound error.
            if (toDos == null)
            {
                return NotFound("Current Todo Not Found");
            }
            var toDoModel = new ToDoModel
            {
                Id = toDos.Id,
                Name = toDos.Name,
                Status = toDos.Status,
            };

            return Ok(toDoModel);
        }

        // POST Data into DB, Created Separate Model that excludes Id.
        // We don't need Id while Posting data.
        [HttpPost] 
        public async Task<ActionResult<List<ToDoModel>>> AddTodo(TodoModelDTO todoDTO)
        {
            //Check if Name is not empty.
            if (string.IsNullOrWhiteSpace(todoDTO.Name))
            {
                return BadRequest("Name field is required.");
            }

            var todo = new ToDoModel
            {
                Name = todoDTO.Name,
                Status = todoDTO.Status,
                CreateDate = DateTime.Now,
            };

            _context.SimpleToDo.Add(todo);
            await _context.SaveChangesAsync();


            return Ok(todo);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoModel>> PutTodo(int id, TodoModelDTO todoDTO)
        {
            //Check if Name is not empty.
            if (string.IsNullOrWhiteSpace(todoDTO.Name))
            {
                return BadRequest("Name field is required.");
            }

            var todo = await _context.SimpleToDo.FindAsync(id);
            if(todo is null)
            {
                return NotFound("Data not found");
            }
 
            todo.Name = todoDTO.Name;
            todo.Status = todoDTO.Status;

            await _context.SaveChangesAsync();


            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
  
            var todo = await _context.SimpleToDo.FindAsync(id);
            if (todo is null)
            {
                return NotFound("Data not found");
            }

            _context.SimpleToDo.Remove(todo);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
