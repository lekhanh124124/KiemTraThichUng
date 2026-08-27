using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using MediatR;

namespace KiemTraThichUng.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<int>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return ApiResponse<int>.Success(await _authService.RegisterAsync(
                request.Username,
                request.Email,
                request.Password));
        }
    }
}
