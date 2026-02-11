using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.API.Services;
using Project.Domain.Entities.Concretes;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(ApiResponse<string>.Fail("Kullanıcı adı ve şifre gereklidir."));

            AppUser? user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return Unauthorized(ApiResponse<string>.Fail("Geçersiz kullanıcı adı veya şifre."));

            if (!user.EmailConfirmed)
                return Unauthorized(ApiResponse<string>.Fail("E-posta onaylanmamış. Lütfen e-postanızı onaylayın."));

            bool passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                return Unauthorized(ApiResponse<string>.Fail("Geçersiz kullanıcı adı veya şifre."));

            IList<string> roles = await _userManager.GetRolesAsync(user);

            string token = _tokenService.GenerateToken(user, roles);

            return Ok(ApiResponse<LoginResponse>.Ok(new LoginResponse
            {
                Token = token,
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = roles.ToList(),
                Expiration = DateTime.UtcNow.AddMinutes(60)
            }, "Giriş başarılı."));
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public DateTime Expiration { get; set; }
    }
}
