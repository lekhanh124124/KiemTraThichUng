namespace KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.DTOs
{
    public class CauHinhDeKiemTraDto
    {
        public int Id { get; set; }
        public int IdCauTruc { get; set; }
        public string MaCauHinhDeKiemTra { get; set; } = string.Empty;
        public string? TenCauHinhDeKiemTra { get; set; }
        public int ThoiGianLamBaiGiay { get; set; }
        public bool IsUsed { get; set; }

        public double ThetaMin { get; set; }
        public double ThetaMax { get; set; }
        public double ThetaKhoiTao { get; set; }
        public double ThetaDat { get; set; }
        public double PriorMean { get; set; }
        public double PriorVariance { get; set; }
        public double StandardErrorInitial { get; set; }

        public int? Stt { get; set; }
        public bool IsVisible { get; set; }
        public string? TrangThai { get; set; }

        public List<ChiTietCauHinhDeKiemTraDto> ChiTietCauHinhDeKiemTras { get; set; } = new();
    }

    public class ChiTietCauHinhDeKiemTraDto
    {
        public int Id { get; set; }
        public int IdCauHinhDeKiemTra { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public string? TenLoaiCauHoi { get; set; }
        public int? IdMucDoNhanThuc { get; set; }
        public string? TenMucDoNhanThuc { get; set; }
        public int SoLuongCauHoi { get; set; }
        public int? Stt { get; set; }
        public bool IsVisible { get; set; }
    }
}
