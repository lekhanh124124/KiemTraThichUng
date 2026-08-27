using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.DataControls.DTOs;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetMucDoNhanThucForSelector
{
    public class GetMucDoNhanThucForSelectorQuery : IQuery<IReadOnlyList<SelectorItemResponse>>
    {
        public bool IsVisible { get; set; }
    }
}
