// File: KiemTraThichUng.Infrastructure/Persistence/AppDbContext.cs
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Entities.CauHinhDanhMuc;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.Entities.PhienKiemTra;
using KiemTraThichUng.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KiemTraThichUng.Infrastructure.Persistence
{
    public class AppDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>,
          IUnitOfWork
    {
        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<BoCauHoi> BoCauHoi { get; set; }
        public DbSet<CauTruc> CauTruc { get; set; }
        public DbSet<CauHoi> CauHoi { get; set; }
        public DbSet<CauTraLoi> CauTraLoi { get; set; }
        public DbSet<CauHinhDeKiemTra> CauHinhDeKiemTra { get; set; }
        public DbSet<ChiTietCauHinhDeKiemTra> ChiTietCauHinhDeKiemTra { get; set; }
        public DbSet<KetQuaKiemTra> KetQuaKiemTra { get; set; }
        public DbSet<ChiTietKetQuaKiemTra> ChiTietKetQuaKiemTra { get; set; }
        public DbSet<ChiTietLuaChon> ChiTietLuaChon { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ===== Rename Identity Tables =====
            builder.Entity<ApplicationUser>().ToTable("NguoiDung");
            builder.Entity<IdentityUserLogin<int>>().ToTable("NguoiDungLogin");
            builder.Entity<IdentityRole<int>>().ToTable("VaiTro");
            builder.Entity<IdentityUserRole<int>>().ToTable("NguoiDungVaiTro");
            builder.Entity<IdentityUserClaim<int>>().ToTable("NguoiDungClaim");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("VaiTroClaim");
            builder.Entity<IdentityUserToken<int>>().ToTable("NguoiDungToken");

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;

            var entries = ChangeTracker.Entries<AuditableEntity>();

            var idNguoiDung = _currentUserService.UserId;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.Create(idNguoiDung, now);

                if (entry.State == EntityState.Modified)
                    entry.Entity.Modify(idNguoiDung, now);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
