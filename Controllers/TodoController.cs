using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        private readonly ITodoService _todoService = todoService;

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest createTodoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _todoService.CreateTodoAsync(createTodoRequest);
                return Ok(new
                {
                    message = "Todo created successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating todo", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodosAsync()
        {
            try
            {
                var todos = await _todoService.GetAllAsync();
                if (todos == null || !todos.Any())
                {
                    return Ok(new { message = "No todos found" });
                }
                return Ok(new { message = "Successfully retrieved all the todos", todos = todos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving todos", error = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTodoByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoService.GetByIdAsync(id);
                if (todo == null)
                {
                    return Ok(new { message = "Todo not found with id: " + id });
                }
                return Ok(new { todo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting todo by id", error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest updateTodoRequest)
        {
            try
            {
                var todo = await _todoService.UpdateTodoAsync(id, updateTodoRequest);
                return Ok(new { message = "Successfully updated the todo", todo = todo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting todo by id", error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                await _todoService.DeleteTodoAsync(id);
                return Ok(new { message = "Successfully deleted the todo" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the todo", error = ex.Message });
            }
        }
    }
}