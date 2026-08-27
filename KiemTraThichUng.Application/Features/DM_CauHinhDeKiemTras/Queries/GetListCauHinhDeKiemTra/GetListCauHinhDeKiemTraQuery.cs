using AutoMapper;
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.DTOs;
using MediatR;

namespace KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Queries.GetListCauHinhDeKiemTra
{
    public class GetListCauHinhDeKiemTraQuery : IRequest<ApiResponse<PagedResult<CauHinhDeKiemTraDto>>>
    {
        public int? IdCauTruc { get; set; }
        public string? Keyword { get; set; }
        public string? SortCol { get; set; }
        public bool? IsAsc { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }

    public class GetListCauHinhDeKiemTraQueryHandler : IRequestHandler<GetListCauHinhDeKiemTraQuery, ApiResponse<PagedResult<CauHinhDeKiemTraDto>>>
    {
        private readonly ICauHinhDeKiemTraRepository _repository;
        private readonly IMapper _mapper;

        public GetListCauHinhDeKiemTraQueryHandler(ICauHinhDeKiemTraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResult<CauHinhDeKiemTraDto>>> Handle(GetListCauHinhDeKiemTraQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _repository.GetListAdminAsync(
                request.IdCauTruc,
                request.Keyword,
                request.SortCol,
                request.IsAsc,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var dtos = _mapper.Map<List<CauHinhDeKiemTraDto>>(items);

            return ApiResponse<PagedResult<CauHinhDeKiemTraDto>>.Success(new PagedResult<CauHinhDeKiemTraDto>
            {
                Items = dtos,
                PagingInfo = new PagingInfo
                {
                    TotalItems = totalCount,
                    PageNumber = request.PageNumber ?? 1,
                    PageSize = request.PageSize ?? 20
                }
            });
        }
    }
}
