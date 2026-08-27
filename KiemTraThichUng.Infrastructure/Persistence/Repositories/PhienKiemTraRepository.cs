using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDuLieuKiemTra;
using KiemTraThichUng.Domain.Entities.CauHinhDanhMuc;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using KiemTraThichUng.Domain.Entities.PhienKiemTra;
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace KiemTraThichUng.Infrastructure.Persistence.Repositories
{
    public class PhienKiemTraRepository : IPhienKiemTraRepository
    {
        private readonly AppDbContext _context;

        public PhienKiemTraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(KetQuaKiemTra KetQua, IList<ChiTietKetQuaKiemTra> ChiTietKetQuas)> LayPhienKiemTraByIdNguoiDungAsync(
            int idNguoiDung,
            CancellationToken cancellationToken)
        {
            var query =
                from ketQua in _context.KetQuaKiemTra
                    .Where(kq =>
                        kq.IsDeleted == false &&
                        kq.IdNguoiDung == idNguoiDung &&
                        kq.TrangThai == TrangThaiKiemTra.DangLam)

                join ct in _context.ChiTietKetQuaKiemTra
                    .Where(ct => ct.IsDeleted == false)
                    on ketQua.Id equals ct.IdKetQuaKiemTra into ctGroup

                select new
                {
                    KetQua = ketQua,
                    ChiTietKetQuaKiemTra = ctGroup.ToList()
                };

            var result = await query.FirstOrDefaultAsync(cancellationToken);

            return (result?.KetQua!, result?.ChiTietKetQuaKiemTra!);
        }

        public async Task<DuLieuKiemTraDto?> LayPhienKiemTraByIdKetQuaKiemTraAsync(
            int idNguoiDung,
            LayDuLieuKiemTraQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                from ketQua in _context.KetQuaKiemTra.AsNoTracking()

                where !ketQua.IsDeleted
                      && ketQua.IdNguoiDung == idNguoiDung
                      && ketQua.TrangThai == TrangThaiKiemTra.HoanThanh
                      && ketQua.Id ==
                            (
                                request.IdKetQuaKiemTra ??
                                _context.KetQuaKiemTra
                                    .Where(x => x.IsDeleted == false && x.IdNguoiDung == idNguoiDung)
                                    .OrderByDescending(x => x.Id)
                                    .Select(x => x.Id)
                                    .FirstOrDefault()
                            )

                join chiTiet in _context.ChiTietKetQuaKiemTra.AsNoTracking()
                        .Where(ct => ct.IsDeleted == false && ct.TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                    on ketQua.Id equals chiTiet.IdKetQuaKiemTra
                    into nhomChiTiet
                from chiTiet in nhomChiTiet.DefaultIfEmpty()

                join luaChon in _context.ChiTietLuaChon.AsNoTracking()
                        .Where(lc => lc.IsDeleted == false)
                    on chiTiet.Id equals luaChon.IdChiTietKetQuaKiemTra
                    into nhomLuaChon
                from luaChon in nhomLuaChon.DefaultIfEmpty()

                select new
                {
                    // KetQua
                    ketQua.Id,
                    ketQua.IdNguoiDung,
                    ketQua.IdCauHinhDeKiemTra,
                    ketQua.ThoiGianBatDau,
                    ketQua.ThoiGianKetThuc,
                    ketQua.DiemSo,
                    ketQua.Theta,
                    ketQua.StandardError,

                    // ChiTiet
                    ChiTietId = chiTiet != null ? chiTiet.Id : (int?)null,
                    IdCauHoi = chiTiet != null ? chiTiet.IdCauHoi : (int?)null,
                    IdCauHoiCha = chiTiet != null ? chiTiet.IdCauHoiCha : (int?)null,
                    IsTraLoiDung = chiTiet != null ? chiTiet.IsTraLoiDung : (bool?)null,
                    PhanTramDiem = chiTiet != null ? chiTiet.PhanTramDiem : (double?)null,
                    DoKhoLucThi = chiTiet != null ? chiTiet.DoKhoLucThi : (double?)null,
                    ThetaBefore = chiTiet != null ? chiTiet.ThetaBefore : (double?)null,
                    ThetaAfter = chiTiet != null ? chiTiet.ThetaAfter : (double?)null,
                    ThetaTarget = chiTiet != null ? chiTiet.ThetaTarget : (double?)null,
                    StandardErrorBefore = chiTiet != null ? chiTiet.StandardErrorBefore : (double?)null,
                    StandardErrorAfter = chiTiet != null ? chiTiet.StandardErrorAfter : (double?)null,
                    ThongTinCauHoi = chiTiet != null ? chiTiet.ThongTinCauHoi : (double?)null,
                    ThongTinTichLuyBefore = chiTiet != null ? chiTiet.ThongTinTichLuyBefore : (double?)null,
                    ThongTinTichLuyAfter = chiTiet != null ? chiTiet.ThongTinTichLuyAfter : (double?)null,

                    // LuaChon
                    IdCauTraLoi = luaChon != null ? luaChon.IdCauTraLoi : (int?)null,
                    NoiDungCauTraLoi = luaChon != null ? luaChon.NoiDungCauTraLoi : null
                };

            var danhSachDong = await query.ToListAsync(cancellationToken);

            var sql = query.ToQueryString();
            Console.WriteLine(sql);

            if (danhSachDong.Count == 0)
                return null;

            var thongTinChung = danhSachDong.First();

            var danhSachChiTiet = danhSachDong
                .Where(x => x.ChiTietId.HasValue)
                .GroupBy(x => x.ChiTietId)
                .Select(nhom => nhom.First())
                .Select(x => new KetQuaDanhGiaDapAn
                {
                    IdCauHoi = x.IdCauHoi!.Value,
                    IdCauHoiCha = x.IdCauHoiCha,
                    IdCauTraLoi = x.IdCauTraLoi,
                    NoiDungCauTraLoi = x.NoiDungCauTraLoi,
                    IsCorrect = x.IsTraLoiDung ?? false,
                    ScoreRatio = x.PhanTramDiem ?? 0,
                    CurrentDifficulty = x.DoKhoLucThi ?? 0,
                    ThetaBefore = x.ThetaBefore ?? 0,
                    ThetaAfter = x.ThetaAfter ?? 0,
                    TargetTheta = x.ThetaTarget ?? 0,
                    StandardErrorBefore = x.StandardErrorBefore ?? 0,
                    StandardErrorAfter = x.StandardErrorAfter ?? 0,
                    CurrentItemInformation = x.ThongTinCauHoi ?? 0,
                    CumulativeInformationBefore = x.ThongTinTichLuyBefore ?? 0,
                    CumulativeInformationAfter = x.ThongTinTichLuyAfter ?? 0
                })
                .ToList();

            return new DuLieuKiemTraDto
            {
                IdNguoiDung = thongTinChung.IdNguoiDung,
                IdKetQuaKiemTra = thongTinChung.Id,
                IdCauHinhDeKiemTra = thongTinChung.IdCauHinhDeKiemTra,
                ThoiGianBatDau = thongTinChung.ThoiGianBatDau,
                ThoiGianKetThuc = thongTinChung.ThoiGianKetThuc,
                DiemSo = thongTinChung.DiemSo,
                Theta = thongTinChung.Theta,
                StandardError = thongTinChung.StandardError,
                ThetaMin = danhSachChiTiet.Any() ? danhSachChiTiet.Min(x => x.ThetaAfter) : 0,
                ThetaMax = danhSachChiTiet.Any() ? danhSachChiTiet.Max(x => x.ThetaAfter) : 0,
                ChiTietKetQuas = danhSachChiTiet
            };
        }

        public async Task<KetQuaKiemTra> TaoPhienKiemTraAsync(
            KetQuaKiemTra ketQuaKiemTra,
            CancellationToken cancellationToken)
        {
            var result = await _context.KetQuaKiemTra.AddAsync(ketQuaKiemTra);
            return result.Entity;
        }

        public async Task TaoChiTietKetQuaKiemTraAsync(
            IReadOnlyList<ChiTietKetQuaKiemTra> chiTietKetQuaKiemTras,
            CancellationToken cancellationToken)
        {
            await _context.ChiTietKetQuaKiemTra.AddRangeAsync(chiTietKetQuaKiemTras, cancellationToken);
        }
        public async Task<IReadOnlyList<(int? IdLoaiCauHoi, int? IdMucDoNhanThuc, int SoLuong)>> LayCauHinhDaLamByIdKetQuaKiemTraAsync(
            int idKetQuaKiemTra,
            CancellationToken cancellationToken)
        {
            var effectiveQuestionIdsQuery =
                _context.ChiTietKetQuaKiemTra
                    .AsNoTracking()
                    .Where(ct =>
                        ct.IdKetQuaKiemTra == idKetQuaKiemTra &&
                        !ct.IsDeleted &&
                        ct.TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                    .Select(ct => ct.IdCauHoiCha ?? ct.IdCauHoi)
                    .Distinct();

            var result =
                await (
                    from ch in _context.CauHoi.AsNoTracking()
                    where effectiveQuestionIdsQuery.Contains(ch.Id)
                          && !ch.IsDeleted
                    group ch by new
                    {
                        ch.IdLoaiCauHoi,
                        ch.IdMucDoNhanThuc
                    }
                    into g
                    select new
                    {
                        g.Key.IdLoaiCauHoi,
                        g.Key.IdMucDoNhanThuc,
                        SoLuong = g.Count()
                    }
                ).ToListAsync(cancellationToken);

            return result
                .Select(x => (
                    (int?)x.IdLoaiCauHoi,
                    x.IdMucDoNhanThuc,
                    x.SoLuong))
                .ToList();
        }

        public async Task TaoChiTietLuaChonDapAnAsync(
            IReadOnlyList<ChiTietLuaChon> chiTietLuaChonDapAns,
            CancellationToken cancellationToken)
        {
            await _context.ChiTietLuaChon.AddRangeAsync(chiTietLuaChonDapAns, cancellationToken);
        }

        public async Task<(bool IsThieu, IReadOnlyList<int> CauHoisThieu)> KiemTraCauTraLoiThieuAsync(
            int idNguoiDung,
            IReadOnlyList<int?>? idsCauHoi, 
            CancellationToken cancellationToken)
        {
            if (idsCauHoi == null || idsCauHoi.Count == 0)
            {
                return (false, Array.Empty<int>());
            }

            var idsCauHoiDaChon = idsCauHoi
                .Where(x => x.HasValue)
                .Select(x => x!.Value)
                .Distinct()
                .ToList();

            if (idsCauHoiDaChon.Count == 0)
            {
                return (false, Array.Empty<int>());
            }

            var query =
                from chiTietKetQua in _context.ChiTietKetQuaKiemTra
                join ketQua in _context.KetQuaKiemTra
                    on chiTietKetQua.IdKetQuaKiemTra equals ketQua.Id

                where
                    chiTietKetQua.IsDeleted == false &&
                    chiTietKetQua.TrangThai == TrangThaiChiTietKetQua.DaGiao &&
                    ketQua.IdNguoiDung == idNguoiDung

                select chiTietKetQua.IdCauHoi;

            var idCauHoiDaGiao = await query.Distinct().ToArrayAsync(cancellationToken);

            var idsCauHoiThieu = idCauHoiDaGiao.Except(idsCauHoiDaChon).ToList();

            var isThieu = idsCauHoiThieu.Count > 0;

            return (isThieu, idsCauHoiThieu);
        }

        public async Task<(bool IsHoanThanh, CauHinhDeKiemTra? CauHinh)> KiemTraDaHoanThanhAsync(
            int idNguoiDung, 
            CancellationToken cancellationToken)
        {
            var query =
                from ketQua in _context.KetQuaKiemTra
                join cauHinh in _context.CauHinhDeKiemTra
                    on ketQua.IdCauHinhDeKiemTra equals cauHinh.Id
                where ketQua.IdNguoiDung == idNguoiDung &&
                      ketQua.IsDeleted == false &&
                      ketQua.TrangThai == TrangThaiKiemTra.DangLam
                select cauHinh;

            var cauHinhChuaHoanThanh = await query.FirstOrDefaultAsync(cancellationToken);

            var isHoanThanh = cauHinhChuaHoanThanh == null; // Hoàn thành khi không còn cấu trúc nào chưa hoàn thành

            return (isHoanThanh, cauHinhChuaHoanThanh);
        }

        public async Task<IReadOnlyList<PhienKiemTraItemDto>> LayDanhSachKetQuaKiemTraByIdNguoiDungAsync(
            int idNguoiDung, 
            CancellationToken cancellationToken)
        {
            var query =
                from kq in _context.KetQuaKiemTra.AsNoTracking()
                join p in _context.CauHinhDeKiemTra.AsNoTracking()
                    on kq.IdCauHinhDeKiemTra equals p.Id
                where kq.IdNguoiDung == idNguoiDung &&
                      kq.IsDeleted == false
                orderby kq.Id descending
                select new
                {
                    kq.Id,
                    kq.IdNguoiDung,
                    kq.IdCauHinhDeKiemTra,
                    p.TenCauHinhDeKiemTra,
                    p.IdCauTruc,
                    p.ThetaDat,
                    kq.ThoiGianBatDau,
                    kq.ThoiGianKetThuc,
                    kq.TrangThai,
                    kq.IsDat,
                    kq.DiemSo,
                    kq.Theta,
                    kq.StandardError,
                };
            var rows = await query.ToListAsync(cancellationToken);

            var result = rows.Select(kq => new PhienKiemTraItemDto
            {
                IdNguoiDung = kq.IdNguoiDung,
                IdKetQuaKiemTra = kq.Id,
                IdCauHinhDeKiemTra = kq.IdCauHinhDeKiemTra,
                TenCauHinhDeKiemTra = kq.TenCauHinhDeKiemTra,
                idCauTruc = kq.IdCauTruc,
                ThoiGianBatDau = kq.ThoiGianBatDau,
                ThoiGianKetThuc = kq.ThoiGianKetThuc,
                IsDangLam = kq.TrangThai == TrangThaiKiemTra.HoanThanh ? false : true,
                IsDat = kq.IsDat,
                TenMucNangLucDat = MucDoNangLuc.FromTheta(kq.ThetaDat).Name,
                MucNangLuc = kq.Theta != null ? MucDoNangLuc.FromTheta((double)kq.Theta).Id : null,
                TenMucNangLuc = kq.Theta != null ? MucDoNangLuc.FromTheta((double)kq.Theta).Name : null,
                DiemSo = kq.DiemSo,
                Theta = kq.Theta,
                StandardError = kq.StandardError,
            }).ToList();

            return result;
        }
    }
}
