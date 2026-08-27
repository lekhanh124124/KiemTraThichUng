// File: KiemTraThichUng.Application/Features/EX_CauHois/Queries/GetListCauHoi/GetListCauHoiQuery.cs
using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Domain.Enums;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetListCauHoi
{
    public class GetListCauHoiQuery : IQuery<PagedResult<CauHoiItemDto>>
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
        public string? Keyword { get; set; }
        public string? SortCol { get; set; } = "id";
        public bool? IsAsc { get; set; } = false;
        public string? MaCauHoi { get; set; }
        public bool? IsVisible { get; set; }
        public DateTime? TuNgayTao { get; set; }
        public DateTime? DenNgayTao { get; set; }
        public IReadOnlyList<int>? IdMucDoNhanThuc { get; set; }
        public TrangThaiDuyet? IdTrangThaiCauHoi { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public int? IdCauTruc { get; set; }
        public int? IdBoCauHoi { get; set; }
        public int? IdNguoiSoan { get; set; }
        public int? IdNhanSu { get; set; }
    }
}
