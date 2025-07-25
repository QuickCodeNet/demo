using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Features;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Features
{
    public class ExpenseTypesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IExpenseTypesRepository> _repositoryMock;
        private readonly Mock<ILogger<ExpenseTypesInsertCommand.ExpenseTypesInsertHandler>> _loggerMock;
        public ExpenseTypesInsertCommandTests()
        {
            _repositoryMock = new Mock<IExpenseTypesRepository>();
            _loggerMock = new Mock<ILogger<ExpenseTypesInsertCommand.ExpenseTypesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ExpenseTypesDto>("tr");
            var fakeResponse = new RepoResponse<ExpenseTypesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ExpenseTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new ExpenseTypesInsertCommand.ExpenseTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ExpenseTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ExpenseTypesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ExpenseTypesDto>("tr");
            var fakeResponse = new RepoResponse<ExpenseTypesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ExpenseTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new ExpenseTypesInsertCommand.ExpenseTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ExpenseTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.Null(result.Value);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}