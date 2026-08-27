using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.Auth.Commands.Register;
using KiemTraThichUng.Application.Features.Auth.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraThichUng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("DangKy")]
        public async Task<ActionResult<ApiResponse<int>>> Register(RegisterCommand command)
        {
            var id = await sender.Send(command);
            return Ok(id);
        }

        [HttpPost("DangNhap")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginQuery query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }
    }
}
