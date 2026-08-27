// File: KiemTraThichUng.API/Controllers/DataControlsController.cs
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.DataControls.DTOs;
using KiemTraThichUng.Application.Features.DataControls.Queries.GetAllBoCauHoi;
using KiemTraThichUng.Application.Features.DataControls.Queries.GetAllCauTruc;
using KiemTraThichUng.Application.Features.DataControls.Queries.GetLoaiCauHoiForSelector;
using KiemTraThichUng.Application.Features.DataControls.Queries.GetMucDoNhanThucForSelector;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataControlsController(ISender sender) : ControllerBase
    {
        [HttpPost("GetBoCauHoi")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<BoCauHoiItemResponse>>>> GetBoCauHoi(
            [FromBody] GetAllBoCauHoiQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost("GetCauTrucBch")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<CauTrucItemResponse>>>> GetCauTrucBch(
            [FromBody] GetAllCauTrucQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost("GetLoaiCauHoiForSelector")]
        public async Task<ActionResult<ApiResponse<List<SelectorItemResponse>>>> GetLoaiCauHoiForSelector([FromBody] GetLoaiCauHoiForSelectorQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost("GetMucDoNhanThucForSelector")]
        public async Task<ActionResult<ApiResponse<List<SelectorItemResponse>>>> GetMucDoNhanThucForSelector([FromBody] GetMucDoNhanThucForSelectorQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
    }
}
