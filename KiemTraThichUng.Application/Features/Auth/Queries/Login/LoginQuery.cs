// File: KiemTraThichUng.Application/Features/Auth/Queries/Login/LoginQuery.cs
using KiemTraThichUng.Application.Abstractions.Messaging;

namespace KiemTraThichUng.Application.Features.Auth.Queries.Login
{
    public sealed record LoginQuery(
        string Username,
        string Password
    ) : ICommand<LoginResponse>;
}
