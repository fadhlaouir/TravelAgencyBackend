using System.Threading.Tasks;

namespace TravelAgencyBackend.Application.UseCases.Auth.Register
{
    public interface IRegisterUseCase
    {
        Task<RegisterResponse> Execute(RegisterRequest request);
    }
}
