// File: KiemTraThichUng.Application/Common/Responses/ApiResponse.cs
namespace KiemTraThichUng.Application.Common.Responses
{
    public sealed class ApiResponse<T>
    {
        public T? Result { get; }
        public IReadOnlyCollection<ApiMessage> WarningMessages => _warnings;
        public IReadOnlyCollection<ApiMessage> ErrorMessages => _errors;
        public bool IsOk => !_errors.Any();

        private readonly List<ApiMessage> _warnings = new();
        private readonly List<ApiMessage> _errors = new();

        private ApiResponse(T? result)
        {
            Result = result;
        }

        public static ApiResponse<T> Success(T result)
            => new(result);

        public static ApiResponse<T> Failure(IEnumerable<ApiMessage> errors)
        {
            var response = new ApiResponse<T>(default);
            response._errors.AddRange(errors);
            return response;
        }

        public static ApiResponse<T> Failure(string errorMessage, string code = "")
        {
            var response = new ApiResponse<T>(default);
            response._errors.Add(new ApiMessage(code, errorMessage));
            return response;
        }

        public ApiResponse<T> AddWarning(string message, string code = "")
        {
            if (!string.IsNullOrWhiteSpace(message))
                _warnings.Add(new ApiMessage(code, message));

            return this;
        }

        public ApiResponse<T> AddError(string message, string code = "")
        {
            if (!string.IsNullOrWhiteSpace(message))
                _errors.Add(new ApiMessage(code, message));

            return this;
        }
    }
}
