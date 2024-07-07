using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        private readonly ITodoService _todoService = todoService;

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest createTodoRequest) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try
            {
                await _todoService.CreateTodoAsync(createTodoRequest);
                return Ok(new {
                    message = "Todo created successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "An error occurred while creating todo", error = ex.Message});
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodosAsync()
        {
        try
        {
            var todos = await _todoService.GetAllAsync();
            if (todos == null || !todos.Any()) {
                return Ok(new {message = "No todos found"});
            }
            return Ok(new {message = "Successfully retrieved all the todos", todos = todos});
        }
        catch (Exception ex)
        {
          return StatusCode(500, new {message = "An error occurred while retrieving todos", error = ex.Message});
        }
    }
    }
}