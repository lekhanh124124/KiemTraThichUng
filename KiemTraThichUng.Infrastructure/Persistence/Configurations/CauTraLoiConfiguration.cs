//// File: KiemTraThichUng.Infrastructure/Persistence/Configurations/CauTraLoiConfiguration.cs
//using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace KiemTraThichUng.Infrastructure.Persistence.Configurations
//{
//    public class CauTraLoiConfiguration : IEntityTypeConfiguration<CauTraLoi>
//    {
//        public void Configure(EntityTypeBuilder<CauTraLoi> builder)
//        {
//            builder.HasKey(x => x.Id);

//            builder.Property(x => x.IdCauHoi)
//                   .IsRequired();

//            builder.HasIndex(x => x.IdCauHoi);
//        }
//    }
//}
