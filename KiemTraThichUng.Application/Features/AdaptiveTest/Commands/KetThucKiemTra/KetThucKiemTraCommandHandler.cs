using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.KetThucKiemTra
{
    public class KetThucKiemTraCommandHandler : IRequestHandler<KetThucKiemTraCommand, ApiResponse<KetThucKiemTraResponse>>
    {
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICauHinhDeKiemTraRepository _cauHinhDeKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        public KetThucKiemTraCommandHandler(
            IPhienKiemTraRepository phienKiemTraRepository, 
            ICauHinhDeKiemTraRepository cauHinhDeKiemTraRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _cauHinhDeKiemTraRepository = cauHinhDeKiemTraRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<KetThucKiemTraResponse>> Handle(KetThucKiemTraCommand request, CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;

            var phienKiemTra = await _phienKiemTraRepository.LayPhienKiemTraByIdNguoiDungAsync(idNguoiDung, cancellationToken);

            var cauHinhDeKiemTra = await _cauHinhDeKiemTraRepository.LayCauHinhDeKiemTraByIdAsync(phienKiemTra.KetQua.IdCauHinhDeKiemTra, cancellationToken);

            var cauHoiBoQua = phienKiemTra.ChiTietKetQuas.Where(ct => ct.TrangThai != TrangThaiChiTietKetQua.DaTraLoi).ToList();

            foreach (var chiTiet in cauHoiBoQua)
            {
                chiTiet.BoQua();
            }

            var ketQuaCuoiCung = phienKiemTra.ChiTietKetQuas.LastOrDefault(ct => ct.TrangThai == TrangThaiChiTietKetQua.DaTraLoi);
            var mucNangLucDat = MucDoNangLuc.FromTheta(cauHinhDeKiemTra.CauHinh.ThetaDat);
            MucDoNangLuc? mucNangLuc = MucDoNangLuc.VeryLow;
            double? saiSoUocLuong = cauHinhDeKiemTra.CauHinh.StandardErrorInitial;
            double? diemNangLuc = mucNangLuc.MinTheta;
            double? diemThang10 = MucDoNangLuc.ConvertThetaToScore10(
                mucNangLuc.MinTheta,
                mucNangLuc.MinTheta,
                mucNangLuc.MaxTheta);
            bool? isDat = false;

            if (ketQuaCuoiCung != null)
            {
                diemThang10 = MucDoNangLuc.ConvertThetaToScore10(
                    ketQuaCuoiCung.ThetaAfter!.Value, 
                    cauHinhDeKiemTra.CauHinh.ThetaMin, 
                    cauHinhDeKiemTra.CauHinh.ThetaMax);
                diemNangLuc = ketQuaCuoiCung.ThetaAfter;
                mucNangLuc = MucDoNangLuc.FromTheta(diemNangLuc.Value);
                isDat = mucNangLuc.IsDat(mucNangLucDat);
                saiSoUocLuong = ketQuaCuoiCung.StandardErrorAfter;
            }

            phienKiemTra.KetQua.HoanThanh(isDat, diemThang10, diemNangLuc, saiSoUocLuong);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<KetThucKiemTraResponse>.Success(new KetThucKiemTraResponse
            {
                Id = phienKiemTra.KetQua.Id,
                IdNguoiDung = phienKiemTra.KetQua.IdNguoiDung,
                IdCauHinhDeKiemTra = phienKiemTra.KetQua.IdCauHinhDeKiemTra,
                DoKhoMin = MucDoNangLuc.FromTheta(cauHinhDeKiemTra.CauHinh.ThetaMin).Name,
                DoKhoMax = MucDoNangLuc.FromTheta(cauHinhDeKiemTra.CauHinh.ThetaMax).Name,
                MucNangLucDat = mucNangLucDat.Name,
                ThoiGianBatDau = phienKiemTra.KetQua.ThoiGianBatDau,
                ThoiGianKetThuc = phienKiemTra.KetQua.ThoiGianKetThuc,
                DiemSo = diemThang10,
                IsDat = isDat,
                MucNangLuc = mucNangLuc.Id,
                MaMucNangLuc = mucNangLuc.Name,
                DiemNangLuc = phienKiemTra.KetQua.Theta,
                SaiSoUocLuong = phienKiemTra.KetQua.StandardError,
                IdCauHoiHienTai = ketQuaCuoiCung?.IdCauHoi,
                TrangThai = phienKiemTra.KetQua.TrangThai
            });
        }
    }
}
