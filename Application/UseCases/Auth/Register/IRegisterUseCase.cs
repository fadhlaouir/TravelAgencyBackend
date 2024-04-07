using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelAgencyBackend.Domain.Entities;

namespace TravelAgencyBackend.Application.UseCases.Auth.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUseCase(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResponse> Execute(RegisterRequest request)
        {
            // Validate the registration request
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsValid)
            {
                return new RegisterResponse { Message = "Validation failed.", Errors = (Enum)validationResult.Errors };
            }

            // Check if the email is already in use
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new RegisterResponse { Message = "A user with the provided email already exists." };
            }

            // Create a new ApplicationUser instance
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode,
                PassportNumber = request.PassportNumber,
                PreferredLanguage = request.PreferredLanguage
            };

            // Attempt to create the new user
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new RegisterResponse { Message = "Registration successful." };
            }
            else
            {
                // Registration failed, return error response
                var errorMessages = result.Errors.Select(e => e.Description);
                return new RegisterResponse { Message = "Registration failed.", Errors = (Enum)errorMessages };
            }
        }

        private ValidationResult ValidateRequest(RegisterRequest request)
        {
            var errors = new List<string>();

            // Perform validation checks here
            if (string.IsNullOrEmpty(request.Email))
            {
                errors.Add("Email is required.");
            }
            // Add more validation checks as needed

            return new ValidationResult { IsValid = errors.Count == 0, Errors = errors };
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
