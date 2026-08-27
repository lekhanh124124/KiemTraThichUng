using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiemTraThichUng.Infrastructure.Persistence.Configurations
{
    public class CauHinhDeKiemTraConfiguration : IEntityTypeConfiguration<CauHinhDeKiemTra>
    {
        public void Configure(EntityTypeBuilder<CauHinhDeKiemTra> builder)
        {
            builder.Property(x => x.MaCauHinhDeKiemTra)
                .HasMaxLength(15)
                .HasComputedColumnSql(
                    "RIGHT('0' + CAST(YEAR(NgayTao) % 100 AS VARCHAR(2)), 2) " +
                    "+ RIGHT('0' + CAST(MONTH(NgayTao) AS VARCHAR(2)), 2) " +
                    "+ RIGHT('0' + CAST(DAY(NgayTao) AS VARCHAR(2)), 2) " +
                    "+ RIGHT(REPLICATE('0',9) + CAST(Id AS VARCHAR(9)),9)",
                    stored: true)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
