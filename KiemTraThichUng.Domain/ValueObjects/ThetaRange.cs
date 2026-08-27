// File: KiemTraThichUng.Domain/ValueObjects/ThetaRange.cs
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.ValueObjects
{
    public sealed class ThetaRange
    {
        public double GiaTriMin { get; }
        public double GiaTriMax { get; }

        private ThetaRange(double giaTriMin, double giaTriMax)
        {
            if (giaTriMin > giaTriMax)
                throw new DomainValidationException("ThetaMin không được lớn hơn ThetaMax.");

            GiaTriMin = giaTriMin;
            GiaTriMax = giaTriMax;
        }
        public static ThetaRange Create(double min, double max)
            => new(min, max);

        public bool Contains(double theta)
        {
            return theta >= GiaTriMin && theta <= GiaTriMax;
        }
    }
}
