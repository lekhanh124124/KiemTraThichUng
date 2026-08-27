using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateCauHoi
{
    public class UpdateCauHoiCommand : CauHoiDto, ICommand<UpdateCauHoiResponse>
    {
    }
}
