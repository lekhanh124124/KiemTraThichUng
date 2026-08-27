using KiemTraThichUng.Application.Abstractions.Messaging;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.BatDauKiemTra
{
    public class BatDauKiemTraCommand : ICommand<BatDauKiemTraResponse>
    {
        public int IdCauHinhDeKiemTra { get; set; } 
    }
}
