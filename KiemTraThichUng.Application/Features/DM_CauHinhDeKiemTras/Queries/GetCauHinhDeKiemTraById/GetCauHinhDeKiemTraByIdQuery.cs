using AutoMapper;
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.DTOs;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Queries.GetCauHinhDeKiemTraById
{
    public class GetCauHinhDeKiemTraByIdQuery : IRequest<ApiResponse<CauHinhDeKiemTraDto>>
    {
        public int Id { get; set; }
    }

    public class GetCauHinhDeKiemTraByIdQueryHandler : IRequestHandler<GetCauHinhDeKiemTraByIdQuery, ApiResponse<CauHinhDeKiemTraDto>>
    {
        private readonly ICauHinhDeKiemTraRepository _repository;
        private readonly IMapper _mapper;

        public GetCauHinhDeKiemTraByIdQueryHandler(ICauHinhDeKiemTraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CauHinhDeKiemTraDto>> Handle(GetCauHinhDeKiemTraByIdQuery request, CancellationToken cancellationToken)
        {
            var (entity, details) = await _repository.GetByIdAdminAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CauHinhDeKiemTra), request.Id);
            }

            var dto = _mapper.Map<CauHinhDeKiemTraDto>(entity);
            if (details != null)
            {
                dto.ChiTietCauHinhDeKiemTras = _mapper.Map<List<ChiTietCauHinhDeKiemTraDto>>(details);
            }

            return ApiResponse<CauHinhDeKiemTraDto>.Success(dto);
        }
    }
}
