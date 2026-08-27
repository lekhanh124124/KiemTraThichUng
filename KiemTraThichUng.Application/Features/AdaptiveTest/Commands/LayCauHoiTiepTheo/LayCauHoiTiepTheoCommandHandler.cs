using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.Entities.PhienKiemTra;
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;
using System.Security.Claims;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.LayCauHoiTiepTheo
{
    public class LayCauHoiTiepTheoCommandHandler : IRequestHandler<LayCauHoiTiepTheoCommand, ApiResponse<LayCauHoiTiepTheoResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        private readonly ICauHinhDeKiemTraRepository _cauHinhDeKiemTraRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LayCauHoiTiepTheoCommandHandler(
            IPhienKiemTraRepository phienKiemTraRepository, 
            INganHangCauHoiRepository nganHangCauHoiRepository, 
            ICauHinhDeKiemTraRepository cauHinhDeKiemTraRepository, 
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
            _cauHinhDeKiemTraRepository = cauHinhDeKiemTraRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<LayCauHoiTiepTheoResponse>> Handle(LayCauHoiTiepTheoCommand request, CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;
            var phienKiemTra = await _phienKiemTraRepository.LayPhienKiemTraByIdNguoiDungAsync(idNguoiDung, cancellationToken);

            if (phienKiemTra.KetQua == null)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = true,
                        Reason = StopReason.SessionClosed
                    });
            }
            else if (phienKiemTra.KetQua.TrangThai == TrangThaiKiemTra.HoanThanh)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = true,
                        Reason = StopReason.UserSubmitted
                    });
            }

            var cauHinhDeKiemTra = await _cauHinhDeKiemTraRepository.LayCauHinhDeKiemTraByIdAsync(phienKiemTra.KetQua.IdCauHinhDeKiemTra, cancellationToken);

            if (cauHinhDeKiemTra.CauHinh == null)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = true,
                        Reason = StopReason.BlueprintUnderMaintenance
                    });
            }

            var thoiGianKetThuc = phienKiemTra.KetQua.ThoiGianBatDau.AddSeconds(cauHinhDeKiemTra.CauHinh.ThoiGianLamBaiGiay);
            var thoiGianHienTai = DateTime.Now;
            if (thoiGianHienTai >= thoiGianKetThuc)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = true,
                        Reason = StopReason.TimeExpired
                    });
            }

            bool coCauHoiDaGiao = phienKiemTra.ChiTietKetQuas.Any(ct => ct.TrangThai == TrangThaiChiTietKetQua.DaGiao);
            if (coCauHoiDaGiao)
            {
                var idCauHoiDangLam = phienKiemTra.ChiTietKetQuas
                    .Where(ct => ct.TrangThai == TrangThaiChiTietKetQua.DaGiao)
                    .Select(ct => ct.IdCauHoiCha ?? ct.IdCauHoi)
                    .FirstOrDefault();
                var cauHoiDangLam = await _nganHangCauHoiRepository.GetCauHoiByIdAsync(idCauHoiDangLam, cancellationToken);
                if (cauHoiDangLam != null)
                {
                    return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                        new LayCauHoiTiepTheoResponse
                        {
                            IsFinished = false,
                            CauHoi = new CauHoiMucTieuDto
                            {
                                Id = cauHoiDangLam.Id,
                                MaCauHoi = cauHoiDangLam.MaCauHoi,
                                IdCauTruc = cauHoiDangLam.IdCauTruc,
                                IdLoaiCauHoi = cauHoiDangLam.IdLoaiCauHoi,
                                IdMucDoNhanThuc = cauHoiDangLam.IdMucDoNhanThuc,
                                IdTrangThai = cauHoiDangLam.IdTrangThai,
                                IsKhongDao = cauHoiDangLam.IsKhongDao,
                                IsCauHoiCha = cauHoiDangLam.IsCauHoiCha,
                                DoKho = cauHoiDangLam.DoKhoKhoiTao,
                                DoPhanLoai = null,
                                NoiDung = cauHoiDangLam.NoiDung,
                                Stt = cauHoiDangLam.Stt,
                                GhiChu = cauHoiDangLam.GhiChu,
                                MediaUrl = cauHoiDangLam.MediaUrl,
                                TieuDeVeTrai = cauHoiDangLam.TieuDeVeTrai,
                                TieuDeVePhai = cauHoiDangLam.TieuDeVePhai,
                                CauHois = cauHoiDangLam.CauHois?.Select(ch => new CauHoisMucTieuDto
                                {
                                    Id = ch.Id,
                                    MaCauHoi = ch.MaCauHoi,
                                    IdCauTruc = ch.IdCauTruc,
                                    IdLoaiCauHoi = ch.IdLoaiCauHoi,
                                    IdMucDoNhanThuc = ch.IdMucDoNhanThuc,
                                    IdTrangThai = ch.IdTrangThai,
                                    IsKhongDao = ch.IsKhongDao,
                                    IsCauHoiCha = ch.IsCauHoiCha,
                                    DoKho = ch.DoKhoKhoiTao,
                                    DoPhanLoai = null,
                                    NoiDung = ch.NoiDung,
                                    Stt = ch.Stt,
                                    GhiChu = ch.GhiChu,
                                    MediaUrl = ch.MediaUrl,
                                    TieuDeVeTrai = ch.TieuDeVeTrai,
                                    TieuDeVePhai = ch.TieuDeVePhai,
                                    CauTraLois = ch.CauTraLois?.Select(ct => new CauTraLoisMucTieuDto
                                    {
                                        Id = ct.Id,
                                        IdCauHoi = ct.IdCauHoi,
                                        IsKhongDao = ct.IsKhongDao,
                                        IsVeTrai = ct.IsVeTrai,
                                        NoiDung = ct.NoiDung,
                                        Stt = ct.Stt,
                                        ViTriGachChan = ct.ViTriGachChan
                                    }).ToList()
                                })!
                            }
                        });
                }
                        
            }

            var cauHinhDaLam = await _phienKiemTraRepository.LayCauHinhDaLamByIdKetQuaKiemTraAsync(phienKiemTra.KetQua.Id, cancellationToken);

            var groupedDaLam = cauHinhDaLam
                .GroupBy(x => new { x.IdLoaiCauHoi, x.IdMucDoNhanThuc })
                .Select(g => new
                {
                    g.Key.IdLoaiCauHoi,
                    g.Key.IdMucDoNhanThuc,
                    SoLuong = g.Sum(x => x.SoLuong)
                })
                .ToList();

            var cauHinhMucTieu = cauHinhDeKiemTra.ChiTietCauHinhs
                .Select(ct =>
                {
                    var soDaLam = groupedDaLam
                        .Where(x =>
                            (ct.IdLoaiCauHoi == null || x.IdLoaiCauHoi == ct.IdLoaiCauHoi) &&
                            (ct.IdMucDoNhanThuc == null || x.IdMucDoNhanThuc == ct.IdMucDoNhanThuc))
                        .Sum(x => x.SoLuong);

                    return new
                    {
                        ct.IdLoaiCauHoi,
                        ct.IdMucDoNhanThuc,
                        DaLam = soDaLam,
                        TongCauHoi = ct.SoLuongCauHoi
                    };
                })
                .FirstOrDefault(x => x.DaLam < x.TongCauHoi);

            if (cauHinhMucTieu == null)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = true,
                        Reason = StopReason.BlueprintCompleted
                    });
            }

            var thetaMucTieu = phienKiemTra.ChiTietKetQuas
                .Where(ct => ct.TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                .OrderByDescending(ct => ct.Id)
                .Select(ct => ct.ThetaTarget)
                .FirstOrDefault();

            if (!phienKiemTra.ChiTietKetQuas.Any() || phienKiemTra.ChiTietKetQuas.Count() == 0)
            {
                thetaMucTieu = cauHinhDeKiemTra.CauHinh.ThetaKhoiTao;
            }
            else if ( thetaMucTieu < cauHinhDeKiemTra.CauHinh.ThetaMin)
            {
                thetaMucTieu = cauHinhDeKiemTra.CauHinh.ThetaMin;
            }
            else if (thetaMucTieu > cauHinhDeKiemTra.CauHinh.ThetaMax)
            {
                thetaMucTieu = cauHinhDeKiemTra.CauHinh.ThetaMax;
            }
            else if (thetaMucTieu == null)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = true,
                        Reason = StopReason.NoEligibleQuestion
                    });
            }

            var idsLoaiTru = phienKiemTra.ChiTietKetQuas
                .Where(ct => ct.TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                .Select(ct => ct.IdCauHoiCha ?? ct.IdCauHoi)
                .ToList();

            var cauHoiTiepTheo = await _nganHangCauHoiRepository.GetCauHoiByBlueprintAsync(
                cauHinhMucTieu.IdLoaiCauHoi,
                cauHinhMucTieu.IdMucDoNhanThuc,
                thetaMucTieu.Value,
                idsLoaiTru,
                cancellationToken);

            if (cauHoiTiepTheo == null)
            {
                return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                    new LayCauHoiTiepTheoResponse
                    {
                        IsFinished = false,
                        Reason = StopReason.NoEligibleQuestion
                    });
            }

            phienKiemTra.KetQua.CapNhatCauHoiHienTai(cauHoiTiepTheo.Id);

            var chiTietKetQuasToInsert = cauHoiTiepTheo.CauHois?
                .Select(ch => new ChiTietKetQuaKiemTra(
                    phienKiemTra.KetQua.Id,
                    ch.Id!.Value,
                    ch.IdCauHoiCha,
                    ch.DoKho,
                    ch.DoPhanLoai)).ToList();

            await _phienKiemTraRepository.TaoChiTietKetQuaKiemTraAsync(chiTietKetQuasToInsert!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<LayCauHoiTiepTheoResponse>.Success(
                new LayCauHoiTiepTheoResponse
                {
                    IsFinished = false,
                    Reason = null,
                    CauHoi = cauHoiTiepTheo
                });
        }
    }
}
