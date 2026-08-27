using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.DataControls.DTOs;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetAllCauTruc
{
    public class GetAllCauTrucQuery : IQuery<IReadOnlyList<CauTrucItemResponse>>
    {
        public int? IdBoCauHoi { get; set; }
        public int? IdParent { get; set; }
        public string Keyword { get; set; } = string.Empty;
        public string SortCol { get; set; } = "stt";
        public bool IsAsc { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
