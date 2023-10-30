using Application.Dtos;
using Application.Services;
using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly GoalService _service;

    public GoalController(GoalService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Goal>> Get(int? pageNumber, int? pageSize, string? orderBy, string? orderByDesc)
    {
        return Ok(_service.Get(pageNumber,pageSize,orderBy,orderByDesc));
    }

    [HttpGet("id")]
    public ActionResult<Goal> GetById([FromQuery] int id)
    {
        var result = _service.GetById(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Put(GoalDto goal)
    {
        try
        {
            var result= await _service.Update(goal);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var result= await _service.Delete(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Goal>> Post([FromBody] GoalDto goal)
    {
        try
        {
            return await _service.Add(goal);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}