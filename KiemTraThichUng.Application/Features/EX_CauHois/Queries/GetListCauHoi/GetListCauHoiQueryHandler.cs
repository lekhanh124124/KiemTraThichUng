using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetListCauHoi
{
    public class GetListCauHoiQueryHandler : IRequestHandler<GetListCauHoiQuery, ApiResponse<PagedResult<CauHoiItemDto>>>
    {
        private readonly INganHangCauHoiRepository _cauHoirepository;
        public GetListCauHoiQueryHandler(INganHangCauHoiRepository cauHoirepository)
        {
            _cauHoirepository = cauHoirepository;
        }
        public async Task<ApiResponse<PagedResult<CauHoiItemDto>>> Handle(
            GetListCauHoiQuery request, 
            CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _cauHoirepository.GetListCauHoiAsync(
                request,
                cancellationToken);

            var result = new PagedResult<CauHoiItemDto>
            {
                Items = items!,
                PagingInfo = new PagingInfo
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalItems = totalCount
                }
            };

            return ApiResponse<PagedResult<CauHoiItemDto>>.Success(result);
        }

    }
}
