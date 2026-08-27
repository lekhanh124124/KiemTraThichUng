using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Queries.GetListByIdParent
{
    public class GetListByIdParentQueryHandler : IRequestHandler<GetListByIdParentQuery, ApiResponse<PagedResult<CauTrucItemResponse>>>
    {
        private readonly ICauHinhDanhMucRepository _nganHangCauHoiRepository;
        public GetListByIdParentQueryHandler(ICauHinhDanhMucRepository nganHangCauHoiRepository)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
        }
        public async Task<ApiResponse<PagedResult<CauTrucItemResponse>>> Handle(
            GetListByIdParentQuery request, 
            CancellationToken cancellationToken)
        {
            var (danhSachCauTruc, totalCount) = await _nganHangCauHoiRepository.GetAllCauTrucAsync(
                request.IdBoCauHoi,
                request.IdParent,
                request.Keyword,
                request.SortCol,
                request.IsAsc,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var pagedResult = new PagedResult<CauTrucItemResponse>
            {
                Items = danhSachCauTruc.Select(x => new CauTrucItemResponse
                {
                    Id = x.Id,
                    TenCauTruc = x.TenCauTruc!,
                    MaCauTruc = x.MaCauTruc,
                    IdParent = x.IdParent,
                    IdBoCauHoi = x.IdBoCauHoi,
                    GhiChu = x.GhiChu,
                    Stt = x.Stt,
                    IsVisible = x.IsVisible
                }),
                PagingInfo = new PagingInfo
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalItems = totalCount
                }
            };

            return ApiResponse<PagedResult<CauTrucItemResponse>>.Success(pagedResult);
        }
    }
}
