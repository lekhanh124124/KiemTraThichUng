// File: KiemTraThichUng.Domain/ValueObjects/MucDoNangLuc.cs

using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.ValueObjects
{
    public sealed class MucDoNangLuc : Enumeration
    {
        public double MinTheta { get; }
        public double MaxTheta { get; }

        private MucDoNangLuc(int id, string name, double minTheta, double maxTheta)
            : base(id, name)
        {
            MinTheta = minTheta;
            MaxTheta = maxTheta;
        }

        public double MidTheta => (MinTheta + MaxTheta) / 2;

        public static readonly MucDoNangLuc VeryLow =
            new(1, "VeryLow", -3.0, -1.5);

        public static readonly MucDoNangLuc Low =
            new(2, "Low", -1.5, 0.0);

        public static readonly MucDoNangLuc Medium =
            new(3, "Medium", 0.0, 1.0);

        public static readonly MucDoNangLuc High =
            new(4, "High", 1.0, 2.0);

        public static readonly MucDoNangLuc VeryHigh =
            new(5, "VeryHigh", 2.0, 3.0);

        public static MucDoNangLuc FromTheta(double theta)
        {
            var result = GetAll<MucDoNangLuc>()
                .FirstOrDefault(x =>
                    theta >= x.MinTheta &&
                    (theta < x.MaxTheta || x == VeryHigh));

            if (result is null)
                throw new DomainValidationException(
                    $"Theta {theta} nằm ngoài phạm vi năng lực hợp lệ.");

            return result;
        }

        public static MucDoNangLuc FromId(int value)
        {
            try
            {
                return FromId<MucDoNangLuc>(value);
            }
            catch
            {
                throw new DomainValidationException(
                    $"Giá trị mức độ năng lực {value} không hợp lệ.");
            }
        }

        public static double ConvertThetaToScore10(
            double theta,
            double thetaMin,
            double thetaMax,
            int decimalPlaces = 2)
        {
            if (thetaMax <= thetaMin)
                throw new DomainValidationException(
                    "thetaMax phải lớn hơn thetaMin.");

            // Clamp
            if (theta < thetaMin)
                theta = thetaMin;

            if (theta > thetaMax)
                theta = thetaMax;

            var normalized = (theta - thetaMin) / (thetaMax - thetaMin);

            var score = normalized * 10d;

            return Math.Round(score, decimalPlaces);
        }

        public bool IsDat(MucDoNangLuc mucChuan)
        {
            return MinTheta >= mucChuan.MinTheta;
        }
    }
}