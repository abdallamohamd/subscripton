using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Webapi.dto;
using Webapi.models;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly UserManager<appuser> userManager;
        private readonly IConfiguration configuration;

        public userController(UserManager<appuser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost ("regester")]
        public async Task <IActionResult> regester (regesterdto regesterdto)
        {
            if (ModelState.IsValid)
            {
                appuser user = new appuser();
                user.UserName = regesterdto.name;
                user.Email = regesterdto.email;
                user.PhoneNumber = regesterdto.phone;
                user.PasswordHash = regesterdto.password;
                IdentityResult Result = await userManager.CreateAsync(user, regesterdto.password);
                if(Result.Succeeded)
                {
                    return Created();
                }
                foreach(var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost ("login")]
        public async Task <IActionResult> login(logindto logindto)
        {
            if (ModelState.IsValid)
            {
                appuser user = await userManager.FindByNameAsync(logindto.Name);
                if(user != null)
                {
                     bool found = await userManager.CheckPasswordAsync(user, logindto.Password);
                    if(found == true)
                    {
                        //claims
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ));

                        var roles = await userManager.GetRolesAsync(user);
                        foreach(var item  in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }

                        //token
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));

                        SigningCredentials sin = new SigningCredentials ( key,SecurityAlgorithms.HmacSha256 );

                        JwtSecurityToken token = new JwtSecurityToken(

                            issuer: configuration["jwt:issu"],
                            audience: configuration["jwt:adu"],
                            expires: DateTime.Now.AddHours(1),
                            claims: claims,
                            signingCredentials: sin                

                            );
                        return Ok(
                            new
                            {
                                token= new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = DateTime.Now.AddHours(1)
                             });
                    }

                }
                ModelState.AddModelError("", "not found");
            }
            return BadRequest(ModelState);
        }
    }
}
