// File: KiemTraThichUng.Application/Features/EX_CauHois/DTOs/CauTraLoisDto.cs
namespace KiemTraThichUng.Application.Features.EX_CauHois.DTOs
{
    public class CauTraLoisDto
    {
        public int? Id { get; set; }
        public int? ViTriGachChan { get; set; }
        public int? IdCauHoi { get; set; }
        public bool? IsDung { get; set; }
        public bool? IsKhongDao { get; set; }
        public bool? IsVeTrai { get; set; }
        public int? Stt { get; set; }
        public bool? IsVisible { get; set; }
        public string? NoiDung { get; set; }
        public double? PhanTramDiem { get; set; }
        public bool? IsThietLapRieng { get; set; }
    }
}
