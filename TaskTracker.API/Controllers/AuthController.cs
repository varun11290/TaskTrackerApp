using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.API.DTO;
using TaskTracker.API.Models;
using TaskTracker.API.Repo;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepo repo,IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDTO userToCreate)
        {
            userToCreate.UserName = userToCreate.UserName.ToLower();

            if (await _repo.UserExist(userToCreate.UserName))
                return BadRequest("User already exists");

            var creatUser = new User()
            {
                Name = userToCreate.UserName
            };
            var createdUser = await _repo.Register(creatUser, userToCreate.Password);
            return StatusCode(201);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            var user = await _repo.Login(loginUser.UserName, loginUser.Password);
            if (user == null)
                return Unauthorized("user name or password is incorrect");

            //here we will create the token

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name)
            };

            var key =new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSetings:Token").Value));

            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDisc=new SecurityTokenDescriptor(){
                 Subject=new ClaimsIdentity(claims),
                 Expires=DateTime.Now.AddDays(1),
                 SigningCredentials=cred
            };

            var tokeHandler=new JwtSecurityTokenHandler();
            var token=tokeHandler.CreateToken(tokenDisc);

            return Ok(new {
                token=tokeHandler.WriteToken(token)
            });
        }
    }
}