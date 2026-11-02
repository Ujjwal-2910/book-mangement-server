using BookManagement.Application.Interfaces.Repositories;
using BookManagement.Domain.Entities;
using BookManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(User user)
        {
            _context.Users.Add(user);
            return Task.CompletedTask;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
