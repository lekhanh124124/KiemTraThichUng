namespace KiemTraThichUng.Application.Features.DataControls.DTOs
{
    public class BoCauHoiItemResponse
    {
        public int Id { get; set; }
        public string MaBoCauHoi { get; set; } = string.Empty;
        public string TenBoCauHoi { get; set; } = string.Empty;
        public string TaiLieuThamKhao { get; set; } = string.Empty;

        public string GhiChu { get; set; } = string.Empty;
        public bool IsLocked { get; set; } = false;
    }
}
