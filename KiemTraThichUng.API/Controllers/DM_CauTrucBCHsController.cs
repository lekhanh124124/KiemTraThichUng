using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.CreateCautruc;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.DeleteCauTruc;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.UpdateCautruc;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.DTOs;
using KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Queries.GetListByIdParent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DM_CauTrucBCHsController(ISender sender) : ControllerBase
    {
        [HttpPost("GetListByIdParent")]
        public async Task<ActionResult<ApiResponse<PagedResult<CauTrucItemResponse>>>> GetListByIdParent([FromBody] GetListByIdParentQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateCauTrucResponse>>> CreateCauTruc([FromBody] CreateCauTrucCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<UpdateCauTrucResponse>>> UpdateCauTruc([FromBody] UpdateCauTrucCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteCauTrucResponse>>> DeleteCauTruc([FromBody] DeleteCauTrucCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }
    }
}
