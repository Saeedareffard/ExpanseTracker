using Application.Services;
using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Authorize]
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
    public ActionResult Put(Goal goal)
    {
        try
        {
            var result= _service.Update(goal);
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
    public ActionResult Delete(int id)
    {
        try
        {
            var result= _service.Delete(id);
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
    public ActionResult<Goal> Post([FromBody] Goal goal)
    {
        try
        {
            return _service.Add(goal);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}