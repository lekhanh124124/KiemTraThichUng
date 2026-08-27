// File: KiemTraThichUng.Application/Features/Auth/Queries/Login/LoginQueryHandler.cs
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using MediatR;

namespace KiemTraThichUng.Application.Features.Auth.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ApiResponse<LoginResponse>>
    {
        private readonly IAuthService _authService;

        public LoginQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return ApiResponse<LoginResponse>.Success(await _authService.LoginAsync(request.Username, request.Password));
        }
    }
}
