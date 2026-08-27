namespace KiemTraThichUng.Application.Features.ExamSelection.DTOs
{
    public class DeKiemTraitemDto
    {
        public int? Id { get; set; }
        public int? IdCauTruc { get; set; }
        public string? TenCauHinhDeKiemTra { get; set; }
        public string? MaCauHinhDeKiemTra { get; set; }
        public int? ThoiGianLamBaiGiay { get; set; }
        public string? DoKhoMin { get; set; }
        public string? DoKhoMax { get; set; }
        public string? MucNangLucDat { get; set; }
        public int? Stt { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public bool? IsDangLam { get; set; }
    }
}
