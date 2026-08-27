using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.NganHangCauHoi;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.UpdateCautruc
{
    public class UpdateCauTrucCommandHandler 
        : IRequestHandler<UpdateCauTrucCommand, ApiResponse<UpdateCauTrucResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public UpdateCauTrucCommandHandler(
            IUnitOfWork unitOfWork, 
            ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _unitOfWork = unitOfWork;
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
        }
        public async Task<ApiResponse<UpdateCauTrucResponse>> Handle(
            UpdateCauTrucCommand request, 
            CancellationToken cancellationToken)
        {

            var entity = await _cauHinhDanhMucRepository.GetCauTrucByIdAsync(request.Id, cancellationToken);

            entity.CapNhatThongTin(
                tenCauTruc: request.TenCauTruc,
                maCauTruc: request.MaCauTruc,
                ghiChu: request.GhiChu,
                stt: request.Stt,
                isVisible: request.IsVisible);

            await _cauHinhDanhMucRepository.UpdateCauTrucAsync(entity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new UpdateCauTrucResponse
            {
                Id = entity.Id,
                TenCauTruc = entity.TenCauTruc!,
                MaCauTruc = entity.MaCauTruc,
                IdBoCauHoi = entity.IdBoCauHoi,
                IdParent = entity.IdParent,
                GhiChu = entity.GhiChu,
                Stt = entity.Stt,
                IsVisible = entity.IsVisible
            };
            return ApiResponse<UpdateCauTrucResponse>.Success(response);
        }
    }
}
