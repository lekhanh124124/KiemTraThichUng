// File: KiemTraThichUng.Application/Features/EX_CauHois/Commands/CreateCauHoi/CreateCauHoiCommandHandler.cs
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.CreateCauHoi
{
    public class CreateCauHoiCommandHandler
        : IRequestHandler<CreateCauHoiCommand, ApiResponse<CreateCauHoiResponse>>
    {
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCauHoiCommandHandler(
            INganHangCauHoiRepository nganHangCauHoiRepository,
            IUnitOfWork unitOfWork)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<CreateCauHoiResponse>> Handle(
            CreateCauHoiCommand request,
            CancellationToken cancellationToken)
        {

            CauHoi? cauHoiCha = null;

            if (request.IsCauHoiCha == true)
            {
                var mucDo = MucDoNangLuc.FromId((int)request.DoKhoKhoiTao!);

                cauHoiCha = new CauHoi(
                    request.NoiDung,
                    request.TieuDeVeTrai,
                    request.TieuDeVePhai,
                    request.MediaUrl,
                    request.GhiChu,
                    request.IdCauTruc!.Value,
                    LoaiCauHoi.FromId(request.IdLoaiCauHoi!.Value),
                    request.IdMucDoNhanThuc.HasValue
                        ? MucDoNhanThuc.FromId(request.IdMucDoNhanThuc.Value)
                        : null,
                    true,
                    null,
                    request.IsKhongDao ?? false,
                    mucDo.MidTheta,
                    null,
                    request.Stt,
                    true);
            }

            var pairs = new List<(CauHoi, CauTraLoi)>();

            foreach (var dto in request.CauHois!)
            {
                var mucDo = MucDoNangLuc.FromId((int)dto.DoKhoKhoiTao!);

                var cauHoiCon = new CauHoi(
                    dto.NoiDung,
                    dto.TieuDeVeTrai,
                    dto.TieuDeVePhai,
                    dto.MediaUrl,
                    dto.GhiChu,
                    dto.IdCauTruc!.Value,
                    LoaiCauHoi.FromId(dto.IdLoaiCauHoi!.Value),
                    dto.IdMucDoNhanThuc.HasValue
                        ? MucDoNhanThuc.FromId(dto.IdMucDoNhanThuc.Value)
                        : null,
                    false,
                    null,
                    dto.IsKhongDao ?? false,
                    mucDo.MidTheta,
                    null,
                    dto.Stt,
                    true);

                foreach (var tlDto in dto.CauTraLois!)
                {
                    var cauTraLoi = new CauTraLoi(
                        Guid.NewGuid().ToString(),
                        tlDto.NoiDung,
                        tlDto.IsDung,
                        tlDto.PhanTramDiem,
                        tlDto.IsKhongDao,
                        tlDto.IsVeTrai,
                        tlDto.ViTriGachChan,
                        tlDto.IsThietLapRieng,
                        tlDto.Stt,
                        true);

                    pairs.Add((cauHoiCon, cauTraLoi));
                }
            }

            await _nganHangCauHoiRepository
                .CreateCauHoiAsync(cauHoiCha, pairs, cancellationToken);

            return ApiResponse<CreateCauHoiResponse>.Success(
                new CreateCauHoiResponse
                {
                    Id = cauHoiCha?.Id,
                    MaCauHoi = cauHoiCha?.MaCauHoi,
                    IdCauTruc = cauHoiCha?.IdCauTruc,
                    IdLoaiCauHoi = cauHoiCha?.IdLoaiCauHoi,
                    IdCauHoiCha = cauHoiCha?.IdCauHoiCha,
                    IdMucDoNhanThuc = cauHoiCha?.IdMucDoNhanThuc,
                    IdTrangThai = (int?)cauHoiCha?.TrangThai,
                    MediaUrl = cauHoiCha?.MediaUrl,
                    IdNguoiDuyet = cauHoiCha?.IdNguoiDuyet,
                    IdNguoiSoan = cauHoiCha?.IdNguoiTao,
                    IsKhongDao = cauHoiCha?.IsKhongDao,
                    IsCauHoiCha = cauHoiCha?.IsCauHoiCha,
                    TieuDeVeTrai = cauHoiCha?.TieuDeVeTrai,
                    TieuDeVePhai = cauHoiCha?.TieuDeVePhai,
                    Stt = cauHoiCha?.Stt,
                    SttCauHoiCon = null,
                    DoKho = cauHoiCha?.DoKho,
                    DoKhoKhoiTao = cauHoiCha?.DoKhoKhoiTao,
                    CauHoiGuid = cauHoiCha?.CauHoiGuid,
                    GhiChu = cauHoiCha?.GiaiThich,
                    GhiChuDuyet = cauHoiCha?.GhiChuDuyet,
                    NgaySoan = cauHoiCha?.NgayTao,
                    NgayDuyet = cauHoiCha?.NgayDuyet,
                    IsVisible = cauHoiCha?.IsVisible
                });
        }
    }
}
