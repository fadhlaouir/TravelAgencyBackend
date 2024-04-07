using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBackend.Application.UseCases.Auth.Login;
using TravelAgencyBackend.Application.UseCases.Auth.Register;
using TravelAgencyBackend.Presentation.Models;
using FluentValidation;

namespace TravelAgencyBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        private readonly IRegisterUseCase _registerUseCase;
        private readonly IValidator<LoginModel> _loginValidator;
        private readonly IValidator<RegisterModel> _registerValidator;

        public AuthController(
            ILoginUseCase loginUseCase,
            IRegisterUseCase registerUseCase,
            IValidator<LoginModel> loginValidator,
            IValidator<RegisterModel> registerValidator)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var validationResult = await _registerValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var registerRequest = new RegisterRequest
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
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

            var registerResponse = await _registerUseCase.Execute(registerRequest);

            if (registerResponse.Success)
            {
                return Ok(new { Message = registerResponse.Message });
            }
            else
            {
                return BadRequest(new { Message = registerResponse.Message });
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

            var loginRequest = new LoginRequest
            {
                Email = model.Email,
                Password = model.Password
            };

            var loginResponse = await _loginUseCase.Execute(new LoginRequest
            {
                Email = model.Email,
                Password = model.Password
            });

            if (loginResponse.Success)
            {
                return Ok(new { Message = loginResponse.Message, Token = loginResponse.Token });
            }
            else
            {
                return Unauthorized(new { Message = loginResponse.Message });
            }
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Add logout logic if needed
            return Ok();
        }
    }
}
