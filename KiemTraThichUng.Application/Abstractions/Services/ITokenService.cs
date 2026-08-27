namespace KiemTraThichUng.Application.Abstractions.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(
            int userId,
            string username,
            IEnumerable<string>? roles = null);
    }
}
