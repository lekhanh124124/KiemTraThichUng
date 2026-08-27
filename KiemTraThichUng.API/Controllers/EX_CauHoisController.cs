using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.EX_CauHois.Commands.CreateCauHoi;
using KiemTraThichUng.Application.Features.EX_CauHois.Commands.DeleteCauHoi;
using KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateCauHoi;
using KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateTrangThaiCauHoi;
using KiemTraThichUng.Application.Features.EX_CauHois.DTOs;
using KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetCauHoiById;
using KiemTraThichUng.Application.Features.EX_CauHois.Queries.GetListCauHoi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EX_CauHoisController(ISender sender) : ControllerBase
    {
        [HttpPost("GetList")]
        public async Task<ActionResult<ApiResponse<PagedResult<CauHoiItemDto>>>> GetList([FromBody] GetListCauHoiQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResponse<CauHoiDto>>> GetById([FromBody] GetCauHoiByIdQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateCauHoiResponse>>> CreateCauHoi([FromBody] CreateCauHoiCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<UpdateCauHoiResponse>>> UpdateCauHoi([FromBody] UpdateCauHoiCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<DeleteCauHoiResponse>>>> DeleteCauHoi([FromBody] DeleteCauHoiCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpPut("CapNhatTrangThaiCauHoi")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<CapNhatTrangThaiCauHoiResponse>>>> CapNhatTrangThaiCauHoi([FromBody] CapNhatTrangThaiCauHoiCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }
    }
}
