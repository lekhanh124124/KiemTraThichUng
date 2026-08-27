// File: KiemTraThichUng.Domain/Entities/PhienKiemTra/ChiTietLuaChon.cs
using KiemTraThichUng.Domain.Common;

namespace KiemTraThichUng.Domain.Entities.PhienKiemTra
{
    public class ChiTietLuaChon : BaseEntity
    {
        public int IdChiTietKetQuaKiemTra { get; private set; }
        public int IdCauTraLoi { get; private set; }

        public string? NoiDungCauTraLoi { get; private set; }
        public bool IsTraLoiDung { get; private set; }
        public double? PhanTramDiem { get; private set; }

        protected ChiTietLuaChon() { }

        public ChiTietLuaChon(
            int idChiTietKetQua,
            int idCauTraLoi,
            string? noiDung,
            bool isDung,
            double? phanTramDiem)
        {
            IdChiTietKetQuaKiemTra = idChiTietKetQua;
            IdCauTraLoi = idCauTraLoi;
            NoiDungCauTraLoi = noiDung;
            IsTraLoiDung = isDung;
            PhanTramDiem = phanTramDiem;
        }
    }
}
