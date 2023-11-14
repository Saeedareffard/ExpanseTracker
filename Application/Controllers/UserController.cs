using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] string? userName, [FromQuery] string? name)
    {
        return Ok((await _unitOfWork.Repository<User>()
            .FindAsync(new UserSpecification.UserSearchSpecification(userName: userName, name: name))).ToList());
    }

    [HttpGet("id")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
        try
        {
            await _unitOfWork.Repository<User>().AddAsync(user);

            await _unitOfWork.Complete();
            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }
        catch (DbUpdateConcurrencyException e)
        {
            return BadRequest(e.Message);
        }
    }
}