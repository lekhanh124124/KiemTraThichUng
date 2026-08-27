using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;

namespace KiemTraThichUng.Infrastructure.Services.AnswerEvaluatorService
{
    public class TracNghiemDonEvalutor : IAnswerEvaluator
    {
        private const double PassingThreshold = 0.5;

        public LoaiCauHoi LoaiCauHoi => LoaiCauHoi.TracNghiemDon;

        public void Evaluate(
            IReadOnlyCollection<CauTraLoi> dapAnDung,
            List<KetQuaDanhGiaDapAn> ketQuaDanhGia)
        {
            var correctByQuestion = dapAnDung
                .GroupBy(x => x.IdCauHoi)
                .ToDictionary(g => g.Key, g => (IReadOnlyCollection<CauTraLoi>)g.ToList());

            var userGroups = ketQuaDanhGia
                .Where(x => x.IdCauHoi.HasValue)
                .GroupBy(x => x.IdCauHoi!.Value);

            foreach (var group in userGroups)
            {
                if (!correctByQuestion.TryGetValue(group.Key, out var correctAnswers))
                    continue;

                EvaluateSingleQuestion(correctAnswers, group);
            }
        }

        private static void EvaluateSingleQuestion(
            IReadOnlyCollection<CauTraLoi> correctAnswers,
            IEnumerable<KetQuaDanhGiaDapAn> userAnswers)
        {
            var correctIds = new HashSet<int>();
            double totalScore = 0;
            bool isThietLapRieng = false;

            // Single pass qua correctAnswers
            foreach (var answer in correctAnswers)
            {
                if (!answer.IsDung)
                    continue;

                correctIds.Add(answer.Id);

                if (answer.IsThietLapRieng)
                {
                    isThietLapRieng = true;
                    totalScore += answer.PhanTramDiem ?? 0.0;
                }
            }

            var userIds = new HashSet<int>();
            foreach (var ua in userAnswers)
            {
                if (ua.IdCauTraLoi.HasValue)
                    userIds.Add(ua.IdCauTraLoi.Value);
            }

            double scoreRatio = 0.0;
            bool isCorrect = false;

            // RULE CỨNG: phải chọn đúng toàn bộ
            if (userIds.Count == correctIds.Count &&
                userIds.All(id => correctIds.Contains(id)))
            {
                if (!isThietLapRieng)
                {
                    scoreRatio = 1.0;
                    isCorrect = true;
                }
                else
                {
                    scoreRatio = Math.Min(totalScore, 1.0);
                    isCorrect = scoreRatio >= PassingThreshold;
                }
            }

            foreach (var item in userAnswers)
            {
                item.ScoreRatio = scoreRatio;
                item.IsCorrect = isCorrect;
            }
        }
    }
}
