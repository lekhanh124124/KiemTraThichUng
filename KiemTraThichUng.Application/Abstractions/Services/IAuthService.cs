// Application/Abstractions/Services/IAuthService.cs
using KiemTraThichUng.Application.Features.Auth.Queries.Login;

namespace KiemTraThichUng.Application.Abstractions.Services;

public interface IAuthService
{
    Task<bool> IsUsernameExistAsync(string username);
    Task<bool> IsEmailExistAsync(string email);

    Task<int> RegisterAsync(string username, string email, string password);

    Task<LoginResponse> LoginAsync(string username, string password);
}