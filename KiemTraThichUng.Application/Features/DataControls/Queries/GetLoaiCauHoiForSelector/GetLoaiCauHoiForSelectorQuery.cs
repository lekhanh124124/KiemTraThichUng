// File: KiemTraThichUng.Application/Features/DataControls/Queries/GetLoaiCauHoiForSelector/GetLoaiCauHoiForSelectorQuery.cs
using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.DataControls.DTOs;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetLoaiCauHoiForSelector
{
    public class GetLoaiCauHoiForSelectorQuery : IQuery<IReadOnlyList<SelectorItemResponse>>
    {
        public bool IsVisible { get; set; }
    }
}
