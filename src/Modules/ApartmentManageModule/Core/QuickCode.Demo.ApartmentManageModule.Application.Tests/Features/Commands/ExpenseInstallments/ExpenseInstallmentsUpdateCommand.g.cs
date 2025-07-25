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
    public class ExpenseInstallmentsUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IExpenseInstallmentsRepository> _repositoryMock;
        private readonly Mock<ILogger<ExpenseInstallmentsUpdateCommand.ExpenseInstallmentsUpdateHandler>> _loggerMock;
        public ExpenseInstallmentsUpdateCommandTests()
        {
            _repositoryMock = new Mock<IExpenseInstallmentsRepository>();
            _loggerMock = new Mock<ILogger<ExpenseInstallmentsUpdateCommand.ExpenseInstallmentsUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ExpenseInstallmentsDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ExpenseInstallmentsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<ExpenseInstallmentsDto>());
            var handler = new ExpenseInstallmentsUpdateCommand.ExpenseInstallmentsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ExpenseInstallmentsUpdateCommand(fakeDto.Id, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ExpenseInstallmentsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ExpenseInstallmentsDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ExpenseInstallmentsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<ExpenseInstallmentsDto>());
            var handler = new ExpenseInstallmentsUpdateCommand.ExpenseInstallmentsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ExpenseInstallmentsUpdateCommand(fakeDto.Id, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}