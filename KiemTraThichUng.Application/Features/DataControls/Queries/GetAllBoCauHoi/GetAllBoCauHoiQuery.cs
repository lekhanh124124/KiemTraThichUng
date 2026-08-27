using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.DataControls.DTOs;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetAllBoCauHoi
{
    public class GetAllBoCauHoiQuery : IQuery<IReadOnlyList<BoCauHoiItemResponse>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? IdParent { get; set; }
        public string? Keyword { get; set; }
        public string? SortCol { get; set; }
        public bool? IsAsc { get; set; }
        public bool? IsVisible { get; set; }
    }
}
