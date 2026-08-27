namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.CreateCauHoi
{
    public class CreateCauHoiResponse
    {
        public int? Id { get; set; }
        public string? MaCauHoi { get; set; }
        public int? IdCauTruc { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public int? IdCauHoiCha { get; set; }
        public int? IdMucDoNhanThuc { get; set; }
        public int? IdTrangThai { get; set; }
        public string? MediaUrl { get; set; }
        public int? IdNguoiDuyet { get; set; }
        public int? IdNguoiSoan { get; set; }
        public bool? IsKhongDao { get; set; }
        public bool? IsCauHoiCha { get; set; }
        public string? TieuDeVeTrai { get; set; }
        public string? TieuDeVePhai { get; set; }
        public int? Stt { get; set; }
        public int? SttCauHoiCon { get; set; }
        public double? DoKho { get; set; }
        public double? DoKhoKhoiTao { get; set; }
        public Guid? CauHoiGuid { get; set; }
        public string? GhiChu { get; set; }
        public string? GhiChuDuyet { get; set; }
        public DateTime? NgaySoan { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public bool? IsVisible { get; set; }
    }
}
