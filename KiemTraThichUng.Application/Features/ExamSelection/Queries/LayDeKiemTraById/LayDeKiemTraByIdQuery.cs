using KiemTraThichUng.Application.Abstractions.Messaging;
using KiemTraThichUng.Application.Features.ExamSelection.DTOs;

namespace KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDeKiemTraById
{
    public class LayDeKiemTraByIdQuery : IQuery<DeKiemTraDto>
    {
        public int IdCauHinhDeKiemTra { get; set; }
    }
}
