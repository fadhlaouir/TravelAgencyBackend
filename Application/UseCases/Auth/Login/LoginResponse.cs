namespace TravelAgencyBackend.Application.UseCases.Auth.Login
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public string Token { get; set; }
    }
}
