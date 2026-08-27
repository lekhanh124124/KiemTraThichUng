// File: KiemTraThichUng.Application/Abstractions/Persistence/INganHangCauHoiRepository.cs
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetListCauHoi;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;

namespace KiemTraThichUng.Application.Abstractions.Persistence
{
    public interface INganHangCauHoiRepository
    {
        Task<IList<CauHoi>> GetCauHoiByIdsAsync(
            IReadOnlyList<int> Ids,
            CancellationToken cancellationToken);  

        Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiCauHoiByIdsAsync(
            IReadOnlyList<int> ids,
            CancellationToken cancellationToken);

        Task<(IReadOnlyList<CauHoiItemDto>?, int)> GetListCauHoiAsync(
            GetListCauHoiQuery request, 
            CancellationToken cancellationToken);

        Task<CauHoiDto?> GetCauHoiByIdAsync(
            int id,
            CancellationToken cancellationToken);

        Task<CauHoi?> CreateCauHoiAsync(
            CauHoi? cauHoiCha,
            List<(CauHoi CauHoi, CauTraLoi CauTraLoi)> cauHois,
            CancellationToken cancellationToken);

        Task<IList<(CauHoi, IEnumerable<CauTraLoi>)>> GetCauHoiByIdForUpdateAsync(
            int? id,
            CancellationToken cancellationToken);

        Task<IList<(CauHoi CauHoi, IEnumerable<CauTraLoi> CauTraLois)>> LayCauHoiByIdsAsync(
            IReadOnlyList<int?> ids,
            CancellationToken cancellationToken);

        Task<CauHoi?> UpdateCauHoiByBatchAsync(
            CauHoi? cauHoiCha,
            List<(CauHoi CauHoi, CauTraLoi CauTraLoi)> cauHois,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<CauHoi>> DeleteCauHoiByIdsAsync(
            IReadOnlyList<int> ids, 
            CancellationToken cancellationToken);

        Task<CauHoiMucTieuDto?> GetCauHoiByBlueprintAsync(
            int? idLoaiCauHoi,
            int? idMucDoNhanThuc,
            double thetaMucTieu,
            IReadOnlyList<int>? idsLoaiTru,
            CancellationToken cancellationToken);


    }
}
