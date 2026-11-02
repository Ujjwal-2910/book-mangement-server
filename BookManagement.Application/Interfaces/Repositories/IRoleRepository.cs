using BookManagement.Domain.Entities;

namespace BookManagement.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name);
    }
}
