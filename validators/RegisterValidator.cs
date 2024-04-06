using FluentValidation;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(model => model.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(model => model.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address format");
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(model => model.AddressLine1)
                .NotEmpty()
                .WithMessage("Address Line 1 is required");
            RuleFor(model => model.City).NotEmpty().WithMessage("City is required");
            RuleFor(model => model.State).NotEmpty().WithMessage("State is required");
            RuleFor(model => model.Country).NotEmpty().WithMessage("Country is required");
            RuleFor(model => model.PostalCode).NotEmpty().WithMessage("Postal Code is required");
            RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long");
        }
    }
}
