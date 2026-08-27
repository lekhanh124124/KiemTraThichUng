using KiemTraThichUng.Domain.Enums;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.KetThucKiemTra
{
    public class KetThucKiemTraResponse 
    {
        public int Id { get; set; }
        public int? IdNguoiDung { get; set; }
        public int? IdCauHinhDeKiemTra { get; set; } 
        public string? DoKhoMin {  get; set; }
        public string? DoKhoMax { get; set; }
        public string? MucNangLucDat { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public double? DiemSo { get; set; }
        public bool? IsDat { get; set; }
        public int? MucNangLuc { get; set; }
        public string? MaMucNangLuc { get; set; }
        public double? DiemNangLuc { get; set; } 
        public double? SaiSoUocLuong { get; set; }
        public int? IdCauHoiHienTai { get; set; } 
        public TrangThaiKiemTra? TrangThai { get; set; } 
    }
}
