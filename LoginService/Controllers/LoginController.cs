using LoginService.Entity;
using LoginService.Entity.Models;
using LoginService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IdataRepository _login;
        private readonly IConfiguration _configuration;

        public LoginController(IdataRepository login, IConfiguration configuration)
        {
            this._login = login;
            this._configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(string name, string password)
        {
            CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                UserName = name,
                Password = hash,
                PasswordSalt = salt
            };

            user = _login.Register(user);

            return Ok("Registered User Successfully");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string name, string password)
        {
            User user = _login.GetUser(name);
            string token = "";

            if (!String.IsNullOrEmpty(name) && name == user.UserName && VerifyPassword(password, user.PasswordSalt, user.Password))
            {
                var refreshtoken = GenerateRefreshToken();
                SetRefreshToken(refreshtoken);
                token = CreateJWTToken(name, user.RoleType);
            }
            else
            {
                return BadRequest("UserName or password incorrect");
            }

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("UserName or password incorrect");

            }
            else
            {
                if(user.RoleType == 0)
                {
                    return Ok("Normal user page. Token:" + token);
                }
                else
                {
                    return Ok("Admin user page. Token:" + token);
                }
            }
            //return Ok(Redirect("https://localhost:7024"));
        }

        [HttpGet("logout")]
        public async Task<ActionResult<string>> Logout()
        {
            Response.Cookies.Delete("refreshToken")
;
            return Ok("Logged out");
        }

        private bool VerifyPassword(string password, byte[] salt, byte[] currentPassword)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return currentPassword.SequenceEqual(hash);
            }
        }

        private string CreateJWTToken(string name, int roleType)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, roleType == 0 ? "User" : "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private UserModel GenerateRefreshToken()
        {
            var refreshtoken = new UserModel
            {
                refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                TokenCreated = DateTime.Now,
                Expire = DateTime.Now.AddDays(1)
            };

            return refreshtoken;
        }

        private void SetRefreshToken(UserModel newToken)
        {
            var cokkiesOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = newToken.Expire
            };

            Response.Cookies.Append("refreshToken", newToken.refreshToken, cokkiesOption);
        }
    }
}
