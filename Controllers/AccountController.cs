using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using GasField.Data;
using GasField.DTOs;
using GasField.Models;
using Org.BouncyCastle.Crypto.Generators;
using System;

namespace GasField.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApiContext _context;

        public AccountController(ApiContext context)
        {
            _context = context;
        }

        //  Авторизация — выдача JWT-токена
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginDTO loginDTO)
        {
            var identity = await GetIdentity(loginDTO.Username, loginDTO.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Неверный логин или пароль." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = identity.FindFirst(ClaimTypes.Role)?.Value
            };

            return Ok(response);
        }

        //  Регистрация нового пользователя
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            // Проверка, существует ли пользователь
            var existingUser = await _context.People
                .FirstOrDefaultAsync(p => p.Login == registerDto.Login);

            if (existingUser != null)
            {
                return BadRequest(new { errorText = "Пользователь с таким логином уже существует." });
            }

            // Список допустимых ролей (если хочешь — можно вынести в enum)
            var validRoles = new[] { "Engineer", "Operator" };
            if (!validRoles.Contains(registerDto.Role))
            {
                return BadRequest(new { errorText = "Недопустимая роль. Возможные роли: Operator, Engineer." });
            }

            // Создание нового пользователя
            var newPerson = new Person
            {
                Login = registerDto.Login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role,
                FullName = registerDto.FullName,
                CreatedAt = DateTime.UtcNow
            };

            _context.People.Add(newPerson);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }


        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            Person person = await _context.People
                .FirstOrDefaultAsync(x => x.Login == username);

            // Проверка пользователя и пароля
            if (person != null && BCrypt.Net.BCrypt.Verify(password, person.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role),
                    new Claim("FullName", person.FullName)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено или пароль неверный
            return null;
        }
    }
}
