using Application.Dtos;
using Application.Goals;
using Application.Services;
using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly GoalService _service; private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

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
    public async Task<ActionResult<int>> Post([FromBody] CreateGoalRequest goal)
    {
        try
        {
            return await Mediator.Send(goal);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}