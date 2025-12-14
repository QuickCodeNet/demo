using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.BlackList;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.BlackList;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.BlackList
{
    public class InsertBlackListCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IBlackListRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertBlackListCommand.InsertBlackListHandler>> _loggerMock;
        public InsertBlackListCommandTests()
        {
            _repositoryMock = new Mock<IBlackListRepository>();
            _loggerMock = new Mock<ILogger<InsertBlackListCommand.InsertBlackListHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<BlackListDto>("tr");
            var fakeResponse = new RepoResponse<BlackListDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<BlackListDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertBlackListCommand.InsertBlackListHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertBlackListCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<BlackListDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<BlackListDto>("tr");
            var fakeResponse = new RepoResponse<BlackListDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<BlackListDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertBlackListCommand.InsertBlackListHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertBlackListCommand(fakeDto);
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