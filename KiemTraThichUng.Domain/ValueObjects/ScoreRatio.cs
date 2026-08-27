// File: KiemTraThichUng.Domain/ValueObjects/ScoreRatio.cs
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.ValueObjects
{
    public sealed class ScoreRatio
    {
        public double Value { get; }

        private ScoreRatio(double value)
        {
            if (value < 0 || value > 1)
                throw new DomainValidationException("Phần trăm điểm phải nằm trong [0,1].");

            Value = value;
        }

        public static ScoreRatio From(double value)
            => new(value);
    }
}
