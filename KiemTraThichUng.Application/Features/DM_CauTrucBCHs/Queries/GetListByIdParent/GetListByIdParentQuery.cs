// File: KiemTraThichUng.Application/Features/DM_CauTrucBCHs/Queries/GetListByIdParent/GetListByIdParentQuery.cs
using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.DTOs;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Queries.GetListByIdParent
{
    public class GetListByIdParentQuery : IQuery<PagedResult<CauTrucItemResponse>>
    {
        public int? IdBoCauHoi { get; set; }
        public int? IdParent { get; set; }
        public string? Keyword { get; set; } = string.Empty;
        public string? SortCol { get; set; } = string.Empty;
        public bool? IsAsc { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
