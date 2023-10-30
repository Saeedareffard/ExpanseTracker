using Application.Dtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Post([FromBody] CategoryDto category)
    {
        try
        {
            await _service.Add(category);
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        ;
    }

    [HttpGet("id")]
    public ActionResult<Category> GetCategory(int id)
    {
        var category = _service.GetById(id);
        if (category == null) return NotFound();

        return Ok(category);
    }

    [HttpPut]
    public async Task<ActionResult>  UpdateCategory( [FromBody] CategoryDto category)
    {
        try
        {
            var result = await _service.Update(category);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var result = await _service.Delete(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}