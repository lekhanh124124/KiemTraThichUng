// File: KiemTraThichUng.Application/Common/Exceptions/ValidationException.cs
namespace KiemTraThichUng.Application.Common.Exceptions
{
    public sealed class ValidationException : AppException
    {
        public ValidationException(IEnumerable<string> errors)
            : base(errors)
        {
        }
    }
}
