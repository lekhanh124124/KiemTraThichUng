using KiemTraThichUng.Application.Abstractions.Messaging;

namespace KiemTraThichUng.Application.Features.Auth.Commands.Register
{
    public sealed record RegisterCommand(
        string Username,
        string Email,
        string Password
    ) : ICommand<int>;
}
