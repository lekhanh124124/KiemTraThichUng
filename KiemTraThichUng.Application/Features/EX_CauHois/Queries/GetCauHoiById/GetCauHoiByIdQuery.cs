using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetCauHoiById
{
    public class GetCauHoiByIdQuery : IQuery<CauHoiDto>
    {
        public int Id { get; set; }
    }
}
