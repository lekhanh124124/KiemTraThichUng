// File: KiemTraThichUng.Application/Common/Exceptions/AppException.cs
namespace KiemTraThichUng.Application.Common.Exceptions
{
    public abstract class AppException : Exception
    {
        public List<string> Errors { get; }

        protected AppException(string message)
            : base(message)
        {
            Errors = new List<string> { message };
        }

        protected AppException(IEnumerable<string> errors)
            : base("Application exception")
        {
            Errors = errors.ToList();
        }
    }
}
