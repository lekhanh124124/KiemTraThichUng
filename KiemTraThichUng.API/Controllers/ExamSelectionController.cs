using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.ExamSelection.DTOs;
using KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDanhSachDeKiemTra;
using KiemTraThichUng.Application.Features.ExamSelection.Queries.LayDeKiemTraById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamSelectionController(ISender sender) : ControllerBase
    {
        [HttpPost("GetList")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<PagedResult<DeKiemTraitemDto>>>> GetListByIdParent([FromBody] GetListByIdParentQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
        [HttpPost("GetById")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<DeKiemTraDto>>> GetById([FromBody] LayDeKiemTraByIdQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
    } 
}
