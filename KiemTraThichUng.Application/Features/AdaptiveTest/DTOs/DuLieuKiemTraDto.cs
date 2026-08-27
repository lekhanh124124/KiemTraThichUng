namespace KiemTraThichUng.Application.Features.AdaptiveTest.DTOs
{
    public class DuLieuKiemTraDto
    {
        public int? IdNguoiDung { get; set; }
        public int? IdKetQuaKiemTra { get; set; }
        public int? IdCauHinhDeKiemTra { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public double? DiemSo { get; set; }
        public double? Theta { get; set; }
        public double? StandardError { get; set; }
        public double? ThetaMin { get; set; }
        public double? ThetaMax { get; set; }
        public IReadOnlyList<KetQuaDanhGiaDapAn>? ChiTietKetQuas { get; set; }
    }
}
