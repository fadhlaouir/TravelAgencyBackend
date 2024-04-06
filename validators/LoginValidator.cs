using FluentValidation;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Should be a valid email address format");
            RuleFor(login => login.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(5)
                .WithMessage("Minimum Length 5");
        }
    }
}
