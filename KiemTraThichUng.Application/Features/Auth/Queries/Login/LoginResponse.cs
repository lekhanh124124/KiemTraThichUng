// File: KiemTraThichUng.Application/Features/Auth/Queries/Login/LoginResponse.cs
namespace KiemTraThichUng.Application.Features.Auth.Queries.Login
{
    public sealed record LoginResponse(
        string Token,
        IEnumerable<string> Roles
    );
}
