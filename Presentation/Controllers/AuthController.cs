using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBackend.Domain.Entities;
using TravelAgencyBackend.Presentation.Models;
using FluentValidation;

namespace TravelAgencyBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<LoginModel> _loginValidator;
        private readonly IValidator<RegisterModel> _registerValidator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IValidator<LoginModel> loginValidator,
            IValidator<RegisterModel> registerValidator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // Validate the input model
            var validationResult = await _registerValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Check if the email is null or empty
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new { Message = "Email cannot be null or empty." });
            }

            // Check if a user with the same email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                // User with the same email already exists
                return BadRequest(new { Message = "A user with the provided email already exists." });
            }

            // Create a new ApplicationUser instance
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PostalCode = model.PostalCode,
                PassportNumber = model.PassportNumber,
                PreferredLanguage = model.PreferredLanguage
            };

            // Attempt to create the new user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Registration successful." });
            }
            else
            {
                // Handle other registration errors
                var errorMessages = result.Errors.Select(e => e.Description);
                return BadRequest(errorMessages);
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var validationResult = await _loginValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Check if email or password is null
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new { Message = "Email or password cannot be null or empty." });
            }

            // Attempt to find user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            // Attempt to sign in
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false
            );

            if (result.Succeeded)
            {
                return Ok(new { Message = "Login successful." });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
        }



        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
