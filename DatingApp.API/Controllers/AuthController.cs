using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        public readonly IAuthRepository _repo;
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto user)
        {
            //validate the request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.userName = user.userName.ToLower();
            if (await _repo.UserExist(user.userName))
            {
                return BadRequest("userName already exist");
            }
            var userToCreate = new User
            {
                UserName = user.userName,


            };
            var createdUser = await _repo.Register(userToCreate, user.password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForRegisterDto user)
        {
            var u = await _repo.Login(user.userName.ToLower(), user.password);
            if (u == null)
                return Unauthorized();
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                new Claim(ClaimTypes.Name,u.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds =new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires= System.DateTime.Now.AddDays(1),
                    SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return Ok(new {token = tokenHandler.WriteToken(token)});


        }
    }
}