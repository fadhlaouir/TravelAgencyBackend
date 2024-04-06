using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelAgencyBackend.Application.Interfaces.Services;
using TravelAgencyBackend.Domain.Entities;

namespace TravelAgencyBackend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}
