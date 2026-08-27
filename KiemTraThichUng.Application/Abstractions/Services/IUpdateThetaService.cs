using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;

namespace KiemTraThichUng.Application.Abstractions.Services
{
    public interface IUpdateThetaService
    {
        void UpdateThetaWithMAP(
            List<KetQuaDanhGiaDapAn> ketQuaDaLam,
            List<KetQuaDanhGiaDapAn> evaluationResults,
            double thetaMax,
            double thetaMin,
            double thetaInitial,
            double priorMean,
            double priorVariance,
            double standardErrorInitial);
    }
}
