using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDuLieuKiemTra
{
    public class LayDuLieuKiemTraQuery : IQuery<DuLieuKiemTraDto>
    {
        public int? IdKetQuaKiemTra { get; set; }
    }
}
