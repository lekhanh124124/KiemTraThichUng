using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.EX_CauHois.Commands.CreateCauHoi;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateCauHoi
{
    public class UpdateCauHoiCommandHandler : IRequestHandler<UpdateCauHoiCommand, ApiResponse<UpdateCauHoiResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        public UpdateCauHoiCommandHandler(
            IUnitOfWork unitOfWork, 
            INganHangCauHoiRepository nganHangCauHoiRepository)
        {
            _unitOfWork = unitOfWork;
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
        }
        public async Task<ApiResponse<UpdateCauHoiResponse>> Handle(
            UpdateCauHoiCommand request,
            CancellationToken cancellationToken)
        {
            var cauHois = await _nganHangCauHoiRepository
                .GetCauHoiByIdForUpdateAsync(request.Id, cancellationToken);

            if (!cauHois.Any())
                return ApiResponse<UpdateCauHoiResponse>
                    .Failure("Không tìm thấy câu hỏi cần cập nhật.");

            var isCauHoiNhom = request.IsCauHoiCha == true;

            // =====================================================
            // 1. XÁC ĐỊNH CÂU HỎI CHA
            // =====================================================
            CauHoi? cauHoiCha = null;

            if (isCauHoiNhom)
            {
                cauHoiCha = cauHois
                    .Select(x => x.Item1)
                    .FirstOrDefault(x => x.Id == request.Id && x.IsCauHoiCha);

                if (cauHoiCha == null)
                    return ApiResponse<UpdateCauHoiResponse>
                        .Failure("Không tìm thấy câu hỏi cha.");

                var mucDo = MucDoNangLuc.FromId((int)request.DoKhoKhoiTao!);

                cauHoiCha.CapNhatThongTin(
                    request.NoiDung,
                    request.TieuDeVeTrai,
                    request.TieuDeVePhai,
                    request.MediaUrl,
                    request.GhiChu,
                    request.IsKhongDao,
                    mucDo.MidTheta,
                    null,
                    request.Stt,
                    null
                );
            }

            // =====================================================
            // 2. BUILD DICTIONARY (O(1) lookup)
            // =====================================================

            var cauHoiDict = cauHois
                .Select(x => x.Item1)
                .Where(x => !x.IsCauHoiCha)
                .ToDictionary(x => x.Id);

            var cauTraLoiDict = cauHois
                .SelectMany(x => x.Item2)
                .ToDictionary(x => x.Id);

            var updatePairs = new List<(CauHoi, CauTraLoi)>();

            // =====================================================
            // 3. MAP REQUEST → DOMAIN
            // =====================================================

            foreach (var cauHoiRequest in request.CauHois!)
            {
                if (!cauHoiDict.TryGetValue((int)cauHoiRequest.Id!, out var cauHoiEntity))
                    continue;

                var mucDo = MucDoNangLuc.FromId((int)cauHoiRequest.DoKhoKhoiTao!);

                cauHoiEntity.CapNhatThongTin(
                    cauHoiRequest.NoiDung,
                    cauHoiRequest.TieuDeVeTrai,
                    cauHoiRequest.TieuDeVePhai,
                    cauHoiRequest.MediaUrl,
                    cauHoiRequest.GhiChu,
                    cauHoiRequest.IsKhongDao,
                    mucDo.MidTheta,
                    null,
                    cauHoiRequest.Stt,
                    null
                );

                foreach (var cauTraLoiRequest in cauHoiRequest.CauTraLois!)
                {
                    CauTraLoi tlEntity;

                    if (cauTraLoiRequest.Id > 0 && cauTraLoiDict.TryGetValue((int)cauTraLoiRequest.Id, out var existingTl))
                    {
                        tlEntity = existingTl;

                        tlEntity.CapNhatThongTin(
                            cauTraLoiRequest.NoiDung,
                            cauTraLoiRequest.IsDung,
                            cauTraLoiRequest.PhanTramDiem,
                            cauTraLoiRequest.IsKhongDao,
                            cauTraLoiRequest.IsVeTrai,
                            cauTraLoiRequest.ViTriGachChan,
                            cauTraLoiRequest.IsThietLapRieng,
                            cauTraLoiRequest.Stt,
                            cauTraLoiRequest.IsVisible
                        );
                    }
                    else
                    {
                        tlEntity = new CauTraLoi(
                            Guid.NewGuid().ToString(),
                            cauTraLoiRequest.NoiDung,
                            cauTraLoiRequest.IsDung,
                            cauTraLoiRequest.PhanTramDiem,
                            cauTraLoiRequest.IsKhongDao,
                            cauTraLoiRequest.IsVeTrai,
                            cauTraLoiRequest.ViTriGachChan,
                            cauTraLoiRequest.IsThietLapRieng,
                            cauTraLoiRequest.Stt,
                            cauTraLoiRequest.IsVisible
                        );
                    }

                    updatePairs.Add((cauHoiEntity, tlEntity));
                }
            }

            await _nganHangCauHoiRepository.UpdateCauHoiByBatchAsync(
                cauHoiCha,
                updatePairs,
                cancellationToken);

            return ApiResponse<UpdateCauHoiResponse>.Success(
                new UpdateCauHoiResponse
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
