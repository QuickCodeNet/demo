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
    public class CommonExpensesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICommonExpensesRepository> _repositoryMock;
        private readonly Mock<ILogger<CommonExpensesInsertCommand.CommonExpensesInsertHandler>> _loggerMock;
        public CommonExpensesInsertCommandTests()
        {
            _repositoryMock = new Mock<ICommonExpensesRepository>();
            _loggerMock = new Mock<ILogger<CommonExpensesInsertCommand.CommonExpensesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CommonExpensesDto>("tr");
            var fakeResponse = new RepoResponse<CommonExpensesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CommonExpensesDto>())).ReturnsAsync(fakeResponse);
            var handler = new CommonExpensesInsertCommand.CommonExpensesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new CommonExpensesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<CommonExpensesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CommonExpensesDto>("tr");
            var fakeResponse = new RepoResponse<CommonExpensesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CommonExpensesDto>())).ReturnsAsync(fakeResponse);
            var handler = new CommonExpensesInsertCommand.CommonExpensesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new CommonExpensesInsertCommand(fakeDto);
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