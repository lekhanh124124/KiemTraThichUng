using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.Entities.CauHinhDanhMuc;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.CreateCautruc
{
    public class CreateCauTrucCommandHandler : IRequestHandler<CreateCauTrucCommand, ApiResponse<CreateCauTrucResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public CreateCauTrucCommandHandler(
            IUnitOfWork unitOfWork, 
            ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _unitOfWork = unitOfWork;
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
        }
        public async Task<ApiResponse<CreateCauTrucResponse>> Handle(
            CreateCauTrucCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = new CauTruc(
                request.MaCauTruc,
                request.IdBoCauHoi,
                request.IdParent,
                request.TenCauTruc,
                request.GhiChu,
                request.Stt,
                request.IsVisible);

            var result = await _cauHinhDanhMucRepository.CreateCauTrucAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<CreateCauTrucResponse>.Success(new CreateCauTrucResponse
            {
                Id = result.Id,
                MaCauTruc = result.MaCauTruc,
                TenCauTruc = result.TenCauTruc ?? string.Empty,
                IdBoCauHoi = result.IdBoCauHoi,
                IdParent = result.IdParent,
                GhiChu = result.GhiChu!,
                Stt = result.Stt,
                IsVisible = result.IsVisible
            });
        }
    }
}
