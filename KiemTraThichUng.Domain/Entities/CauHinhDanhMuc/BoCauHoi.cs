// File: KiemTraThichUng.Domain/Entities/CauHinhDanhMuc/BoCauHoi.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.Entities.CauHinhDanhMuc
{
    public class BoCauHoi : AuditableEntity
    {
        public string MaBoCauHoi { get; private set; } = string.Empty;
        public string? TenBoCauHoi { get; private set; }
        public string? TaiLieuThamKhao { get; private set; }
        public string? GhiChu { get; private set; }

        public bool IsLocked { get; private set; }

        protected BoCauHoi() { }

        public BoCauHoi(
            string maBoCauHoi,
            string? tenBoCauHoi,
            string? taiLieuThamKhao,
            string? ghiChu,
            bool? isLocked)
        {
            if (string.IsNullOrWhiteSpace(maBoCauHoi))
                throw new DomainValidationException("Mã bộ câu hỏi không được rỗng.");

            MaBoCauHoi = maBoCauHoi;

            TenBoCauHoi = tenBoCauHoi ?? string.Empty;
            TaiLieuThamKhao = taiLieuThamKhao ?? string.Empty;
            GhiChu = ghiChu ?? string.Empty;
            IsLocked = isLocked ?? false;
        }

        public void CapNhatThongTin(
            string? tenBoCauHoi,
            string? taiLieuThamKhao,
            string? ghiChu,
            int idNguoiCapNhat)
        {
            TenBoCauHoi = tenBoCauHoi ?? TenBoCauHoi;
            TaiLieuThamKhao = taiLieuThamKhao ?? TaiLieuThamKhao;
            GhiChu = ghiChu ?? GhiChu;
        }

        public void Lock()
        {
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }
    }
}
