// File: KiemTraThichUng.Application/Common/Responses/ApiMessage.cs
namespace KiemTraThichUng.Application.Common.Responses
{
    public sealed record ApiMessage(
        string Code,
        string Message
    );
}
