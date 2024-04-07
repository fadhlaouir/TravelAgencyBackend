
namespace TravelAgencyBackend.Application.UseCases.Auth.Register
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public Enum Errors { get; internal set; }
    }
}
