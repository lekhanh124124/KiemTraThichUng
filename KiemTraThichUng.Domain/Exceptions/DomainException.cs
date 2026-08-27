// File: KiemTraThichUng.Domain/Exceptions/DomainException.cs
namespace KiemTraThichUng.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}
