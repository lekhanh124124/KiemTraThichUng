using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetCauHoiById
{
    public class GetCauHoiByIdQueryHandler : IRequestHandler<GetCauHoiByIdQuery, ApiResponse<CauHoiDto>>
    {
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        public GetCauHoiByIdQueryHandler(INganHangCauHoiRepository nganHangCauHoiRepository)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
        }
        public async Task<ApiResponse<CauHoiDto>> Handle(GetCauHoiByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _nganHangCauHoiRepository.GetCauHoiByIdAsync(request.Id, cancellationToken);
            if (result is null)
            {
                throw new NotFoundException(nameof(CauHoiDto), request.Id);
            }
            var mucDo = MucDoNangLuc.FromTheta((double)result.DoKhoKhoiTao!);
            result.DoKhoKhoiTao = mucDo.Id;
            result.CauHois!.ForEach(cauHoi =>
            {
                var mucDoCauHoi = MucDoNangLuc.FromTheta((double)cauHoi.DoKhoKhoiTao!);
                cauHoi.DoKhoKhoiTao = mucDoCauHoi.Id;
            });


            return ApiResponse<CauHoiDto>.Success(result!);
        }
    }
}
