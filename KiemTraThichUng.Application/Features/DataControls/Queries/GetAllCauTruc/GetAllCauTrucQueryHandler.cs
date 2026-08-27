using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DataControls.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetAllCauTruc
{
    public class GetAllCauTrucQueryHandler : IRequestHandler<GetAllCauTrucQuery, ApiResponse<IReadOnlyList<CauTrucItemResponse>>>
    {
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public GetAllCauTrucQueryHandler(ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
        }
        public async Task<ApiResponse<IReadOnlyList<CauTrucItemResponse>>> Handle(
            GetAllCauTrucQuery request, 
            CancellationToken cancellationToken)
        {
            var (danhSachCauTruc, totalCount) = await _cauHinhDanhMucRepository.GetAllCauTrucAsync(
                request.IdBoCauHoi,
                request.IdParent,
                request.Keyword,
                request.SortCol,
                request.IsAsc,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            return ApiResponse<IReadOnlyList<CauTrucItemResponse>>
                .Success(danhSachCauTruc.Select(x => new CauTrucItemResponse
                {
                    Id = x.Id,
                    IdBoCauHoi = x.IdBoCauHoi,
                    IdParent = x.IdParent,
                    MaCauTruc = x.MaCauTruc,
                    TenCauTruc = x.TenCauTruc!,
                    GhiChu = x.GhiChu!,
                    Stt = x.Stt,
                    IsVisible = x.IsVisible,
                }).ToList());
        }
    }
}
