using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Application.Interfaces.Repositories;
using TravelAgencyBackend.Domain.Entities;

namespace TravelAgencyBackend.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Implement other repository methods as needed
    }
}
