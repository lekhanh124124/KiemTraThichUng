using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using MediatR;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.DeleteCauHoi
{
    public class DeleteCauHoiCommandHandler : IRequestHandler<DeleteCauHoiCommand, ApiResponse<IReadOnlyList<DeleteCauHoiResponse>>>
    {
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCauHoiCommandHandler(INganHangCauHoiRepository nganHangCauHoiRepository, IUnitOfWork unitOfWork)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IReadOnlyList<DeleteCauHoiResponse>>> Handle(
            DeleteCauHoiCommand request, 
            CancellationToken cancellationToken)
        {
            var result = await _nganHangCauHoiRepository.DeleteCauHoiByIdsAsync(request.Ids, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<IReadOnlyList<DeleteCauHoiResponse>>.Success(
                (IReadOnlyList<DeleteCauHoiResponse>)result.Select(r => new DeleteCauHoiResponse
                {
                    Id = r?.Id,
                    MaCauHoi = r?.MaCauHoi,
                    IdCauTruc = r?.IdCauTruc,
                    IdLoaiCauHoi = r?.IdLoaiCauHoi,
                    IdCauHoiCha = r?.IdCauHoiCha,
                    IdMucDoNhanThuc = r?.IdMucDoNhanThuc,
                    IdTrangThai = (int?)r?.TrangThai,
                    MediaUrl = r?.MediaUrl,
                    IdNguoiDuyet = r?.IdNguoiDuyet,
                    IdNguoiSoan = r?.IdNguoiTao,
                    IsKhongDao = r?.IsKhongDao,
                    IsCauHoiCha = r?.IsCauHoiCha,
                    TieuDeVeTrai = r?.TieuDeVeTrai,
                    TieuDeVePhai = r?.TieuDeVePhai,
                    Stt = r?.Stt,
                    SttCauHoiCon = null,
                    DoKho = r?.DoKho,
                    DoKhoKhoiTao = r?.DoKhoKhoiTao,
                    CauHoiGuid = r?.CauHoiGuid,
                    GhiChu = r?.GiaiThich,
                    GhiChuDuyet = r?.GhiChuDuyet,
                    NgaySoan = r?.NgayTao,
                    NgayDuyet = r?.NgayDuyet,
                    IsVisible = r?.IsVisible
                }));
        }
    }
}
