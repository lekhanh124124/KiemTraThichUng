using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.Entities.PhienKiemTra;
using MediatR;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.BatDauKiemTra
{
    public class BatDauKiemTraCommandHandler : IRequestHandler<BatDauKiemTraCommand, ApiResponse<BatDauKiemTraResponse>>
    {
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        public BatDauKiemTraCommandHandler(
            IPhienKiemTraRepository phienKiemTraRepository, 
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<BatDauKiemTraResponse>> Handle(
            BatDauKiemTraCommand request, 
            CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;
            var phienKiemTra = await _phienKiemTraRepository.LayPhienKiemTraByIdNguoiDungAsync(idNguoiDung, cancellationToken);
            if (phienKiemTra.KetQua != null)
            {
                var resumeResponse = new BatDauKiemTraResponse
                {
                    Id = phienKiemTra.KetQua.Id,
                    IdNguoiDung = phienKiemTra.KetQua.IdNguoiDung,
                    IdCauHinhDeKiemTra = phienKiemTra.KetQua.IdCauHinhDeKiemTra,
                    ThoiGianBatDau = phienKiemTra.KetQua.ThoiGianBatDau,
                    ThoiGianKetThuc = phienKiemTra.KetQua.ThoiGianKetThuc,
                    DiemSo = phienKiemTra.KetQua.DiemSo,
                    DiemNangLuc = phienKiemTra.KetQua.Theta,
                    SaiSoUocLuong = phienKiemTra.KetQua.StandardError,
                    IdCauHoiHienTai = phienKiemTra.KetQua.IdCauHoiHienTai,
                    TrangThai = phienKiemTra.KetQua.TrangThai
                };
                return ApiResponse<BatDauKiemTraResponse>.Success(resumeResponse);
            }

            var phienKiemTraMoi = new KetQuaKiemTra(
                idNguoiDung,
                request.IdCauHinhDeKiemTra);

            await _phienKiemTraRepository.TaoPhienKiemTraAsync(phienKiemTraMoi, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<BatDauKiemTraResponse>.Success(new BatDauKiemTraResponse
            {
                Id = phienKiemTraMoi.Id,
                IdNguoiDung = phienKiemTraMoi.IdNguoiDung,
                IdCauHinhDeKiemTra = phienKiemTraMoi.IdCauHinhDeKiemTra,
                ThoiGianBatDau = phienKiemTraMoi.ThoiGianBatDau,
                ThoiGianKetThuc = phienKiemTraMoi.ThoiGianKetThuc,
                DiemSo = phienKiemTraMoi.DiemSo,
                DiemNangLuc = phienKiemTraMoi.Theta,
                SaiSoUocLuong = phienKiemTraMoi.StandardError,
                IdCauHoiHienTai = phienKiemTraMoi.IdCauHoiHienTai,
                TrangThai = phienKiemTraMoi.TrangThai
            });
        }
    }
}
