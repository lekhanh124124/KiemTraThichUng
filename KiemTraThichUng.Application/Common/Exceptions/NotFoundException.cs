// File: KiemTraThichUng.Application/Common/Exceptions/NotFoundException.cs
namespace KiemTraThichUng.Application.Common.Exceptions
{
    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string entity, object key)
            : base($"{entity} ({key}) was not found.")
        {
        }
    }
}
