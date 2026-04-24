using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NIAUNIVERSITYPANELAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IJwtService _jwt;

        public AuthController(IAuthService auth, IJwtService jwt)
        {
            _auth = auth;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            _auth.GenerateOtp(request.MobileNumber);
            return Ok("OTP Sent");
        }

        [HttpPost("verify")]
        public IActionResult Verify(VerifyOtpRequest request)
        {
            var isValid = _auth.VerifyOtp(request.MobileNumber, request.OTP);

            if (!isValid)
                return Unauthorized();

            var token = _jwt.GenerateToken(request.MobileNumber);

            return Ok(new { token });
        }
    }
}
