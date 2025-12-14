using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.MessageLog;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.MessageLog
{
    public class InsertMessageLogCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IMessageLogRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertMessageLogCommand.InsertMessageLogHandler>> _loggerMock;
        public InsertMessageLogCommandTests()
        {
            _repositoryMock = new Mock<IMessageLogRepository>();
            _loggerMock = new Mock<ILogger<InsertMessageLogCommand.InsertMessageLogHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageLogDto>("tr");
            var fakeResponse = new RepoResponse<MessageLogDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<MessageLogDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertMessageLogCommand.InsertMessageLogHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertMessageLogCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<MessageLogDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageLogDto>("tr");
            var fakeResponse = new RepoResponse<MessageLogDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<MessageLogDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertMessageLogCommand.InsertMessageLogHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertMessageLogCommand(fakeDto);
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