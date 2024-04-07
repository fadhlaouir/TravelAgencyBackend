using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAgencyBackend.Application.Common;
using TravelAgencyBackend.Domain.Entities;

namespace TravelAgencyBackend.Application.UseCases.Auth.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public LoginUseCase(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
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
                // Generate JWT token
                var token = GenerateJwtToken(user);

                // Sign-in successful, return success response with token
                return new LoginResponse { Success = true, Message = "Login successful.", Token = token };
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


            // If all validations pass, return success response
            return new LoginResponse { Success = true };
        }

        // Method to generate JWT token
        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id), // Use UserId instead of email for better security
            new Claim(ClaimTypes.Email, user.Email), // Include email as a claim
                                                     // Add additional claims if needed
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}