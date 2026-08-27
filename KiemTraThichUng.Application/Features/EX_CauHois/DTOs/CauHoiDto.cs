// File: KiemTraThichUng.Application/Features/EX_CauHois/DTOs/CauHoisDto.cs
using KiemTraThichUng.Domain.Enums;

namespace KiemTraThichUng.Application.Features.EX_CauHois.DTOs
{
    public class CauHoiDto
    {
        public int? Id { get; set; }
        public string? MaCauHoi { get; set; }
        public int? IdCauTruc { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public int? IdMucDoNhanThuc { get; set; }
        public TrangThaiDuyet? IdTrangThai { get; set; }
        public bool? IsKhongDao { get; set; }
        public bool? IsCauHoiCha { get; set; }
        public double? DoKho { get; set; }
        public double? DoKhoKhoiTao { get; set; }
        public string? NoiDung { get; set; }
        public int? Stt { get; set; }
        public string? GhiChu { get; set; }
        public string? MediaUrl { get; set; }
        public string? TieuDeVeTrai { get; set; }
        public string? TieuDeVePhai { get; set; }
        public List<CauHoisDto>? CauHois { get; set; }
    }
}
