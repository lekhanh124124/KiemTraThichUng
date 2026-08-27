using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;

namespace KiemTraThichUng.Infrastructure.Services.UpdateTheta
{
    public class UpdateThetaService : IUpdateThetaService
    {
        public void UpdateThetaWithMAP(
            List<KetQuaDanhGiaDapAn> ketQuaDaLam, 
            List<KetQuaDanhGiaDapAn> evaluationResults, 
            double thetaMax, 
            double thetaMin, 
            double thetaInitial, 
            double priorMean, 
            double priorVariance, 
            double standardErrorInitial)
        {
            double theta =
                ketQuaDaLam.LastOrDefault()?.ThetaAfter
                ?? thetaInitial;

            double cumulativeInformation =
                ketQuaDaLam.LastOrDefault()?.CumulativeInformationAfter
                ?? 0.0;

            double standardError =
                ketQuaDaLam.LastOrDefault()?.StandardErrorAfter
                ?? standardErrorInitial;

            double batchGradient = 0.0;
            double batchInformation = cumulativeInformation;

            foreach (var item in evaluationResults)
            {
                double b = item.CurrentDifficulty!.Value;

                // ===== BEFORE =====
                item.ThetaBefore = theta;
                item.StandardErrorBefore = standardError;
                item.CumulativeInformationBefore = cumulativeInformation;

                // ===== Likelihood =====
                double p = RaschProbability(theta, b);
                double u = item.IsCorrect == true
                    ? item.ScoreRatio!.Value
                    : 0.0;

                double itemInformation = ComputeItemInformation(p);
                item.CurrentItemInformation = itemInformation;

                // ===== Accumulate batch =====
                batchGradient += (u - p);
                batchInformation += itemInformation;

                double priorWeight = ComputePriorWeight(batchInformation);

                // ===== Batch Hybrid MAP → MLE =====
                double gradient =
                    batchGradient
                    - priorWeight * (theta - priorMean) / priorVariance;

                double denominator =
                    batchInformation
                    + priorWeight / priorVariance;

                double deltaTheta = gradient / denominator;

                theta += deltaTheta;
                theta = Clamp(theta, thetaMin, thetaMax);

                // ===== AFTER =====
                cumulativeInformation = batchInformation;

                standardError =
                    Math.Sqrt(
                        1.0 / Math.Max(cumulativeInformation, 1e-6)
                    );

                item.ThetaAfter = theta;

                item.TargetTheta = ComputeTargetTheta(
                    theta,
                    standardError,
                    item.IsCorrect == true,
                    item.ScoreRatio!.Value
                );

                item.StandardErrorAfter = standardError;
                item.CumulativeInformationAfter = cumulativeInformation;
            }
        }

        // =========================
        // ===== Helper methods =====
        // =========================

        // Xác suất Rasch 1PL
        private static double RaschProbability(double theta, double b)
        {
            return 1.0 / (1.0 + Math.Exp(-(theta - b)));
        }

        // Thông tin của 1 câu hỏi (1PL)
        private static double ComputeItemInformation(double p)
        {
            return p * (1.0 - p);
        }


        // Theta mục tiêu dùng để chọn câu hỏi tiếp theo
        private static double ComputeTargetTheta(
            double thetaAfter,
            double standardError,
            bool isCorrect,
            double scoreRatio)
        {
            double k = 0.5; // Hệ số điều chỉnh độ nhạy của việc chọn câu tiếp theo

            double direction =
                isCorrect
                    ? scoreRatio
                    : -1.0;

            double targetTheta =
                thetaAfter + k * standardError * direction;

            return targetTheta;
        }

        private static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private static double ComputePriorWeight(double cumulativeInformation)
        {
            // β điều chỉnh tốc độ chuyển MAP → MLE
            double beta = 0.4;

            return Math.Exp(-beta * cumulativeInformation);
        }
    }
}
