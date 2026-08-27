using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.Enums;
using MediatR;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateTrangThaiCauHoi
{
    public class CapNhatTrangThaiCauHoiCommandHandler : IRequestHandler<CapNhatTrangThaiCauHoiCommand, ApiResponse<IReadOnlyList<CapNhatTrangThaiCauHoiResponse>>>
    {
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CapNhatTrangThaiCauHoiCommandHandler(INganHangCauHoiRepository nganHangCauHoiRepository, IUnitOfWork unitOfWork)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IReadOnlyList<CapNhatTrangThaiCauHoiResponse>>> Handle(
            CapNhatTrangThaiCauHoiCommand request, 
            CancellationToken cancellationToken)
        {
            var cauHois = await _nganHangCauHoiRepository.GetCauHoiByIdsAsync(request.Ids, cancellationToken);

            var trangThaiMoi = request.IdTrangThai;

            int idNguoiDuyetMock = 1; 

            foreach (var cauHoi in cauHois)
            {
                switch (trangThaiMoi)
                {
                    case TrangThaiDuyet.DeXuatDuyet:
                        cauHoi.DeXuatDuyet();
                        break;

                    case TrangThaiDuyet.DaDuyet:
                        cauHoi.Duyet(idNguoiDuyetMock, DateTime.Now, request.GhiChuDuyet);
                        break;
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = cauHois.Select(c => new CapNhatTrangThaiCauHoiResponse
            {
                Id = c.Id,
                MaCauHoi = c.MaCauHoi,
                IdCauTruc = c.IdCauTruc,
                IdLoaiCauHoi = c.IdLoaiCauHoi,
                IdCauHoiCha = c.IdCauHoiCha,
                IdMucDoNhanThuc = c.IdMucDoNhanThuc,
                IdTrangThai = (int)c.TrangThai,
                MediaUrl = c.MediaUrl,
                IdNguoiDuyet = c.IdNguoiDuyet,
                IdNguoiSoan = c.IdNguoiTao,
                IsKhongDao = c.IsKhongDao,
                IsCauHoiCha = c.IsCauHoiCha,
                TieuDeVeTrai = c.TieuDeVeTrai,
                TieuDeVePhai = c.TieuDeVePhai,
                Stt = c.Stt,
                DoKho = c.DoKho,
                DoKhoKhoiTao = c.DoKhoKhoiTao,
                CauHoiGuid = c.CauHoiGuid,
                GhiChu = c.GiaiThich,
                GhiChuDuyet = c.GhiChuDuyet,
                NgaySoan = c.NgayTao,
                NgayDuyet = c.NgayDuyet,
                IsVisible = c.IsVisible
            }).ToList();

            return ApiResponse<IReadOnlyList<CapNhatTrangThaiCauHoiResponse>>
                .Success(response);
        }
    }
}
