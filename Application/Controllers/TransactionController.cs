using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("category")]
    public ActionResult<IEnumerable<Transaction>> GetTransactionsByCategory([FromQuery] int categoryId)
    {
        return _unitOfWork.Repository<Transaction>()
            .Find(new TransactionSpecification.TransactionsByCategoryIdSpecification(categoryId)).ToList();
    }

    [HttpGet("categoryIds")]
    public ActionResult<IEnumerable<Transaction>> GetTransactionByCategories([FromQuery] List<int> categoryIds)
    {
        return _unitOfWork.Repository<Transaction>()
            .Find(new TransactionSpecification.TransactionsByCategoryIdSpecification(categoryIds)).ToList();
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Transaction>> GetAllTransactions([FromQuery] int? pageNumber, [FromQuery] int? size,
        [FromQuery] string? orderBy, [FromQuery] string? orderByDesc)
    {
        if (orderBy != null && orderByDesc != null)
        {
            return BadRequest("either one of these parameters should be mentioned : orderBy , orderByDesc");
        }

        return _unitOfWork.Repository<Transaction>()
            .Find(new TransactionSpecification.TransactionsPagedAndOrdered(page: pageNumber, size: size,
                orderBy: orderBy, orderByDes: orderByDesc)).ToList();
    }

    [HttpGet("id")]
    public ActionResult<Transaction> GetTransaction(int id)
    {
        return Ok(_unitOfWork.Repository<Transaction>().GetById(id));
    }

    [HttpPost]
    public ActionResult<Transaction> PostTransaction([FromBody] Transaction transaction)
    {
        try
        {
            _unitOfWork.Repository<Transaction>().Add(transaction);
            _unitOfWork.Complete();
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
        if (!_unitOfWork.Repository<Transaction>().Contains(x => x.Id == transaction.Id))
        {
            return NotFound();
        }

        try
        {
            _unitOfWork.Repository<Transaction>().Update(transaction);
            _unitOfWork.Complete();
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}