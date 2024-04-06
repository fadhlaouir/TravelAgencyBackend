using TravelAgencyBackend.Domain.Entities;

namespace TravelAgencyBackend.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        
    }
}
