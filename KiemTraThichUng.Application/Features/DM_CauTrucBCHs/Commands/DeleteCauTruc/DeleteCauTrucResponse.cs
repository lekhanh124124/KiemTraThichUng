using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.DTOs;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.DeleteCauTruc
{
    public class DeleteCauTrucResponse
    {
        public IReadOnlyList<CauTrucItemResponse>? data { get; set; }
    }
}
