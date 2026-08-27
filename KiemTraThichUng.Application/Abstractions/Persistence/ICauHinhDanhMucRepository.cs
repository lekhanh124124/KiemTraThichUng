// File: KiemTraThichUng.Application/Abstractions/Persistence/IDataControlsRepository.cs
using KiemTraThichUng.Domain.Entities.CauHinhDanhMuc;

namespace KiemTraThichUng.Application.Abstractions.Persistence
{
    public interface ICauHinhDanhMucRepository
    {
        Task<CauTruc> GetCauTrucByIdAsync(int id, CancellationToken cancellationToken);
        Task<CauTruc> CreateCauTrucAsync(CauTruc entity, CancellationToken cancellationToken);
        Task UpdateCauTrucAsync(CauTruc entity, CancellationToken cancellationToken);
        Task<IReadOnlyList<CauTruc>> DeleteCauTrucByIdsAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken);
        Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiBoCauHoiByIdsAsync(IReadOnlyList<int> id, CancellationToken cancellationToken);
        Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiCauTrucByIdsAsync(IReadOnlyList<int> id, CancellationToken cancellationToken);
        Task<bool> KiemTraTonTaiCauTrucByIdParentAsync(int idParent, CancellationToken cancellationToken);
        Task<bool> KiemTraTonTaiCauTrucByMaCauTrucAsync(string maCauTruc, CancellationToken cancellationToken);
        Task<bool> KiemTraTonTaiCauTrucByMaCauTrucNgoaiIdAsync(int id, string maCauTruc, CancellationToken cancellationToken);

        Task<(IReadOnlyList<BoCauHoi>, int)> GetAllBoCauHoiAsync(
            int? idParent,
            string? keyword,
            bool? isVisible,
            string? sortCol,
            bool? isAsc,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken);

        Task<(IReadOnlyList<CauTruc>, int)> GetAllCauTrucAsync(
            int? idBoCauHoi,
            int? idParent,
            string? keyword,
            string? sortCol,
            bool? isAsc,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken);
    }
}
