// File: KiemTraThichUng.Application/Abstractions/Services/IDanhGiaDapAnService.cs
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;

namespace KiemTraThichUng.Application.Abstractions.Services
{
    public interface IAnswerEvaluator
    {
        LoaiCauHoi LoaiCauHoi { get; }

        void Evaluate(
            IReadOnlyCollection<CauTraLoi> dapAnDung,
            List<KetQuaDanhGiaDapAn> ketQuaDanhGia);
    }
}
