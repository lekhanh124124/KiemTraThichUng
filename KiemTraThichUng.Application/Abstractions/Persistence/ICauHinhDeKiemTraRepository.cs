using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.ExamSelection.DTOs;
using KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDanhSachDeKiemTra;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;

namespace KiemTraThichUng.Application.Abstractions.Persistence
{
    public interface ICauHinhDeKiemTraRepository
    {
        Task<CauHinhDeKiemTra> CreateCauHinhDeKiemTraAsync(CauHinhDeKiemTra entity, CancellationToken cancellationToken);
        Task UpdateCauHinhDeKiemTraAsync(CauHinhDeKiemTra entity, CancellationToken cancellationToken);
        Task<IReadOnlyList<CauHinhDeKiemTra>> DeleteCauHinhDeKiemTraByIdsAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken);
        Task<bool> KiemTraTonTaiMaAsync(string ma, int? excludeId = null, CancellationToken cancellationToken = default);
        
        Task AddChiTietCauHinhDeKiemTraRangeAsync(IEnumerable<ChiTietCauHinhDeKiemTra> details, CancellationToken cancellationToken);
        Task ClearChiTietCauHinhDeKiemTraAsync(int idCauHinhDeKiemTra, CancellationToken cancellationToken);

        Task<(CauHinhDeKiemTra? CauHinh, IReadOnlyList<ChiTietCauHinhDeKiemTra>? ChiTietCauHinhs)> GetByIdAdminAsync(
            int id,
            CancellationToken cancellationToken);

        Task<(List<CauHinhDeKiemTra>, int)> GetListAdminAsync(
            int? idCauTruc,
            string? keyword,
            string? sortCol,
            bool? isAsc,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken);

        Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiCauHinhDeKiemTraByIdsAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken);
        Task<(CauHinhDeKiemTra CauHinh, IReadOnlyList<ChiTietCauHinhDeKiemTra> ChiTietCauHinhs)> LayCauHinhDeKiemTraByIdAsync(
            int id,
            CancellationToken cancellationToken);
        Task<(List<CauHinhDeKiemTra>, int)> LayDanhSachCauHinhDeKiemTraByIdCauTrucAsync(
            GetListByIdParentQuery request,
            CancellationToken cancellationToken);
    }
}
