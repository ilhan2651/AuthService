using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstract;
using Service.Abstract;
using Service.Dto;
using System.Security.Claims;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public AuthController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto dto)
        {
            try
            {
                var token = await _userService.RegisterUserAsync(dto);
                return Ok(new { token });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Registration failed" });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            try
            {
                var token=await _userService.LoginUserAsync(dto);
                return Ok( token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(new { message = "Login failed", error = ex.Message });
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            try
            {
                var response = await _userService.RefreshTokenAsync(request.Email, request.RefreshToken);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("sub")?.Value; // JWT'de sub=email ise

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            await _userService.LogoutUserAsync(email);
            return Ok(new { message = "Çıkış başarılı" });
        }

    }
}
