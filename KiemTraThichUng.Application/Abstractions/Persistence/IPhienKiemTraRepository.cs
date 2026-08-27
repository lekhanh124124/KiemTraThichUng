using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDuLieuKiemTra;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using KiemTraThichUng.Domain.Entities.PhienKiemTra;

namespace KiemTraThichUng.Application.Abstractions.Persistence
{
    public interface IPhienKiemTraRepository
    {
        Task<(KetQuaKiemTra KetQua, IList<ChiTietKetQuaKiemTra> ChiTietKetQuas)> LayPhienKiemTraByIdNguoiDungAsync(
            int idNguoiDung,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<PhienKiemTraItemDto>> LayDanhSachKetQuaKiemTraByIdNguoiDungAsync(
            int idNguoiDung,
            CancellationToken cancellationToken);

        Task<DuLieuKiemTraDto?> LayPhienKiemTraByIdKetQuaKiemTraAsync(
            int idNguoiDung,
            LayDuLieuKiemTraQuery request,
            CancellationToken cancellationToken);

        Task<KetQuaKiemTra> TaoPhienKiemTraAsync(
            KetQuaKiemTra ketQuaKiemTra,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<(int? IdLoaiCauHoi, int? IdMucDoNhanThuc, int SoLuong)>> LayCauHinhDaLamByIdKetQuaKiemTraAsync(
            int idKetQuaKiemTra,
            CancellationToken cancellationToken);

        Task TaoChiTietKetQuaKiemTraAsync(
            IReadOnlyList<ChiTietKetQuaKiemTra> chiTietKetQuaKiemTras,
            CancellationToken cancellationToken);

        Task TaoChiTietLuaChonDapAnAsync(
            IReadOnlyList<ChiTietLuaChon> chiTietLuaChonDapAns,
            CancellationToken cancellationToken);

        Task<(bool IsThieu, IReadOnlyList<int> CauHoisThieu)> KiemTraCauTraLoiThieuAsync(
            int idNguoiDung,
            IReadOnlyList<int?>? idsCauHoi,
            CancellationToken cancellationToken);

        Task<(bool IsHoanThanh, CauHinhDeKiemTra? CauHinh)> KiemTraDaHoanThanhAsync(
            int idNguoiDung,
            CancellationToken cancellationToken);
    }
}
