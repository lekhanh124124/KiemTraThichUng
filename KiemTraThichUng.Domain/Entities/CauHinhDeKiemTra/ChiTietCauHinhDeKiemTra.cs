// File: KiemTraThichUng.Domain/Entities/CauHinhDeKiemTra/ChiTietCauHinhDeKiemTra.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;

namespace KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra
{
    public class ChiTietCauHinhDeKiemTra : DisplayEntity
    {
        public int IdCauHinhDeKiemTra { get; private set; }
        public int? IdLoaiCauHoi { get; private set; }
        public LoaiCauHoi? LoaiCauHoi
            => IdLoaiCauHoi.HasValue
                ? LoaiCauHoi.FromId(IdLoaiCauHoi.Value)
                : null;
        public int? IdMucDoNhanThuc { get; private set; }
        public MucDoNhanThuc? MucDoNhanThuc
            => IdMucDoNhanThuc.HasValue
                ? MucDoNhanThuc.FromId(IdMucDoNhanThuc.Value)
                : null;
        public int SoLuongCauHoi { get; private set; }

        protected ChiTietCauHinhDeKiemTra() { }

        public ChiTietCauHinhDeKiemTra(
            int idCauHinhDeKiemTra,
            LoaiCauHoi? loaiCauHoi,
            MucDoNhanThuc? mucDoNhanThuc,
            int soLuongCauHoi,
            int? stt,
            bool? isVisible)
        {
            IdCauHinhDeKiemTra = idCauHinhDeKiemTra;
            IdLoaiCauHoi = loaiCauHoi?.Id;
            IdMucDoNhanThuc = mucDoNhanThuc?.Id;
            SoLuongCauHoi = soLuongCauHoi;
            Initialize(stt, isVisible);
        }
        public void CapjNhatThongTin(
            LoaiCauHoi? loaiCauHoi,
            MucDoNhanThuc? mucDoNhanThuc,
            int? soLuongCauHoi,
            int? stt,
            bool? isVisible)
        {
            IdLoaiCauHoi = loaiCauHoi?.Id ?? IdLoaiCauHoi;
            IdMucDoNhanThuc = mucDoNhanThuc?.Id ?? IdMucDoNhanThuc;
            SoLuongCauHoi = soLuongCauHoi ?? SoLuongCauHoi;
            UpdateDisplay(stt, isVisible);
        }
    }
}
