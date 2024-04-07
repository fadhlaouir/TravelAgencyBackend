namespace TravelAgencyBackend.Application.UseCases.Auth.Login
{
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
