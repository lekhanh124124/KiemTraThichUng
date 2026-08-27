using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDanhSachPhienKiemTra
{
    public class LayDanhSachPhienKiemTraQueryHandler : IRequestHandler<LayDanhSachPhienKiemTraQuery, ApiResponse<IReadOnlyList<PhienKiemTraItemDto>>>
    {
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;
        public LayDanhSachPhienKiemTraQueryHandler(
            IPhienKiemTraRepository phienKiemTraRepository, 
            ICurrentUserService currentUserService)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<IReadOnlyList<PhienKiemTraItemDto>>> Handle(LayDanhSachPhienKiemTraQuery request, CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;
            var danhSachPhienKiemTra = await _phienKiemTraRepository.LayDanhSachKetQuaKiemTraByIdNguoiDungAsync(idNguoiDung, cancellationToken);
            return ApiResponse<IReadOnlyList<PhienKiemTraItemDto>>.Success(danhSachPhienKiemTra);
        }
    }
}
