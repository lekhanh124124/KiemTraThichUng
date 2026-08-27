using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDanhSachDeKiemTra;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using KiemTraThichUng.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KiemTraThichUng.Infrastructure.Persistence.Repositories
{
    public class CauHinhDeKiemTraRepository : ICauHinhDeKiemTraRepository
    {
        private readonly AppDbContext _context;

        public CauHinhDeKiemTraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CauHinhDeKiemTra> CreateCauHinhDeKiemTraAsync(CauHinhDeKiemTra entity, CancellationToken cancellationToken)
        {
            await _context.CauHinhDeKiemTra.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task UpdateCauHinhDeKiemTraAsync(CauHinhDeKiemTra entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task<IReadOnlyList<CauHinhDeKiemTra>> DeleteCauHinhDeKiemTraByIdsAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            var entities = await _context.CauHinhDeKiemTra
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var entity in entities)
            {
                entity.SoftDelete();
            }

            return entities;
        }

        public async Task AddChiTietCauHinhDeKiemTraRangeAsync(IEnumerable<ChiTietCauHinhDeKiemTra> details, CancellationToken cancellationToken)
        {
            await _context.ChiTietCauHinhDeKiemTra.AddRangeAsync(details, cancellationToken);
        }

        public async Task ClearChiTietCauHinhDeKiemTraAsync(int idCauHinhDeKiemTra, CancellationToken cancellationToken)
        {
            var details = await _context.ChiTietCauHinhDeKiemTra
                .Where(x => x.IdCauHinhDeKiemTra == idCauHinhDeKiemTra && !x.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var detail in details)
            {
                detail.SoftDelete();
            }
        }
        public async Task<bool> KiemTraTonTaiMaAsync(string ma, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _context.CauHinhDeKiemTra
                .AnyAsync(x => x.MaCauHinhDeKiemTra == ma && 
                              (!excludeId.HasValue || x.Id != excludeId.Value) &&
                              !x.IsDeleted, 
                          cancellationToken);
        }

        public async Task<(CauHinhDeKiemTra? CauHinh, IReadOnlyList<ChiTietCauHinhDeKiemTra>? ChiTietCauHinhs)> GetByIdAdminAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var query =
                from c in _context.CauHinhDeKiemTra.AsNoTracking()
                where c.Id == id && !c.IsDeleted
                join ct in _context.ChiTietCauHinhDeKiemTra.AsNoTracking()
                    on c.Id equals ct.IdCauHinhDeKiemTra into ctGroup
                select new
                {
                    CauHinhDeKiemTra = c,
                    ChiTietCauHinhDeKiemTras = ctGroup.Where(x => !x.IsDeleted).ToList()
                };

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return (result?.CauHinhDeKiemTra, result?.ChiTietCauHinhDeKiemTras);
        }

        public async Task<(List<CauHinhDeKiemTra>, int)> GetListAdminAsync(
            int? idCauTruc,
            string? keyword,
            string? sortCol,
            bool? isAsc,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            var query = _context.CauHinhDeKiemTra.AsNoTracking()
                .Where(x => !x.IsDeleted);

            if (idCauTruc.HasValue)
            {
                query = query.Where(x => x.IdCauTruc == idCauTruc.Value);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.TenCauHinhDeKiemTra!.Contains(keyword) || x.MaCauHinhDeKiemTra.Contains(keyword));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(sortCol) && isAsc.HasValue)
            {
                var entityType = typeof(CauHinhDeKiemTra);
                var property = entityType.GetProperty(
                    sortCol,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    query = isAsc.Value
                        ? query.OrderBy(x => EF.Property<object>(x, property.Name))
                        : query.OrderByDescending(x => EF.Property<object>(x, property.Name));
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }

            var results = await query
                .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 20))
                .Take(pageSize ?? 20)
                .ToListAsync(cancellationToken);

            return (results, totalCount);
        }

        public async Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiCauHinhDeKiemTraByIdsAsync(
            IReadOnlyList<int> ids, 
            CancellationToken cancellationToken)
        {
            var existingIds = await _context.CauHinhDeKiemTra
                .Where(x => ids.Contains(x.Id) && 
                    x.IsUsed == false &&
                    x.IsDeleted == false &&
                    x.IsVisible == true && 
                    x.TrangThai == TrangThaiDuyet.DaDuyet)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
            var missingIds = ids.Except(existingIds).ToList();
            var tonTai = !missingIds.Any();
            return (tonTai, missingIds);
        }

        public async Task<(CauHinhDeKiemTra CauHinh, IReadOnlyList<ChiTietCauHinhDeKiemTra> ChiTietCauHinhs)> LayCauHinhDeKiemTraByIdAsync(
            int id, 
            CancellationToken cancellationToken)
        {
            var query = 
                from c in _context.CauHinhDeKiemTra.AsNoTracking()
                where c.Id == id &&
                      c.IsUsed == false &&
                      c.IsDeleted == false &&
                      c.IsVisible == true &&
                      c.TrangThai == TrangThaiDuyet.DaDuyet

                join ct in _context.ChiTietCauHinhDeKiemTra.AsNoTracking()
                    .Where(ct => 
                        ct.IsDeleted == false && 
                        ct.IsVisible == true)
                    on c.Id equals ct.IdCauHinhDeKiemTra into ctGroup
                select new 
                { 
                    CauHinhDeKiemTra = c, 
                    ChiTietCauHinhDeKiemTras = ctGroup.ToList() 
                };

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return (result?.CauHinhDeKiemTra!, result?.ChiTietCauHinhDeKiemTras!);
        }

        public async Task<(List<CauHinhDeKiemTra>, int)> LayDanhSachCauHinhDeKiemTraByIdCauTrucAsync(
            GetListByIdParentQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                from ch in _context.CauHinhDeKiemTra.AsNoTracking()
                join ct in _context.CauTruc.AsNoTracking()
                    on ch.IdCauTruc equals ct.Id
                where
                    (request.IdCauTruc == null || ct.Id == request.IdCauTruc)
                    && (request.IdBoCauHoi == null || ct.IdBoCauHoi == request.IdBoCauHoi)
                    && (request.Keyword == null
                        || ch.TenCauHinhDeKiemTra!.Contains(request.Keyword)
                        || ch.MaCauHinhDeKiemTra!.Contains(request.Keyword))
                    && ch.IsUsed == false
                    && ch.IsDeleted == false
                    && ch.IsVisible == true
                    && ch.TrangThai == TrangThaiDuyet.DaDuyet
                select new
                {
                    CauHinh = ch,
                };

            var totalCount = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.SortCol) && request.IsAsc.HasValue)
            {
                var entityType = typeof(CauHinhDeKiemTra);

                var property = entityType.GetProperty(
                    request.SortCol,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    throw new ValidationException(
                        [$"Sort column '{request.SortCol}' không hợp lệ."]);
                }

                query = request.IsAsc.Value
                    ? query.OrderBy(x => EF.Property<object>(x.CauHinh, property.Name))
                    : query.OrderByDescending(x => EF.Property<object>(x.CauHinh, property.Name));
            }
            else
            {
                query = query.OrderBy(x => x.CauHinh.Id);
            }

            if (request.PageNumber <= 0 || request.PageNumber == null)
            {
                request.PageNumber = 1;
            }
            if (request.PageSize <= 0 || request.PageSize == null)
            {
                request.PageSize = 20;
            }

            query = query
                .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value);

            var data = await query.ToListAsync(cancellationToken);
            var results = data.Select(x => x.CauHinh).ToList();

            return (results, totalCount);
        }
    }
}
