using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DataControls.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.DataControls.Queries.GetAllBoCauHoi
{
    public class GetAllBoCauHoiQueryHandler : IRequestHandler<GetAllBoCauHoiQuery, ApiResponse<IReadOnlyList<BoCauHoiItemResponse>>>
    {
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public GetAllBoCauHoiQueryHandler(ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
        }
        public async Task<ApiResponse<IReadOnlyList<BoCauHoiItemResponse>>> Handle(GetAllBoCauHoiQuery request, CancellationToken cancellationToken)
        {
            var danhSachBoCauHoi = await _cauHinhDanhMucRepository.GetAllBoCauHoiAsync(
                request.IdParent, 
                request.Keyword, 
                request.IsVisible, 
                request.SortCol, 
                request.IsAsc, 
                request.PageNumber, 
                request.PageSize,
                cancellationToken);

            return ApiResponse<IReadOnlyList<BoCauHoiItemResponse>>
                .Success(danhSachBoCauHoi.Item1.Select(x => new BoCauHoiItemResponse
                {
                    Id = x.Id,
                    MaBoCauHoi = x.MaBoCauHoi,
                    TenBoCauHoi = x.TenBoCauHoi!,
                    TaiLieuThamKhao = x.TaiLieuThamKhao!,
                    GhiChu = x.GhiChu!,
                    IsLocked = x.IsLocked
                }).ToList());
        }
    }
}
