// File: KiemTraThichUng.Domain/Exceptions/DomainValidationException.cs
namespace KiemTraThichUng.Domain.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public DomainValidationException(string message)
            : base(message)
        {
        }
    }
}
