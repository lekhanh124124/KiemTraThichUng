using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Domain.Enums;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateTrangThaiCauHoi
{
    public class CapNhatTrangThaiCauHoiCommand : ICommand<IReadOnlyList<CapNhatTrangThaiCauHoiResponse>>
    {
        public IReadOnlyList<int> Ids { get; set; } = new List<int>();
        public TrangThaiDuyet IdTrangThai { get; set; }
        public string? GhiChuDuyet { get; set; }
    }
}
