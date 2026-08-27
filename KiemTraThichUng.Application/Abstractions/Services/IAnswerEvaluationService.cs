using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;

namespace KiemTraThichUng.Application.Abstractions.Services
{
    public interface IAnswerEvaluationService
    {
        void Evaluate(
            int loaiCauHoiId,
            IReadOnlyCollection<CauTraLoi> dapAnDung,
            List<KetQuaDanhGiaDapAn> ketQuaDanhGia);
    }
}
