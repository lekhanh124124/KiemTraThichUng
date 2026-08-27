using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Commands.CreateCauHinhDeKiemTra;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Commands.DeleteCauHinhDeKiemTra;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Commands.UpdateCauHinhDeKiemTra;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.DTOs;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Queries.GetCauHinhDeKiemTraById;
using KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Queries.GetListCauHinhDeKiemTra;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHinhDeKiemTraController(ISender sender) : ControllerBase
    {
        [HttpPost("GetList")]
        public async Task<ActionResult<ApiResponse<PagedResult<CauHinhDeKiemTraDto>>>> GetList([FromBody] GetListCauHinhDeKiemTraQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResponse<CauHinhDeKiemTraDto>>> GetById([FromBody] GetCauHinhDeKiemTraByIdQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CreateCauHinhDeKiemTraCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<int>>> Update([FromBody] UpdateCauHinhDeKiemTraCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> Delete([FromBody] DeleteCauHinhDeKiemTraCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }
    }
}
