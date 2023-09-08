using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _service;

    public TransactionController(TransactionService service)
    {
        _service = service;
    }

    [HttpGet("category")]
    public ActionResult<IEnumerable<Transaction>> GetTransactionsByCategory([FromQuery] int categoryId)
    {
        return Ok(_service.GetByCategory(categoryId));
    }

    [HttpGet("categoryIds")]
    public ActionResult<IEnumerable<Transaction>> GetTransactionByCategories([FromQuery] List<int> categoryIds)
    {
        return Ok(_service.GetByCategoryIds(categoryIds));
    }

    [HttpGet]
   // [Authorize]
    public ActionResult<IEnumerable<Transaction>>? GetAllTransactions([FromQuery] int? pageNumber, [FromQuery] int? size,
        [FromQuery] string? orderBy, [FromQuery] string? orderByDesc)
    {
        if (orderBy != null && orderByDesc != null)
            return BadRequest("either one of these parameters should be mentioned : orderBy , orderByDesc");

        return Ok(_service.Get(pageNumber, size, orderBy, orderByDesc));
    }

    [HttpGet("id")]
    public ActionResult<Transaction> GetTransaction(int id)
    {
        return Ok(_service.GetById(id));
    }

    [HttpPost]
    public ActionResult<Transaction> PostTransaction([FromBody] Transaction transaction)
    {
        try
        {
            _service.Add(transaction);
            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut]
    public ActionResult<Transaction> UpdateTransaction([FromBody] Transaction transaction)
    {
        try
        {
            var result = _service.Update(transaction);
            if (!result) return NotFound();
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public ActionResult DeleteTransaction(int id)
    {
        var result = _service.Delete(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}