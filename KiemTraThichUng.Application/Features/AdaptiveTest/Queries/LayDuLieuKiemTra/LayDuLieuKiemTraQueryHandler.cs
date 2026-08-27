using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDuLieuKiemTra
{
    public class LayDuLieuKiemTraQueryHandler : IRequestHandler<LayDuLieuKiemTraQuery, ApiResponse<DuLieuKiemTraDto>>
    {
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;
        public LayDuLieuKiemTraQueryHandler(
            IPhienKiemTraRepository phienKiemTraRepository,
            ICurrentUserService currentUserService)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<DuLieuKiemTraDto>> Handle(LayDuLieuKiemTraQuery request, CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;
            var phienKiemTra = await _phienKiemTraRepository.LayPhienKiemTraByIdKetQuaKiemTraAsync(idNguoiDung, request, cancellationToken);

            if (phienKiemTra == null)
            {
                //throw new NotFoundException(nameof(phienKiemTra), request.IdKetQuaKiemTra);
                throw new ValidationException(["Người dùng chưa kết thúc bài kiểm tra, vui lòng hoàn thành bài kiểm tra trước khi xem kết quả."]);
            }

            return ApiResponse<DuLieuKiemTraDto>.Success(phienKiemTra);
        }
    }
}
