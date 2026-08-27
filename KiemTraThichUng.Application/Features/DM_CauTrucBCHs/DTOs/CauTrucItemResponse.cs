namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.DTOs
{
    public class CauTrucItemResponse
    {
        public int Id { get; set; }
        public string TenCauTruc { get; set; } = string.Empty;
        public string MaCauTruc { get; set; } = string.Empty;
        public int? IdParent { get; set; }
        public int? IdBoCauHoi { get; set; }
        public string? GhiChu { get; set; }
        public int? Stt { get; set; }
        public bool IsVisible { get; set; }
    }
}
