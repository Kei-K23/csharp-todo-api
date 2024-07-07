using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public class TodoService : ITodoService
    {

        private readonly TodoDbContext _context;
        private readonly ILogger<Todo> _logger;
        private readonly IMapper _mapper;

        public TodoService(TodoDbContext context, ILogger<Todo> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while creating a new todo");
                throw new Exception("An exception occurred while creating a new todo");
            }

        }

        public async Task DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    _logger.LogWarning($"No todo found with id: {id}");

                }

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting todo by id");

                throw new Exception("Error when getting todo by id");
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            try
            {
                var todos = await _context.Todos.ToListAsync();
                if (todos == null)
                {
                    _logger.LogWarning("No todos found in the database");
                    return [];
                }
                return todos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting todos lists");

                throw new Exception("Error when getting todos lists");
            }
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    _logger.LogWarning($"No todo found with id: {id}");
                    return null;
                }
                return todo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting todo by id");

                throw new Exception("Error when getting todo by id");
            }
        }

        public async Task<Todo> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);

                if (todo == null)
                {
                    _logger.LogWarning($"No todo found with id: {id}");
                    throw new Exception("No todo found to update");
                }

                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }

                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }

                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }

                if (request.DueDate != null)
                {
                    todo.DueDate = request.DueDate.Value;
                }

                if (request.Priority != null)
                {
                    todo.Priority = request.Priority;
                }
                todo.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return todo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating todo");
                throw new Exception("Error when updating todo by id");
            }
        }

    }
}