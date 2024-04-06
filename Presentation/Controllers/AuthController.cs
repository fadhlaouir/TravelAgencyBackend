using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBackend.Domain.Entities;
using TravelAgencyBackend.Presentation.Models;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<LoginModel> _validator;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IValidator<LoginModel> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _validator = validator; 

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
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
                    PostalCode = model.PostalCode
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
               return BadRequest(validationResult.Errors);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
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
