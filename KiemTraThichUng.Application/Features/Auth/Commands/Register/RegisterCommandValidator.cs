// Application/Features/Auth/Register/RegisterCommandValidator.cs
using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IAuthService _authService;
    public RegisterCommandValidator(IAuthService authService)
    {
        _authService = authService;
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}