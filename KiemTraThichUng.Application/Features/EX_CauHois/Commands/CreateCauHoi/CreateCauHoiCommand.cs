// File: KiemTraThichUng.Application/Features/EX_CauHois/Commands/CreateCauHoi/CreateCauHoiCommand.cs
using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.CreateCauHoi
{
    public class CreateCauHoiCommand : CauHoiDto, ICommand<CreateCauHoiResponse>
    {
    }
}
