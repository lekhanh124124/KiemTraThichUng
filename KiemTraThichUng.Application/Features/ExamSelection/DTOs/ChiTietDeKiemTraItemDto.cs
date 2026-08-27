namespace KiemTraThichUng.Application.Features.ExamSelection.DTOs
{
    public class ChiTietDeKiemTraItemDto
    {
        public int? Id { get; set; }
        public int? IdCauHinhDeKiemTra { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public string? TenLoaiCauHoi { get; set; }
        public int? IdMucDoNhanThuc { get; set; }
        public string? TenMucDoNhanThuc { get; set; }
        public int? SoLuongCauHoi { get; set; }
    }
}
