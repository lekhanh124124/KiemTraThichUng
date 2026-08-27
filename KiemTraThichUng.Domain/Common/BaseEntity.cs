// File: KiemTraThichUng.Domain/Common/BaseEntity.cs
namespace KiemTraThichUng.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
