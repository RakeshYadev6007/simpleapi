using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Demo.Context;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly ApplicationDBContext _context;

        public JWTTokenController(IConfiguration configuration,ApplicationDBContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Users user)
        {
            if (user != null && user.Name != null && user.Password != null)
            {
                var userData =await GetUser(user.Name, user.Password);
                var jwt = _configuration.GetSection("jwt").Get<jwt>();
                if (userData != null)
                {
                    var claims = new[]
                    {
                         new Claim(JwtRegisteredClaimNames.Sub , jwt.Subject),
                         new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                         new Claim(JwtRegisteredClaimNames.Iat , DateTime.UtcNow.ToString()),
                         new Claim("Id" , userData.UserId.ToString()),
                         new Claim("Name", userData.Name),
                         new Claim("Password" , userData.Password)
                     };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var SignIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: SignIn

                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
               
                else
                {
                    return BadRequest("Invaild Credentials");
                }
            }
            else
            {
                return BadRequest();

            }
           
        }

        [HttpGet]
        public async Task<Users> GetUser(string name, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name && u.Password == password);
        }
        


       
        
    }
}
