using System.Threading.Tasks;

namespace TravelAgencyBackend.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }
}
