using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Controllers;

[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    public AuthenticationController(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    [HttpPost("validate")]
    public ActionResult<User> GetUserByCredential([FromBody] UserCredentialDto credentials)
    {
        var user = _unitOfWork.Repository<UserCredentialModel>()
            .Find(new UserSpecification.UserCredentialsSpecification(credentials.UserName, credentials.Password)).SingleOrDefault();
        ;
        if (user is null) return Unauthorized();

        return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] UserCredentialDto credential)
    {
        var user = GetUserByCredential(credential);
        if (user.Result is UnauthorizedResult) return Unauthorized();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, credential.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Tokens:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Audience"], claims,
            signingCredentials: creds, expires: DateTime.UtcNow.AddMinutes(20));
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    [HttpPost]
    public ActionResult AssignPasswordToUser([FromBody] UserCredentialModel credModel)
    {
        try
        {
            _unitOfWork.Repository<UserCredentialModel>().Add(credModel);
            _unitOfWork.Complete();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}