using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.NopCauTraLoi
{
    public class NopCauTraLoiResponse
    {
        public double? ThetaBefore { get; set; }
        public double? ThetaAfter { get; set; }
        public double? StandardError { get; set; }
        public List<KetQuaDanhGiaDapAn> AnswerResults { get; set; } = new();
    }
}
