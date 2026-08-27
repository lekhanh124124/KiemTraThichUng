namespace KiemTraThichUng.Application.Features.AdaptiveTest.DTOs
{
    public class PhienKiemTraItemDto
    {
        public int? IdNguoiDung { get; set; }
        public int? IdKetQuaKiemTra { get; set; }
        public int? IdCauHinhDeKiemTra { get; set; }
        public string? TenCauHinhDeKiemTra { get; set; }
        public int? idCauTruc { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public bool? IsDangLam { get; set; }
        public string? TenMucNangLucDat { get; set; }
        public bool? IsDat { get; set; }
        public int? MucNangLuc { get; set; }
        public string? TenMucNangLuc { get; set; }
        public double? DiemSo { get; set; }
        public double? Theta { get; set; }
        public double? StandardError { get; set; }
    }
}
