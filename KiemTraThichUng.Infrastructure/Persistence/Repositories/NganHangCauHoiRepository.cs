using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetListCauHoi;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KiemTraThichUng.Infrastructure.Persistence.Repositories
{
    public class NganHangCauHoiRepository : INganHangCauHoiRepository
    {
        private readonly AppDbContext _context;

        public NganHangCauHoiRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CauHoi>> GetCauHoiByIdsAsync(
            IReadOnlyList<int> ids,
            CancellationToken cancellationToken)
        {
            return await _context.CauHoi
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<(bool, IReadOnlyList<int>?)> KiemTraTonTaiCauHoiByIdsAsync(
            IReadOnlyList<int> ids, 
            CancellationToken cancellationToken)
        {
            var existingIds = await _context.CauHoi
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
            var idKhongTonTai = ids.Except(existingIds).ToList();
            var isTonTai = !idKhongTonTai.Any();
            return (isTonTai, idKhongTonTai);
        }

        public async Task<(IReadOnlyList<CauHoiItemDto>?, int)> GetListCauHoiAsync(
            GetListCauHoiQuery request,
            CancellationToken cancellationToken)

        {
            var baseQuery =
                from cauHoi in _context.CauHoi.AsNoTracking()

                join cauTruc in _context.CauTruc.AsNoTracking()
                    on cauHoi.IdCauTruc equals cauTruc.Id
                where cauTruc.IsDeleted == false

                where
                    (string.IsNullOrEmpty(request.Keyword)
                        || cauHoi.MaCauHoi.Contains(request.Keyword)) &&

                    (string.IsNullOrEmpty(request.MaCauHoi)
                        || cauHoi.MaCauHoi.Contains(request.MaCauHoi)) &&

                    (!request.IsVisible.HasValue
                        || cauHoi.IsVisible == request.IsVisible) &&

                    (!request.TuNgayTao.HasValue
                        || cauHoi.NgayTao >= request.TuNgayTao.Value) &&

                    (!request.DenNgayTao.HasValue
                        || cauHoi.NgayTao <= request.DenNgayTao.Value) &&

                    (request.IdMucDoNhanThuc == null
                        || request.IdMucDoNhanThuc.Count == 0
                        || (cauHoi.IdMucDoNhanThuc.HasValue &&
                            request.IdMucDoNhanThuc.Contains(cauHoi.IdMucDoNhanThuc.Value))) &&

                    (!request.IdTrangThaiCauHoi.HasValue
                        || cauHoi.TrangThai == request.IdTrangThaiCauHoi.Value) &&

                    (!request.IdLoaiCauHoi.HasValue
                        || cauHoi.IdLoaiCauHoi == request.IdLoaiCauHoi.Value) &&

                    (!request.IdCauTruc.HasValue
                        || cauHoi.IdCauTruc == request.IdCauTruc.Value) &&

                    (!request.IdBoCauHoi.HasValue
                        || cauTruc.IdBoCauHoi == request.IdBoCauHoi.Value) &&

                    (!request.IdNguoiSoan.HasValue
                        || cauHoi.IdNguoiTao == request.IdNguoiSoan.Value) &&

                    (!request.IdNhanSu.HasValue
                        || cauHoi.IdNguoiTao == request.IdNhanSu) &&

                    cauHoi.IdCauHoiCha == null &&
                    cauHoi.IsDeleted == false

                select new
                {
                    CauHoi = cauHoi,
                    CauTruc = cauTruc
                };

            if (!string.IsNullOrWhiteSpace(request.SortCol) && request.IsAsc.HasValue)
            {
                var entityType = typeof(CauHoi);

                var property = entityType.GetProperty(
                    request.SortCol,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    throw new ValidationException(
                        [$"Sort column '{request.SortCol}' không hợp lệ."]);
                }

                baseQuery = request.IsAsc.Value
                    ? baseQuery.OrderBy(x => EF.Property<object>(x.CauHoi, property.Name))
                    : baseQuery.OrderByDescending(x => EF.Property<object>(x.CauHoi, property.Name));
            }
            else
            {
                baseQuery = baseQuery.OrderByDescending(x => x.CauHoi.Id);
            }

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 20;

            var items = await baseQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CauHoiItemDto
                {
                    Id = x.CauHoi.Id,
                    MaCauHoi = x.CauHoi.MaCauHoi,
                    IdCauTrucBCH = x.CauHoi.IdCauTruc,
                    IdLoaiCauHoi = x.CauHoi.IdLoaiCauHoi,
                    IsKhongDao = x.CauHoi.IsKhongDao,
                    IsCauHoiCha = x.CauHoi.IsCauHoiCha,
                    IdCauHoiCha = x.CauHoi.IdCauHoiCha,
                    DoKho = x.CauHoi.DoKho,
                    DoKhoKhoiTao = x.CauHoi.DoKhoKhoiTao,
                    IsVisible = x.CauHoi.IsVisible,
                    IdTrangThai = x.CauHoi.TrangThai,
                    MediaUrl = x.CauHoi.MediaUrl,
                    TieuDeVeTrai = x.CauHoi.TieuDeVeTrai,
                    TieuDeVePhai = x.CauHoi.TieuDeVePhai,
                    SttCauHoi = x.CauHoi.Stt,
                    IdNguoiSoan = x.CauHoi.IdNguoiTao,
                    NgaySoan = x.CauHoi.NgayTao,
                    IdNguoiDuyet = x.CauHoi.IdNguoiDuyet,
                    NgayDuyet = x.CauHoi.NgayDuyet,
                    CauHoiGUId = x.CauHoi.CauHoiGuid,
                    MaCauTruc = x.CauTruc.MaCauTruc,
                    TenCauTruc = x.CauTruc.TenCauTruc,
                    IdBoCauHoi = x.CauTruc.IdBoCauHoi,
                    MaLoaiCauHoi = x.CauHoi.LoaiCauHoi.Code,
                    TenLoaiCauHoi = x.CauHoi.LoaiCauHoi.Name,
                    MaMucDoNhanThuc = x.CauHoi.MucDoNhanThuc!.Code,
                    TenMucDoNhanThuc = x.CauHoi.MucDoNhanThuc!.Name,
                    GhiChuDuyet = x.CauHoi.GhiChuDuyet,
                    NoiDung = x.CauHoi.NoiDung,
                }).ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<CauHoiDto?> GetCauHoiByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var data = await (
                from parent in _context.CauHoi.AsNoTracking()
                    .Where(c => c.Id == id && !c.IsDeleted)

                from children in _context.CauHoi
                    .Where(c => c.IdCauHoiCha == parent.Id && !c.IsDeleted)
                    .DefaultIfEmpty()

                from answers in _context.CauTraLoi
                    .Where(a => a.IdCauHoi == (children != null ? children.Id : parent.Id) && !a.IsDeleted)
                    .DefaultIfEmpty()

                select new
                {
                    CauHoi = parent,
                    CauHois = children,
                    CauTraLois = answers
                })
                .ToListAsync(cancellationToken);

            if (!data.Any())
                return null;

            var cauHoi = data.First().CauHoi;

            var cauHois = data
                .Where(x => x.CauHois != null)
                .Select(x => x.CauHois!)
                .Where(x => x.IdCauHoiCha == cauHoi.Id)
                .DistinctBy(x => x.Id)
                .ToList();

            if (!cauHois.Any() && !cauHoi.IdCauHoiCha.HasValue)
            {
                cauHois.Add(cauHoi);
            }

            var response = new CauHoiDto
            {
                Id = cauHoi.Id,
                MaCauHoi = cauHoi.MaCauHoi,
                IdCauTruc = cauHoi.IdCauTruc,
                IdLoaiCauHoi = cauHoi.IdLoaiCauHoi,
                IdMucDoNhanThuc = cauHoi.IdMucDoNhanThuc,
                IdTrangThai = cauHoi.TrangThai,
                IsKhongDao = cauHoi.IsKhongDao,
                IsCauHoiCha = cauHoi.IsCauHoiCha,
                DoKho = cauHoi.DoKho,
                DoKhoKhoiTao = cauHoi.DoKhoKhoiTao,
                NoiDung = cauHoi.NoiDung,
                Stt = cauHoi.Stt,
                GhiChu = cauHoi.GiaiThich,
                MediaUrl = cauHoi.MediaUrl,
                TieuDeVeTrai = cauHoi.TieuDeVeTrai,
                TieuDeVePhai = cauHoi.TieuDeVePhai,
                CauHois = cauHois.Select(cauHois =>
                {
                    return new CauHoisDto
                    {
                        Id = cauHois.Id,
                        MaCauHoi = cauHois.MaCauHoi,
                        IdCauTruc = cauHois.IdCauTruc,
                        IdLoaiCauHoi = cauHois.IdLoaiCauHoi,
                        IdMucDoNhanThuc = cauHois.IdMucDoNhanThuc,
                        IdTrangThai = cauHois.TrangThai,
                        IsKhongDao = cauHois.IsKhongDao,
                        IsCauHoiCha = cauHois.IsCauHoiCha,
                        IdCauHoiCha = cauHois.IdCauHoiCha,
                        DoKho = cauHois.DoKho,
                        DoKhoKhoiTao = cauHois.DoKhoKhoiTao,
                        NoiDung = cauHois.NoiDung,
                        Stt = cauHois.Stt,
                        GhiChu = cauHois.GiaiThich,
                        MediaUrl = cauHois.MediaUrl,
                        TieuDeVeTrai = cauHois.TieuDeVeTrai,
                        TieuDeVePhai = cauHois.TieuDeVePhai,

                        CauTraLois = data
                            .Where(data => data.CauTraLois.IdCauHoi == cauHois.Id)
                            .Select(cauTraLois => cauTraLois.CauTraLois!)
                            .OrderBy(cauTraLois => cauTraLois.Id)
                            .Select(cauTraLois => new CauTraLoisDto
                            {
                                Id = cauTraLois.Id,
                                IdCauHoi = cauTraLois.IdCauHoi,
                                NoiDung = cauTraLois.NoiDung,
                                IsDung = cauTraLois.IsDung,
                                IsKhongDao = cauTraLois.IsKhongDao,
                                IsVeTrai = cauTraLois.IsVeTrai,
                                ViTriGachChan = cauTraLois.ViTriGachChan,
                                PhanTramDiem = cauTraLois.PhanTramDiem,
                                IsThietLapRieng = cauTraLois.IsThietLapRieng,
                                Stt = cauTraLois.Stt,
                                IsVisible = cauTraLois.IsVisible
                            }).ToList()
                    };
                }).ToList()
            };

            return response;
        }

        public async Task<CauHoi?> CreateCauHoiAsync(
            CauHoi? cauHoiCha,
            List<(CauHoi CauHoi, CauTraLoi CauTraLoi)> cauHois,
            CancellationToken cancellationToken)
        {
            await using var transaction =
                    await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // 1️⃣ Save cha
                if (cauHoiCha != null)
                {
                    await _context.CauHoi.AddAsync(cauHoiCha, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // 2️⃣ Save con
                var cauHoiCons = cauHois
                    .Select(x => x.CauHoi)
                    .Distinct()
                    .ToList();

                if (cauHoiCha != null)
                {
                    foreach (var ch in cauHoiCons)
                    {
                        ch.ThemCauHoiCon(false, cauHoiCha.Id);
                    }
                }

                await _context.CauHoi.AddRangeAsync(cauHoiCons, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                // 3️⃣ Save trả lời
                foreach (var (cauHoi, cauTraLoi) in cauHois)
                {
                    cauTraLoi.ThemCauTraLoi(cauHoi.Id);
                }

                await _context.CauTraLoi.AddRangeAsync(
                    cauHois.Select(x => x.CauTraLoi),
                    cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return cauHoiCha;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IList<(CauHoi, IEnumerable<CauTraLoi>)>> GetCauHoiByIdForUpdateAsync(
            int? id,
            CancellationToken cancellationToken)
        {
            var cauHoiQuery = _context.CauHoi
                .Where(
                    ch => ch.IsVisible == true &&
                    ch.IsDeleted == false)
                .Where(ch => (ch.IdCauHoiCha ?? ch.Id) == id);

            var cauTraLoiQuery = _context.CauTraLoi
                .Where(
                    ch => ch.IsVisible == true &&
                    ch.IsDeleted == false);

            var finalQuery =
                from ch in cauHoiQuery
                join ctl in cauTraLoiQuery on ch.Id equals ctl.IdCauHoi into ctlGroup
                select new { CauHoi = ch, CauTraLois = ctlGroup };

            var items = await finalQuery.ToListAsync(cancellationToken);

            return items.Select(x => (x.CauHoi, x.CauTraLois)).ToList();
        }

        public async Task<CauHoi?> UpdateCauHoiByBatchAsync(
            CauHoi? cauHoiCha,
            List<(CauHoi CauHoi, CauTraLoi CauTraLoi)> cauHois,
            CancellationToken cancellationToken)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // =========================
                // 1. UPDATE CÂU HỎI CHA
                // =========================
                if (cauHoiCha != null)
                {
                    _context.CauHoi.Update(cauHoiCha);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // =========================
                // 2. SOFT DELETE CÂU HỎI CON
                // =========================
                if (cauHoiCha != null)
                {
                    var requestCauHoiIds = cauHois
                        .Select(x => x.CauHoi.Id)
                        .Where(id => id > 0)
                        .ToHashSet();

                    var dbCauHoiCons = _context.CauHoi
                        .Where(x => x.IdCauHoiCha == cauHoiCha.Id && !x.IsDeleted)
                        .ToList();

                    foreach (var ch in dbCauHoiCons)
                    {
                        if (!requestCauHoiIds.Contains(ch.Id))
                            ch.SoftDelete();
                    }
                }

                // =========================
                // 3. ADD CÂU HỎI CON MỚI
                // =========================
                var newCauHois = cauHois
                    .Select(x => x.CauHoi)
                    .Where(x => x.Id == 0)
                    .Distinct()
                    .ToList();

                if (newCauHois.Any())
                {
                    _context.CauHoi.AddRange(newCauHois);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // =========================
                // 4. SOFT DELETE CÂU TRẢ LỜI
                // =========================
                foreach (var group in cauHois.GroupBy(x => x.CauHoi.Id))
                {
                    var requestTlIds = group
                        .Select(x => x.CauTraLoi.Id)
                        .Where(id => id > 0)
                        .ToHashSet();

                    var dbTls = _context.CauTraLoi
                        .Where(x => x.IdCauHoi == group.Key && !x.IsDeleted)
                        .ToList();

                    foreach (var tl in dbTls)
                    {
                        if (!requestTlIds.Contains(tl.Id))
                            tl.SoftDelete();
                    }
                }

                // =========================
                // 5. ADD / UPDATE CÂU TRẢ LỜI
                // =========================
                foreach (var (cauHoi, tl) in cauHois)
                {
                    tl.ThemCauTraLoi(cauHoi.Id);

                    if (tl.Id == 0)
                        _context.CauTraLoi.Add(tl);
                    else
                        _context.CauTraLoi.Update(tl);
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return cauHoiCha;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IReadOnlyList<CauHoi>> DeleteCauHoiByIdsAsync(
            IReadOnlyList<int> ids, 
            CancellationToken cancellationToken)
        {
            var entities = await _context.CauHoi.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var entity in entities)
            {
                entity.SoftDelete();
            }
            return entities;
        }

        public async Task<CauHoiMucTieuDto?> GetCauHoiByBlueprintAsync(
            int? idLoaiCauHoi, 
            int? idMucDoNhanThuc, 
            double thetaMucTieu,
            IReadOnlyList<int>? idsLoaiTru,
            CancellationToken cancellationToken)
        {
            var parentsQuery =
                from parent in _context.CauHoi.AsNoTracking()
                where parent.IsVisible
                      && !parent.IsDeleted
                      && (!idLoaiCauHoi.HasValue || parent.IdLoaiCauHoi == idLoaiCauHoi)
                      && (!idMucDoNhanThuc.HasValue || parent.IdMucDoNhanThuc == idMucDoNhanThuc)
                      && parent.IdCauHoiCha == null
                      && (idsLoaiTru == null || !idsLoaiTru.Contains(parent.Id))
                select new
                {
                    Parent = parent,
                    Information =
                        (parent.DoKhoKhoiTao.HasValue
                            ? Math.Exp(thetaMucTieu - parent.DoKhoKhoiTao.Value) /
                              Math.Pow(1 + Math.Exp(thetaMucTieu - parent.DoKhoKhoiTao.Value), 2)
                            : 0)
                        +
                        (
                            from child in _context.CauHoi
                            where child.IdCauHoiCha == parent.Id
                                  && child.IsVisible
                                  && !child.IsDeleted
                                  && child.DoKhoKhoiTao.HasValue
                            select
                                Math.Exp(thetaMucTieu - child.DoKhoKhoiTao!.Value) /
                                Math.Pow(1 + Math.Exp(thetaMucTieu - child.DoKhoKhoiTao.Value), 2)
                        ).Sum()
                };

            var top5ParentIds = parentsQuery
                .OrderByDescending(x => x.Information)
                .Select(x => x.Parent.Id)
                .Take(5);

            var data =
                await (
                    from parent in _context.CauHoi.AsNoTracking()
                    where top5ParentIds.Contains(parent.Id)

                    from child in _context.CauHoi.AsNoTracking()
                        .Where(c =>
                            c.IdCauHoiCha == parent.Id &&
                            c.IsVisible &&
                            !c.IsDeleted)
                        .DefaultIfEmpty()

                    from answer in _context.CauTraLoi.AsNoTracking()
                        .Where(a =>
                            a.IsVisible &&
                            !a.IsDeleted &&
                            a.IdCauHoi == (child != null ? child.Id : parent.Id))
                        .DefaultIfEmpty()

                    select new
                    {
                        CauHoi = parent,
                        CauHois = child,
                        CauTraLois = answer
                    }
                ).ToListAsync(cancellationToken);

            if (!data.Any())
                return null;

            var cauHoi = data.First().CauHoi;

            var cauHois = data
                .Where(x => x.CauHois != null)
                .Select(x => x.CauHois!)
                .Where(x => x.IdCauHoiCha == cauHoi.Id)
                .DistinctBy(x => x.Id)
                .ToList();

            if (!cauHois.Any() && !cauHoi.IdCauHoiCha.HasValue)
            {
                cauHois.Add(cauHoi);
            }

            var response = new CauHoiMucTieuDto
            {
                Id = cauHoi.Id,
                MaCauHoi = cauHoi.MaCauHoi,
                IdCauTruc = cauHoi.IdCauTruc,
                IdLoaiCauHoi = cauHoi.IdLoaiCauHoi,
                IdMucDoNhanThuc = cauHoi.IdMucDoNhanThuc,
                IdTrangThai = cauHoi.TrangThai,
                IsKhongDao = cauHoi.IsKhongDao,
                IsCauHoiCha = cauHoi.IsCauHoiCha,
                DoKho = cauHoi.DoKhoKhoiTao,
                NoiDung = cauHoi.NoiDung,
                Stt = cauHoi.Stt,
                GhiChu = cauHoi.GiaiThich,
                MediaUrl = cauHoi.MediaUrl,
                TieuDeVeTrai = cauHoi.TieuDeVeTrai,
                TieuDeVePhai = cauHoi.TieuDeVePhai,

                CauHois = cauHois
                    .Select(ch => new CauHoisMucTieuDto
                    {
                        Id = ch.Id,
                        MaCauHoi = ch.MaCauHoi,
                        IdCauTruc = ch.IdCauTruc,
                        IdLoaiCauHoi = ch.IdLoaiCauHoi,
                        IdMucDoNhanThuc = ch.IdMucDoNhanThuc,
                        IdTrangThai = ch.TrangThai,
                        IsKhongDao = ch.IsKhongDao,
                        IsCauHoiCha = ch.IsCauHoiCha,
                        IdCauHoiCha = ch.IdCauHoiCha,
                        DoKho = ch.DoKhoKhoiTao,
                        NoiDung = ch.NoiDung,
                        Stt = ch.Stt,
                        GhiChu = ch.GiaiThich,
                        MediaUrl = ch.MediaUrl,
                        TieuDeVeTrai = ch.TieuDeVeTrai,
                        TieuDeVePhai = ch.TieuDeVePhai,

                        CauTraLois = data
                                .Where(data => data.CauTraLois.IdCauHoi == ch.Id)
                                .Select(data => data.CauTraLois)
                                .OrderBy(cauTraLois => cauTraLois.Id)
                                .Select(cauTraLois => new CauTraLoisMucTieuDto
                                {
                                    Id = cauTraLois.Id,
                                    IdCauHoi = cauTraLois.IdCauHoi,
                                    NoiDung = cauTraLois.NoiDung,
                                    IsKhongDao = cauTraLois.IsKhongDao,
                                    IsVeTrai = cauTraLois.IsVeTrai,
                                    ViTriGachChan = cauTraLois.ViTriGachChan,
                                    Stt = cauTraLois.Stt,
                                }).ToList()
                    }).ToList()
            };

            return response;
        }

        public async Task<IList<(CauHoi CauHoi, IEnumerable<CauTraLoi> CauTraLois)>> LayCauHoiByIdsAsync(
            IReadOnlyList<int?> ids,
            CancellationToken cancellationToken)
        {
            var cauHoiQuery = _context.CauHoi
                .Where(ch =>
                    ch.IsVisible &&
                    !ch.IsDeleted &&
                    (
                        ids.Contains(ch.Id) ||
                        (ch.IdCauHoiCha.HasValue && ids.Contains(ch.IdCauHoiCha.Value))
                    ));

            var cauTraLoiQuery = _context.CauTraLoi
                .Where(
                    ch => ch.IsVisible == true &&
                    ch.IsDeleted == false);

            var finalQuery =
                from ch in cauHoiQuery
                join ctl in cauTraLoiQuery on ch.Id equals ctl.IdCauHoi into ctlGroup
                select new { CauHoi = ch, CauTraLois = ctlGroup };

            var items = await finalQuery.ToListAsync(cancellationToken);

            return items.Select(x => (x.CauHoi, x.CauTraLois)).ToList();
        }
    }
}
