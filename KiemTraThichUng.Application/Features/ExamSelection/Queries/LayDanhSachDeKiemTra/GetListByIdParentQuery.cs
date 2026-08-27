using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.ExamSelection.DTOs;

namespace KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDanhSachDeKiemTra
{
    public class GetListByIdParentQuery : IQuery<PagedResult<DeKiemTraitemDto>>
    {
        public int? IdBoCauHoi { get; set; }
        public int? IdCauTruc { get; set; }
        public string? Keyword { get; set; } = string.Empty;
        public string? SortCol { get; set; } = string.Empty;
        public bool? IsAsc { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
