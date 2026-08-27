// File: KiemTraThichUng.Application/Features/EX_CauHois/DTOs/CauHoiItemResponse.cs
using KiemTraThichUng.Domain.Enums;

namespace KiemTraThichUng.Application.Features.EX_CauHois.DTOs
{
    public class CauHoiItemDto
    {
        public int? Id { get; set; } 
        public string? MaCauHoi { get; set; }
        public int? IdCauTrucBCH { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public bool? IsKhongDao { get; set; }
        public bool? IsCauHoiCha { get; set; }
        public int? IdCauHoiCha { get; set; }
        public double? DoKho { get; set; }
        public double? DoKhoKhoiTao { get; set; }
        public bool? IsVisible { get; set; }
        public TrangThaiDuyet? IdTrangThai { get; set; }
        public string? MediaUrl { get; set; }
        public string? TieuDeVeTrai { get; set; }
        public string? TieuDeVePhai { get; set; }
        public int? SttCauHoiCon { get; set; }
        public int? SttCauHoi { get; set; }
        public int? IdNguoiSoan { get; set; }
        public DateTime? NgaySoan { get; set; }
        public int? IdNguoiDuyet { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public Guid? CauHoiGUId { get; set; }
        public string? MaCauTruc { get; set; } 
        public string? TenCauTruc { get; set; } 
        public int? IdBoCauHoi { get; set; }
        public string? MaNhanSu { get; set; } 
        public string? HoDem { get; set; } 
        public string? Ten { get; set; } 
        public string? MaLoaiCauHoi { get; set; } 
        public string? TenLoaiCauHoi { get; set; } 
        public string? MaMucDoNhanThuc { get; set; } 
        public string? TenMucDoNhanThuc { get; set; } 
        public string? MaNhanSuDuyet { get; set; } 
        public string? HoDemNguoiDuyet { get; set; } 
        public string? TenNguoiDuyet { get; set; } 
        public string? GhiChuDuyet { get; set; }
        public string? NoiDung { get; set; }
    }
}
