using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DataControls.DTOs;
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetLoaiCauHoiForSelector
{
    public class GetLoaiCauHoiForSelectorQueryHandler
        : IRequestHandler<GetLoaiCauHoiForSelectorQuery, ApiResponse<IReadOnlyList<SelectorItemResponse>>>
    {
        public async Task<ApiResponse<IReadOnlyList<SelectorItemResponse>>> Handle(
            GetLoaiCauHoiForSelectorQuery request, 
            CancellationToken cancellationToken)
        {
            var items = Enumeration.GetAll<LoaiCauHoi>()
                .Select(x => new SelectorItemResponse
                {
                    Id = x.Id,
                    Stt = x.Id,
                    Ma = x.Code,
                    Ten = x.Name
                })
                .OrderBy(x => x.Stt)
                .ToList();

            return ApiResponse<IReadOnlyList<SelectorItemResponse>>.Success(items);
        }
    }
}
