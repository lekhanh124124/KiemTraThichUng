// File: KiemTraThichUng.Domain/Entities/NganHangCauHoi/CauTraLoi.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Exceptions;
using KiemTraThichUng.Domain.ValueObjects;

namespace KiemTraThichUng.Domain.Entities.NganHangCauHoi
{
    public class CauTraLoi : DisplayEntity
    {
        public int IdCauHoi { get; private set; }

        public string MaCauTraLoi { get; private set; } = string.Empty;
        public string? NoiDung { get; private set; }

        public bool IsDung { get; private set; }
        public double? PhanTramDiem { get; private set; }

        public bool IsKhongDao { get; private set; }
        public bool IsVeTrai { get; private set; }
        public int ViTriGachChan { get; private set; }
        public bool IsThietLapRieng { get; private set; }

        protected CauTraLoi() { }

        public CauTraLoi(
            string maCauTraLoi,
            string? noiDung,
            bool? isDung,
            double? phanTramDiem,
            bool? isKhongDao,
            bool? isVeTrai,
            int? viTriGachChan,
            bool? isThietLapRieng,
            int? stt,
            bool? isVisible)
        {
            if (string.IsNullOrWhiteSpace(maCauTraLoi))
                throw new DomainValidationException("Mã câu trả lời không hợp lệ.");

            MaCauTraLoi = maCauTraLoi;
            NoiDung = noiDung ?? string.Empty;
            IsDung = isDung ?? false;

            IsKhongDao = isKhongDao ?? false;
            IsVeTrai = isVeTrai ?? false;
            ViTriGachChan = viTriGachChan ?? 0;
            IsThietLapRieng = isThietLapRieng ?? false;

            if (IsThietLapRieng && phanTramDiem.HasValue)
            {
                var ratio = ScoreRatio.From(phanTramDiem.Value);
                PhanTramDiem = ratio.Value;
            }

            Initialize(stt, isVisible);
        }

        public void CapNhatThongTin(
            string? noiDung,
            bool? isDung,
            double? phanTramDiem,
            bool? isKhongDao,
            bool? isVeTrai,
            int? viTriGachChan,
            bool? isThietLapRieng,
            int? stt,
            bool? isVisible)
        {
            NoiDung = noiDung ?? NoiDung;
            IsDung = isDung ?? IsDung;
            PhanTramDiem = phanTramDiem ?? PhanTramDiem;
            IsKhongDao = isKhongDao ?? IsKhongDao;
            IsVeTrai = isVeTrai ?? IsVeTrai;
            ViTriGachChan = viTriGachChan ?? ViTriGachChan;
            IsThietLapRieng = isThietLapRieng ?? IsThietLapRieng;
            UpdateDisplay(stt, isVisible);
        }

        public void ThemCauTraLoi(int? idCauHoi)
        { 
            if (idCauHoi == null)
                throw new DomainValidationException("Id câu hỏi không hợp lệ.");
            IdCauHoi = (int)idCauHoi;
        }
    }
}
