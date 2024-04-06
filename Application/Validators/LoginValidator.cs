using FluentValidation;
using TravelAgencyBackend.Presentation.Models;

namespace TravelAgencyBackend.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address format");

            RuleFor(login => login.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(5)
                .WithMessage("Password must be at least 5 characters long");
        }
    }
}
