using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.ValueObjects;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.LayCauHoiTiepTheo
{
    public class LayCauHoiTiepTheoResponse
    {
        public bool IsFinished { get; set; }
        public StopReason? Reason { get; set; }
        public CauHoiMucTieuDto? CauHoi { get; set; }
    }
}
