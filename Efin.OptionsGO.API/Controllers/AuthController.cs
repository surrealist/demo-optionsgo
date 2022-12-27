
using Efin.OptionsGo.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GMM.Bookings.APIs.Controllers
{

  [Route("api/v1/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IConfiguration config;

    public AuthController(IConfiguration config)
    {
      this.config = config;
    }

    private (bool success, string roleName, string username, Guid id)
      ValidateUser(string username, string password)
    {
      switch (username)
      {
        case "alice":
          return (
            success: true, roleName: "student",
            username: "Alice", id: new Guid("27AADA00-A47D-4ABE-81EE-6A3B844EED6F"));
        case "bob":
          return (true, "teacher", "Bob", new Guid("CBDF8478-D2D6-4615-A248-0DF9BED9A2BD"));
        case "cathy":
          return (true, "admin", "Cathy", new Guid("AF3634BD-21F6-460D-940C-84CCF79615F5"));
        default:
          return (false, "", "", new Guid());
      }
    }

    [HttpGet]
    public ActionResult GetUserInfo()
    {
      var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

      if (isAuthenticated)
      {
        return Ok(new
        {
          isAuthenticated,
          UserName = User.Identity!.Name,
          Role = User.Claims.SingleOrDefault(x => x.Type =="app-role")?.Value,
          Id = User.Claims.SingleOrDefault(x => x.Type == "app-id")?.Value,
        });
      }
      else
      {
        return Ok(new
        {
          isAuthenticated
        });
      }
    }

    [HttpPost]
    public ActionResult SignIn(UserSignIn item)
    {
      var result = ValidateUser(item.Username, item.Password);
      if (result.success)
      {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var key = Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, result.username),
                new Claim(JwtRegisteredClaimNames.Name, result.username),
                new Claim("gmm-role", result.roleName),
                new Claim("gmm-id", result.id.ToString()),
             }
          ),
          Expires = DateTime.UtcNow.AddDays(30),
          Issuer = issuer,
          Audience = audience,
          SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        var jwtResult = new
        {
          Token = stringToken
        };
        return Ok(jwtResult);
      }

      return Unauthorized();
    }


  }
}