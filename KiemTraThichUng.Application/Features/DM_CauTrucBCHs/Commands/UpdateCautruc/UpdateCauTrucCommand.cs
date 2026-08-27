using KiemTraThichUng.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.UpdateCautruc
{
    public class UpdateCauTrucCommand : ICommand<UpdateCauTrucResponse>
    {
        public int Id { get; set; }
        public string? TenCauTruc { get; set; }
        public string? MaCauTruc { get; set; }
        public string? GhiChu { get; set; }
        public int? Stt { get; set; }
        public bool? IsVisible { get; set; }
    }
}
