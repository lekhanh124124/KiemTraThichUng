// File: KiemTraThichUng.Infrastructure/Identity/AuthService.cs
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Features.Auth.Queries.Login;
using Microsoft.AspNetCore.Identity;

namespace KiemTraThichUng.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<bool> IsUsernameExistAsync(string username)
            => await _userManager.FindByNameAsync(username) != null;

        public async Task<bool> IsEmailExistAsync(string email)
            => await _userManager.FindByEmailAsync(email) != null;

        public async Task<int> RegisterAsync(string username, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => e.Description)
                    .ToList();

                throw new ValidationException(errors);
            }

            await _userManager.AddToRoleAsync(user, "ThiSinh");

            return user.Id;
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username)
                ?? throw new ValidationException(["Sai tài khoản."]);

            var isValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isValid)
                throw new ValidationException(["Sai mật khẩu."]);

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateAccessToken(
                user.Id,
                user.UserName!,
                roles);

            return new LoginResponse(token, roles);
        }
    }
}
