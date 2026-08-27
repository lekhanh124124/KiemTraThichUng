using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Domain.Entities.CauHinhDanhMuc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KiemTraThichUng.Infrastructure.Persistence.Repositories
{
    public class CauHinhDanhMucRepository : ICauHinhDanhMucRepository
    {
        private readonly AppDbContext _context;

        public CauHinhDanhMucRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CauTruc> GetCauTrucByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.CauTruc.FindAsync([id], cancellationToken);
            return entity!;
        }
        public async Task<CauTruc> CreateCauTrucAsync(CauTruc entity, CancellationToken cancellationToken)
        {
            var result = await _context.CauTruc.AddAsync(entity);
            return result.Entity;
        }
        public async Task UpdateCauTrucAsync(CauTruc entity, CancellationToken cancellationToken)
        {
            _context.CauTruc.Update(entity);
        }
        public async Task<IReadOnlyList<CauTruc>> DeleteCauTrucByIdsAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            var entities = await _context.CauTruc.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var entity in entities)
            {
                entity.SoftDelete();
            }
            return entities;
        }

        public async Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiBoCauHoiByIdsAsync(IReadOnlyList<int> id, CancellationToken cancellationToken)
        {
            var existingIds = await _context.BoCauHoi
                .Where(x => id.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
            var missingIds = id.Except(existingIds).ToList();
            var tonTai = !missingIds.Any();
            return (tonTai, missingIds);
        }

        public async Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiCauTrucByIdsAsync(IReadOnlyList<int> id, CancellationToken cancellationToken)
        {
            var existingIds = await _context.CauTruc
                .Where(x => id.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
            var missingIds = id.Except(existingIds).ToList();
            var tonTai = !missingIds.Any();
            return (tonTai, missingIds);
        }

        public async Task<bool> KiemTraTonTaiCauTrucByIdParentAsync(int idParent, CancellationToken cancellationToken)
        {
            var exists = await _context.CauTruc.AnyAsync(x => x.IdParent == idParent);
            return exists;
        }

        public async Task<bool> KiemTraTonTaiCauTrucByMaCauTrucAsync(string maCauTruc, CancellationToken cancellationToken)
        {
            var exists = await _context.CauTruc.AnyAsync(x => x.MaCauTruc == maCauTruc, cancellationToken);
            return exists;
        }

        public async Task<bool> KiemTraTonTaiCauTrucByMaCauTrucNgoaiIdAsync(int id, string maCauTruc, CancellationToken cancellationToken)
        { 
            var exists = await _context.CauTruc.AnyAsync(x => x.MaCauTruc == maCauTruc && x.Id != id, cancellationToken);
            return exists;
        }

        public async Task<(IReadOnlyList<BoCauHoi>, int)> GetAllBoCauHoiAsync(
            int? idParent,
            string? keyword,
            bool? isVisible,
            string? sortCol,
            bool? isAsc,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            var query =
                from boCauHoi in _context.BoCauHoi.AsNoTracking()
                where
                    (keyword == null
                        || boCauHoi.TenBoCauHoi!.Contains(keyword)
                        || boCauHoi.MaBoCauHoi!.Contains(keyword)) &&
                    boCauHoi.IsDeleted == false
                select new
                {
                    BoCauHoi = boCauHoi
                };

            if (!string.IsNullOrWhiteSpace(sortCol) && isAsc.HasValue)
            {
                var entityType = typeof(BoCauHoi);

                var property = entityType.GetProperty(
                    sortCol,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    throw new ValidationException(
                        [$"Sort column '{sortCol}' không hợp lệ."]);
                }

                query = isAsc.Value
                    ? query.OrderBy(x => EF.Property<object>(x.BoCauHoi, property.Name))
                    : query.OrderByDescending(x => EF.Property<object>(x.BoCauHoi, property.Name));
            }
            else
            {
                query = query.OrderBy(x => x.BoCauHoi.Id);
            }

            var totalCount = await query.CountAsync(cancellationToken);


            if (pageNumber <= 0 || pageNumber == null)
            {
                pageNumber = 1;
            }
            if (pageSize <= 0 || pageSize == null)
            {
                pageSize = 20;
            }

            query = query
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

            var data = await query.ToListAsync(cancellationToken);
            var results = data.Select(x => x.BoCauHoi).ToList();
            return (results, totalCount);
        }

        public async Task<(IReadOnlyList<CauTruc>, int)> GetAllCauTrucAsync(
            int? idBoCauHoi,
            int? idParent, 
            string? keyword, 
            string? sortCol, 
            bool? isAsc, 
            int? pageNumber, 
            int? pageSize, 
            CancellationToken cancellationToken)
        {
            var query =
                from cauTruc in _context.CauTruc.AsNoTracking()
                where
                    (keyword == null || cauTruc.TenCauTruc!.Contains(keyword) || cauTruc.MaCauTruc!.Contains(keyword)) &&
                    (!idBoCauHoi.HasValue || cauTruc.IdBoCauHoi == idBoCauHoi) &&
                    ((!idParent.HasValue && !idBoCauHoi.HasValue && cauTruc.IsVisible) || cauTruc.IdParent == idParent) &&
                    (!cauTruc.IsDeleted)
                select new
                {
                    CauTruc = cauTruc
                };

            if (!string.IsNullOrWhiteSpace(sortCol) && isAsc.HasValue)
            {
                var entityType = typeof(CauTruc);

                var property = entityType.GetProperty(
                    sortCol,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    throw new ValidationException(
                        [$"Sort column '{sortCol}' không hợp lệ."]);
                }

                query = isAsc.Value
                    ? query.OrderBy(x => EF.Property<object>(x.CauTruc, property.Name))
                    : query.OrderByDescending(x => EF.Property<object>(x.CauTruc, property.Name));
            }
            else
            {
                query = query.OrderBy(x => x.CauTruc.Id);
            }

            var totalCount = await query.CountAsync(cancellationToken);


            if (pageNumber <= 0 || pageNumber == null)
            {
                pageNumber = 1;
            }
            if (pageSize <= 0 || pageSize == null)
            {
                pageSize = 20;
            }

            query = query
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

            var data = await query.ToListAsync(cancellationToken);
            var results = data.Select(x => x.CauTruc).ToList();
            return (results, totalCount);
        }
    }
}
