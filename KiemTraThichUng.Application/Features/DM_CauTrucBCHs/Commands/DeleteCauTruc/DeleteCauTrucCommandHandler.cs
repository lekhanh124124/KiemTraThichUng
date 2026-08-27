using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.DeleteCauTruc
{
    public class DeleteCauTrucCommandHandler : IRequestHandler<DeleteCauTrucCommand, ApiResponse<DeleteCauTrucResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public DeleteCauTrucCommandHandler(
            IUnitOfWork unitOfWork, 
            ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _unitOfWork = unitOfWork;
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
        }

        public async Task<ApiResponse<DeleteCauTrucResponse>> Handle(
            DeleteCauTrucCommand request, 
            CancellationToken cancellationToken)
        {
            var result = await _cauHinhDanhMucRepository.DeleteCauTrucByIdsAsync(request.Ids, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<DeleteCauTrucResponse>.Success(new DeleteCauTrucResponse
            {
                data = result.Select(x => new CauTrucItemResponse
                {
                    Id = x.Id,
                    IdBoCauHoi = x.IdBoCauHoi,
                    IdParent = x.IdParent,
                    TenCauTruc = x.TenCauTruc!,
                    MaCauTruc = x.MaCauTruc,
                    GhiChu = x.GhiChu,
                    Stt = x.Stt,
                    IsVisible = x.IsVisible
                }).ToList()
            });
        }
    }
}
