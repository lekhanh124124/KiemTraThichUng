// File: KiemTraThichUng.Domain/Common/ApprovalEntity.cs
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.Common
{
    public abstract class ApprovalEntity : DisplayEntity
    {
        public TrangThaiDuyet TrangThai { get; protected set; } = TrangThaiDuyet.TaoMoi;

        public int? IdNguoiDuyet { get; protected set; }
        public DateTime? NgayDuyet { get; protected set; }
        public string? GhiChuDuyet { get; protected set; }

        public void DeXuatDuyet()
        {
            if (TrangThai != TrangThaiDuyet.TaoMoi)
                throw new DomainValidationException("Không thể đề xuất duyệt trạng thái khác tạo mới.");

            TrangThai = TrangThaiDuyet.DeXuatDuyet;
        }

        public void Duyet(int idNguoiDuyet, DateTime now, string? ghiChu = null)
        {
            if (TrangThai != TrangThaiDuyet.DeXuatDuyet)
                throw new DomainValidationException("Không thể duyệt trạng thái khác đề xuất duyệt.");

            TrangThai = TrangThaiDuyet.DaDuyet;
            IdNguoiDuyet = idNguoiDuyet;
            NgayDuyet = now;
            GhiChuDuyet = ghiChu;
        }
    }
}
