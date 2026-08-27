using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.NopCauTraLoi
{
    public class NopCauTraLoiCommand : ICommand<NopCauTraLoiResponse>
    {
        public IReadOnlyList<CauTraLoisMucTieuDto> DapAnNguoiDung { get; set; } = new List<CauTraLoisMucTieuDto>();
    }
}
