using Application.Controllers;
using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ExpanseTracker.Controllers;

public class TransactionControllerTests
{
    // Define the mock repositories and unit of work
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRepository<Transaction>> _transactionRepositoryMock;

    // Initialize the controller with the mock objects
    private readonly TransactionController _controller;

    public TransactionControllerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _transactionRepositoryMock = new Mock<IRepository<Transaction>>();
        _unitOfWorkMock.Setup(uow => uow.Repository<Transaction>()).Returns(_transactionRepositoryMock.Object);

        _controller = new TransactionController(_unitOfWorkMock.Object);
    }

    // Write the tests for GetTransactionsByCategory method
    [Fact]
    public void GetTransactionsByCategory_ReturnsOkResult_WhenTransactionsExist()
    {
        // Arrange
        var categoryId = 1;
        var transactions = new List<Transaction>
            { new() { Id = 1, CategoryId = categoryId }, new() { Id = 2, CategoryId = categoryId } };
        _transactionRepositoryMock
            .Setup(repo => repo.Find(It
                .IsAny<TransactionSpecification.TransactionsByCategoryIdSpecification>()))
            .Returns(transactions);

        // Act
        var result = _controller.GetTransactionsByCategory(categoryId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTransactions = Assert.IsAssignableFrom<IEnumerable<Transaction>>(okResult.Value);
        Assert.Equal(2, returnedTransactions.Count());
    }

    [Fact]
    public void GetTransactionsByCategory_ReturnsNotFoundResult_WhenNoTransactionsExist()
    {
        // Arrange
        var categoryId = 1;
        _transactionRepositoryMock
            .Setup(repo => repo.Find(It.IsAny<TransactionSpecification.TransactionsByCategoryIdSpecification>()))
            .Returns(new List<Transaction>());

        // Act
        var result = _controller.GetTransactionsByCategory(categoryId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}