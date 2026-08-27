// File: KiemTraThichUng.Application/Common/Exceptions/ConflictException.cs
namespace KiemTraThichUng.Application.Common.Exceptions
{
    public sealed class ConflictException : AppException
    {
        public ConflictException(string message)
            : base(message)
        {
        }
    }
}
