// File: KiemTraThichUng.Domain/Entities/CauHinhDeKiemTra/CauHinhDeKiemTra.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.ValueObjects;

namespace KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra
{
    public class CauHinhDeKiemTra : ApprovalEntity
    {
        public int IdCauTruc { get; private set; }

        public string MaCauHinhDeKiemTra { get; private set; } = string.Empty;
        public string? TenCauHinhDeKiemTra { get; private set; }
        public int ThoiGianLamBaiGiay { get; private set; }
        public bool IsUsed { get; private set; }

        public double ThetaMin { get; private set; }
        public double ThetaMax { get; private set; }
        public double ThetaKhoiTao { get; private set; }
        public double ThetaDat { get; private set; }
        public double PriorMean { get; private set; }
        public double PriorVariance { get; private set; }
        public double StandardErrorInitial { get; private set; }

        protected CauHinhDeKiemTra()
        {
        }

        public CauHinhDeKiemTra(
            string maCauHinhDeKiemTra,
            string? tenCauHinhDeKiemTra,
            int idCauTruc,
            int? thoiGianLamBaiGiay,
            double? thetaMin,
            double? thetaMax,
            double? thetaKhoiTao,
            double? thetaDat,
            double? priorMean,
            double? priorVariance,
            double? standardErrorInitial,
            bool? isUsed,
            int? stt,
            bool? isVisible)
        {
            MaCauHinhDeKiemTra = maCauHinhDeKiemTra;
            TenCauHinhDeKiemTra = tenCauHinhDeKiemTra ?? string.Empty;
            IdCauTruc = idCauTruc;
            ThoiGianLamBaiGiay = thoiGianLamBaiGiay ?? 2700;
            ThetaMin = thetaMin ?? MucDoNangLuc.VeryLow.MinTheta;
            ThetaMax = thetaMax ?? MucDoNangLuc.VeryHigh.MaxTheta;
            ThetaKhoiTao = thetaKhoiTao ?? MucDoNangLuc.VeryLow.MinTheta;
            ThetaDat = thetaDat ?? MucDoNangLuc.Low.MinTheta;
            PriorMean = priorMean ?? 0;
            PriorVariance = priorVariance ?? 1;
            StandardErrorInitial = standardErrorInitial ?? 3;
            IsUsed = isUsed ?? false;

            Initialize(stt, isVisible);   
        }

        public void CapNhatThongTin(
            string maCauHinhDeKiemTra,
            string? tenCauHinhDeKiemTra,
            int? idCauTruc,
            int? thoiGianLamBaiGiay,
            double? thetaMin,
            double? thetaMax,
            double? thetaKhoiTao,
            double? thetaDat,
            double? priorMean,
            double? priorVariance,
            double? standardErrorInitial,
            int? stt, 
            bool? isVisible)
        {
            MaCauHinhDeKiemTra = maCauHinhDeKiemTra;
            TenCauHinhDeKiemTra = tenCauHinhDeKiemTra ?? TenCauHinhDeKiemTra;
            IdCauTruc = idCauTruc ?? IdCauTruc;
            ThoiGianLamBaiGiay = thoiGianLamBaiGiay ?? ThoiGianLamBaiGiay;
            ThetaMin = thetaMin ?? ThetaMin;
            ThetaMax = thetaMax ?? ThetaMax;
            ThetaKhoiTao = thetaKhoiTao ?? ThetaKhoiTao;
            ThetaDat = thetaDat ?? ThetaDat;
            PriorMean = priorMean ?? PriorMean;
            PriorVariance = priorVariance ?? PriorVariance;
            StandardErrorInitial = standardErrorInitial ?? StandardErrorInitial;

            UpdateDisplay(stt, isVisible);
        }

        public void KetThucDotKiemTra()
        {
            IsUsed = true;
        }
    }
}
