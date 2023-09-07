using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[Route("api/[controller]")]
//[Authorize]
public class CategoryController : ControllerBase
{

    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult<Category> Post([FromBody] Category category)
    {
        try
        {
            _service.Add(category);
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
    public ActionResult UpdateCategory( [FromBody] Category category)
    {
        try
        {
            var result = _service.Update(category);
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
    public ActionResult DeleteCategory(int id)
    {
        var result = _service.Delete(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}