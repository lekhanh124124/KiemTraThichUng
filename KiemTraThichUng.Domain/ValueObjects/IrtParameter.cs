// File: KiemTraThichUng.Domain/ValueObjects/IrtParameter.cs
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.ValueObjects
{
    public sealed class IrtParameter
    {
        public double DoKho { get; }
        public double DoPhanLoai { get; }

        private IrtParameter(double doKho, double doPhanLoai)
        {
            if (doPhanLoai <= 0)
                throw new DomainValidationException("Độ phân loại phải > 0.");

            DoKho = doKho;
            DoPhanLoai = doPhanLoai;
        }

        public static IrtParameter Create(double doKho, double doPhanLoai)
            => new(doKho, doPhanLoai);
    }
}
