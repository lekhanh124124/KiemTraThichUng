using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Infrastructure.Services.AnswerEvaluatorService
{

    public sealed class AnswerEvaluationService : IAnswerEvaluationService
    {
        private readonly Dictionary<int, IAnswerEvaluator> _evaluators;

        public AnswerEvaluationService(IEnumerable<IAnswerEvaluator> evaluators)
        {
            _evaluators = evaluators.ToDictionary(x => x.LoaiCauHoi.Id);
        }

        public void Evaluate(
            int loaiCauHoiId,
            IReadOnlyCollection<CauTraLoi> dapAnDung,
            List<KetQuaDanhGiaDapAn> ketQuaDanhGia)
        {
            if (!_evaluators.TryGetValue(loaiCauHoiId, out var evaluator))
                throw new DomainValidationException(
                    $"Loại câu hỏi '{loaiCauHoiId}' chưa hỗ trợ cách đánh giá");

            evaluator.Evaluate(dapAnDung, ketQuaDanhGia);
        }
    }
}
