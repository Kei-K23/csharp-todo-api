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

        public Task DeleteTodoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            try
            {
                var todos = await _context.Todos.ToListAsync();
                if (todos == null) {
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

        public Task<Todo> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoAsync(Guid id, CreateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}