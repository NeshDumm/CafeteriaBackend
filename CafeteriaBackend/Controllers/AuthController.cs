using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeteriaBackend.Controllers;
public record LoginRequest(string Usuario, string Password);
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config)
    {
        _config = config;
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // SOLO PARA PRUEBAS: dos usuarios fijos, cada uno con su rol
        string? rol = null;

        if (request.Usuario == "admin" && request.Password == "1234")
            rol = "Admin";
        else if (request.Usuario == "cliente" && request.Password == "1234")
            rol = "Usuario";

        if (rol == null)
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Usuario),
            new Claim(ClaimTypes.Role, rol), // aquí va el rol del usuario dinámico
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
