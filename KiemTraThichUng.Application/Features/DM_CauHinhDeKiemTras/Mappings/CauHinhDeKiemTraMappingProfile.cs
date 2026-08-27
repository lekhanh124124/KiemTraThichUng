using AutoMapper;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.DTOs;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;

namespace KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Mappings
{
    public class CauHinhDeKiemTraMappingProfile : Profile
    {
        public CauHinhDeKiemTraMappingProfile()
        {
            CreateMap<CauHinhDeKiemTra, CauHinhDeKiemTraDto>()
                .ForMember(d => d.TrangThai, opt => opt.MapFrom(s => s.TrangThai.ToString()));

            CreateMap<ChiTietCauHinhDeKiemTra, ChiTietCauHinhDeKiemTraDto>()
                .ForMember(d => d.TenLoaiCauHoi, opt => opt.MapFrom(s => s.LoaiCauHoi != null ? s.LoaiCauHoi.Name : null))
                .ForMember(d => d.TenMucDoNhanThuc, opt => opt.MapFrom(s => s.MucDoNhanThuc != null ? s.MucDoNhanThuc.Name : null));
        }
    }
}
