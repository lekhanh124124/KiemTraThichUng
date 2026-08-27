using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Commands.DeleteCauHinhDeKiemTra
{
    public class DeleteCauHinhDeKiemTraCommand : IRequest<ApiResponse<bool>>
    {
        public List<int> Ids { get; set; } = new();
    }

    public class DeleteCauHinhDeKiemTraCommandHandler : IRequestHandler<DeleteCauHinhDeKiemTraCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICauHinhDeKiemTraRepository _repository;

        public DeleteCauHinhDeKiemTraCommandHandler(IUnitOfWork unitOfWork, ICauHinhDeKiemTraRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteCauHinhDeKiemTraCommand request, CancellationToken cancellationToken)
        {
            if (request.Ids == null || !request.Ids.Any())
            {
                return ApiResponse<bool>.Failure("Danh sách ID không được để trống.");
            }

            var entities = await _repository.DeleteCauHinhDeKiemTraByIdsAsync(request.Ids, cancellationToken);
            
            // Also soft delete details for these configurations
            foreach (var entity in entities)
            {
                await _repository.ClearChiTietCauHinhDeKiemTraAsync(entity.Id, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.Success(true);
        }
    }
}
