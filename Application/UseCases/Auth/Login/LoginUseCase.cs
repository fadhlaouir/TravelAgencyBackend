using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelAgencyBackend.Domain.Entities;

namespace TravelAgencyBackend.Application.UseCases.Auth.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginUseCase(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResponse> Execute(LoginRequest request)
        {
            // Validate the login request
            var validationResult = ValidateLoginRequest(request);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // User not found, return error response
                return new LoginResponse { Success = false, Message = "User not found" };
            }

            // Attempt to sign in the user
            var result = await _signInManager.PasswordSignInAsync(user.Email, request.Password, false, false);
            if (result.Succeeded)
            {
                // Sign-in successful, return success response
                return new LoginResponse { Success = true, Message = "Login successful." };
            }
            else
            {
                // Sign-in failed, return error response
                return new LoginResponse { Success = false, Message = "Invalid email or password." };
            }
        }

        // Additional method to validate the login request
        private LoginResponse ValidateLoginRequest(LoginRequest request)
        {
            // Validate email and password presence
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse { Success = false, Message = "Email and password are required." };
            }

            // Additional validation logic can be added here

            // If all validations pass, return success response
            return new LoginResponse { Success = true };
        }
    }
}
