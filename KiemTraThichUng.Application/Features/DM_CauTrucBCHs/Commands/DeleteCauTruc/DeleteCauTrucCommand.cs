using KiemTraThichUng.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.DeleteCauTruc
{
    public class DeleteCauTrucCommand : ICommand<DeleteCauTrucResponse>
    {
        public IReadOnlyList<int> Ids { get; set; } = new List<int>();
    }
}
