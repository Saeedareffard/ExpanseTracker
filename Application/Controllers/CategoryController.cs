using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public ActionResult<Category> Post([FromBody] Category category)
    {
        try
        {
            _unitOfWork.Repository<Category>().Add(category);
            _unitOfWork.Complete();
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
        var category = _unitOfWork.Repository<Category>().GetById(id);
        if (category == null) return NotFound();

        return Ok(category);
    }

    [HttpPut("id")]
    public ActionResult UpdateCategory(int id, [FromBody] Category category)
    {
        if (id != category.Id) return BadRequest();

        _unitOfWork.Repository<Category>().Update(category);

        try
        {
            _unitOfWork.Complete();
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (!_unitOfWork.Repository<Category>().Contains(x => x.Id == id)) return NotFound();
            return BadRequest(e);
        }

        return NoContent();
    }

    [HttpDelete("id")]
    public ActionResult<Category> DeleteCategory(int id)
    {
        var category = _unitOfWork.Repository<Category>().GetById(id);
        if (category == null) return NotFound();

        _unitOfWork.Repository<Category>().Remove(category);
        _unitOfWork.Complete();

        return Ok(category);
    }
}