// File: KiemTraThichUng.Domain/Common/AuditableEntity.cs
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public int IdNguoiTao { get; protected set; }
        public DateTime NgayTao { get; protected set; }

        public int? IdNguoiCapNhat { get; protected set; }
        public DateTime? NgayCapNhat { get; protected set; }

        public void Create(int idNguoiTao, DateTime now)
        {
            IdNguoiTao = idNguoiTao;
            NgayTao = now;
        }

        public void Modify(int? idNguoiCapNhat, DateTime now)
        {
            IdNguoiCapNhat = idNguoiCapNhat ?? 0;
            NgayCapNhat = now;
        }
    }
}
