using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.ExamSelection.DTOs;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDanhSachDeKiemTra
{
    public class GetListByIdParentQueryHandler : IRequestHandler<GetListByIdParentQuery, ApiResponse<PagedResult<DeKiemTraitemDto>>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICauHinhDeKiemTraRepository _cauHinhDeKiemTraRepository;
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;

        public GetListByIdParentQueryHandler(
            ICurrentUserService currentUserService,
            ICauHinhDeKiemTraRepository cauHinhDeKiemTraRepository, 
            IPhienKiemTraRepository phienKiemTraRepository)
        {
            _currentUserService = currentUserService;
            _cauHinhDeKiemTraRepository = cauHinhDeKiemTraRepository;
            _phienKiemTraRepository = phienKiemTraRepository;
        }

        public async Task<ApiResponse<PagedResult<DeKiemTraitemDto>>> Handle(GetListByIdParentQuery request, CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;

            var (results, totalCount) = await _cauHinhDeKiemTraRepository.LayDanhSachCauHinhDeKiemTraByIdCauTrucAsync(request, cancellationToken);

            var (isHoanThanh, cauHinh) = await _phienKiemTraRepository.KiemTraDaHoanThanhAsync(idNguoiDung, cancellationToken);

            var chuaHoanThanh = !isHoanThanh;

            var idCauHinhChuaHoanThanh = cauHinh?.Id;

            var response = new PagedResult<DeKiemTraitemDto>
            {
                Items = results.Select(x => new DeKiemTraitemDto
                {
                    Id = x.Id,
                    IdCauTruc = x.IdCauTruc,
                    TenCauHinhDeKiemTra = x.TenCauHinhDeKiemTra,
                    MaCauHinhDeKiemTra = x.MaCauHinhDeKiemTra,
                    ThoiGianLamBaiGiay = x.ThoiGianLamBaiGiay,
                    DoKhoMin = MucDoNangLuc.FromTheta(x.ThetaMin).Name,
                    DoKhoMax = MucDoNangLuc.FromTheta(x.ThetaMax).Name,
                    MucNangLucDat = MucDoNangLuc.FromTheta(x.ThetaDat).Name,
                    Stt = x.Stt,
                    NgayDuyet = x.NgayDuyet,
                    IsDangLam = chuaHoanThanh && idCauHinhChuaHoanThanh == x.Id
                }),
                PagingInfo = new PagingInfo
                {
                    PageNumber = request.PageNumber ?? 1,
                    PageSize = request.PageSize ?? 10,
                    TotalItems = totalCount
                }
            };

            return ApiResponse<PagedResult<DeKiemTraitemDto>>.Success(response);
        }
    }
}
