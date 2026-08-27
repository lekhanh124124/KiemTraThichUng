// File: KiemTraThichUng.Domain/Entities/PhienKiemTra/KetQuaKiemTra.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.Entities.PhienKiemTra
{
    public class KetQuaKiemTra : BaseEntity
    {
        public int IdNguoiDung { get; private set; }
        public int IdCauHinhDeKiemTra { get; private set; }

        public DateTime ThoiGianBatDau { get; private set; }
        public DateTime? ThoiGianKetThuc { get; private set; }

        public bool? IsDat { get; private set; }
        public double? DiemSo { get; private set; }
        public double? Theta { get; private set; }
        public double? StandardError { get; private set; }

        public int? IdCauHoiHienTai { get; private set; }

        public TrangThaiKiemTra TrangThai { get; private set; }

        protected KetQuaKiemTra() { } 

        public KetQuaKiemTra(
            int idNguoiDung,
            int idCauHinhDeKiemTra)
        {
            IdNguoiDung = idNguoiDung;
            IdCauHinhDeKiemTra = idCauHinhDeKiemTra;
            ThoiGianBatDau = DateTime.Now;
            TrangThai = TrangThaiKiemTra.DangLam;
        }

        public void CapNhatCauHoiHienTai(int? idCauHoi)
        {
            IdCauHoiHienTai = idCauHoi ?? IdCauHoiHienTai;
        }

        public void HoanThanh(
            bool? isDat, 
            double? diemSo, 
            double? theta, 
            double? standardError)
        {
            if (TrangThai == TrangThaiKiemTra.HoanThanh)
                throw new DomainValidationException("Bài thi đã hoàn thành.");

            TrangThai = TrangThaiKiemTra.HoanThanh;
            ThoiGianKetThuc = DateTime.Now;
            IsDat = isDat ?? IsDat;
            Theta = theta ?? Theta;
            StandardError = standardError ?? StandardError;
            DiemSo = diemSo;
            IdCauHoiHienTai = null;
        }
    }
}
