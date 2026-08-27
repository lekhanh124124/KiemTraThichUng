// File: KiemTraThichUng.Domain/Entities/CauHinhDanhMuc/CauTruc.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.Entities.CauHinhDanhMuc
{
    public class CauTruc : DisplayEntity
    {
        public string MaCauTruc { get; private set; } = string.Empty;
        public string? TenCauTruc { get; private set; }

        public int IdBoCauHoi { get; private set; }
        public int? IdParent { get; private set; }

        public string? GhiChu { get; private set; }

        protected CauTruc() { }

        public CauTruc(
            string maCauTruc,
            int idBoCauHoi,
            int? idParent,
            string? tenCauTruc,
            string? ghiChu,
            int? stt,
            bool? isVisible)
        {
            if (string.IsNullOrWhiteSpace(maCauTruc))
                throw new DomainValidationException("Mã cấu trúc không hợp lệ.");

            MaCauTruc = maCauTruc;
            IdBoCauHoi = idBoCauHoi;
            IdParent = idParent;

            TenCauTruc = tenCauTruc ?? string.Empty;
            GhiChu = ghiChu ?? string.Empty;

            Initialize(stt, isVisible);
        }

        public void CapNhatThongTin(
            string? tenCauTruc,
            string? maCauTruc,
            string? ghiChu,
            int? stt,
            bool? isVisible)
        {
            TenCauTruc = tenCauTruc ?? TenCauTruc;
            MaCauTruc = maCauTruc ?? MaCauTruc;
            GhiChu = ghiChu ?? GhiChu;

            UpdateDisplay(stt, isVisible);
        }
    }
}
