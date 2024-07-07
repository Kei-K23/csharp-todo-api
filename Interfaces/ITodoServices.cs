using TodoAPI.Contracts;
using TodoAPI.Models;

namespace TodoAPI.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task CreateTodoAsync(CreateTodoRequest request);
        Task UpdateTodoAsync(Guid id, CreateTodoRequest request);
        Task DeleteTodoAsync(Guid id);
    }
}