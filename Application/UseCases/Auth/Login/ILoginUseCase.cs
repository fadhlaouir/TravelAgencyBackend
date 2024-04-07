using System.Threading.Tasks;

namespace TravelAgencyBackend.Application.UseCases.Auth.Login
{
    public interface ILoginUseCase
    {
        Task<LoginResponse> Execute(LoginRequest request);
    }
}
