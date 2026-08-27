using KiemTraThichUng.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.CreateCautruc
{
    public class CreateCauTrucCommand : ICommand<CreateCauTrucResponse>
    {
        public string TenCauTruc { get; set; } = string.Empty;
        public string MaCauTruc { get; set; } = string.Empty;

        public int IdBoCauHoi { get; set; }
        public int? IdParent { get; set; }

        public string? GhiChu { get; set; }
        public int Stt { get; set; }
        public bool IsVisible { get; set; }
    }
}
