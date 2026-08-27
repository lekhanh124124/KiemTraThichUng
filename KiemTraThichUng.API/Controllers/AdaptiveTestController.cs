using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.AdaptiveTest.Commands.BatDauKiemTra;
using KiemTraThichUng.Application.Features.AdaptiveTest.Commands.KetThucKiemTra;
using KiemTraThichUng.Application.Features.AdaptiveTest.Commands.LayCauHoiTiepTheo;
using KiemTraThichUng.Application.Features.AdaptiveTest.Commands.NopCauTraLoi;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDanhSachPhienKiemTra;
using KiemTraThichUng.Application.Features.AdaptiveTest.Queries.LayDuLieuKiemTra;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdaptiveTestController(ISender sender) : ControllerBase
    {
        [HttpPost("BatDauKiemTra")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<BatDauKiemTraResponse>>> StartAdaptiveTest([FromBody] BatDauKiemTraCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpPost("GetCauHoiTiepTheo")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<LayCauHoiTiepTheoResponse>>> GetNextQuestion([FromBody] LayCauHoiTiepTheoCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }


        [HttpPost("NopCauTraLoi")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<NopCauTraLoiResponse>>> SubmitAnswer([FromBody] NopCauTraLoiCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpPost("KetThucKiemTra")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<KetThucKiemTraResponse>>> KetThucKiemTra([FromBody] KetThucKiemTraCommand command)
        {
            var result = await sender.Send(command);
            return Ok(result);
        }

        [HttpPost("LayDuLieuKiemTra")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<DuLieuKiemTraDto>>> LayDuLieuKiemTra([FromBody] LayDuLieuKiemTraQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpPost("LayAllKetQuaKiemTra")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<PhienKiemTraItemDto>>> LayAllKetQuaKiemTra([FromBody] LayDanhSachPhienKiemTraQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
    }
}
