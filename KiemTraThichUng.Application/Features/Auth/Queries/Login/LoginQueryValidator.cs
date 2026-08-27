// File: KiemTraThichUng.Application/Features/Auth/Queries/Login/LoginQueryValidator.cs
using FluentValidation;

namespace KiemTraThichUng.Application.Features.Auth.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
