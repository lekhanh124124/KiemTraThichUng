// File: KiemTraThichUng.Application/Abstractions/Messaging/ICommand.cs
using KiemTraThichUng.Application.Common.Responses;
using MediatR;

namespace KiemTraThichUng.Application.Abstractions.Messaging
{
    public interface ICommand<TResponse>
        : IRequest<ApiResponse<TResponse>>
    {
    }
}
