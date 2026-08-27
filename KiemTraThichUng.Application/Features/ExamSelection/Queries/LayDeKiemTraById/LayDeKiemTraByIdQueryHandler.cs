using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.ExamSelection.DTOs;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;
using KiemTraThichUng.Domain.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDeKiemTraById
{
    public class LayDeKiemTraByIdQueryHandler : IRequestHandler<LayDeKiemTraByIdQuery, ApiResponse<DeKiemTraDto>>
    {
        private readonly ICauHinhDeKiemTraRepository _cauHinhDeKiemTraRepository;
        public LayDeKiemTraByIdQueryHandler(ICauHinhDeKiemTraRepository cauHinhDeKiemTraRepository)
        {
            _cauHinhDeKiemTraRepository = cauHinhDeKiemTraRepository;
        }
        public async Task<ApiResponse<DeKiemTraDto>> Handle(LayDeKiemTraByIdQuery request, CancellationToken cancellationToken)
        {
            var cauHinhDeKiemTra = await _cauHinhDeKiemTraRepository.LayCauHinhDeKiemTraByIdAsync(request.IdCauHinhDeKiemTra, cancellationToken);

            return ApiResponse<DeKiemTraDto>.Success(new DeKiemTraDto
            {
                Id = cauHinhDeKiemTra.CauHinh.Id,
                IdCauTruc = cauHinhDeKiemTra.CauHinh.IdCauTruc,
                TenCauHinhDeKiemTra = cauHinhDeKiemTra.CauHinh.TenCauHinhDeKiemTra,
                MaCauHinhDeKiemTra = cauHinhDeKiemTra.CauHinh.MaCauHinhDeKiemTra,
                ThoiGianLamBaiGiay = cauHinhDeKiemTra.CauHinh.ThoiGianLamBaiGiay,
                DoKhoMin = MucDoNangLuc.FromTheta(cauHinhDeKiemTra.CauHinh.ThetaMin).Name,
                DoKhoMax = MucDoNangLuc.FromTheta(cauHinhDeKiemTra.CauHinh.ThetaMax).Name,
                MucNangLucDat = MucDoNangLuc.FromTheta(cauHinhDeKiemTra.CauHinh.ThetaDat).Name,
                Stt = cauHinhDeKiemTra.CauHinh.Stt,
                NgayDuyet = cauHinhDeKiemTra.CauHinh.NgayDuyet,
                TongSoLuongCauHoi = cauHinhDeKiemTra.ChiTietCauHinhs.Sum(x => x.SoLuongCauHoi),
                ChiTietDeKiemTras = cauHinhDeKiemTra.ChiTietCauHinhs.Select(c => new ChiTietDeKiemTraItemDto
                {
                    Id = c.Id,
                    IdCauHinhDeKiemTra = c.IdCauHinhDeKiemTra,
                    IdLoaiCauHoi = c.IdLoaiCauHoi,
                    TenLoaiCauHoi = LoaiCauHoi.FromId(c.IdLoaiCauHoi!.Value).Name,
                    IdMucDoNhanThuc = c.IdMucDoNhanThuc,
                    TenMucDoNhanThuc = c.IdMucDoNhanThuc == null ? null : MucDoNangLuc.FromId(c.IdMucDoNhanThuc!.Value).Name,
                    SoLuongCauHoi = c.SoLuongCauHoi
                }).ToList()
            });
        }
    }
}
