using KiemTraThichUng.Application.Abstractions.Messaging;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.DeleteCauHoi
{
    public class DeleteCauHoiCommand : ICommand<IReadOnlyList<DeleteCauHoiResponse>>
    {
        public IReadOnlyList<int> Ids { get; set; } = new List<int>();
    }
}
