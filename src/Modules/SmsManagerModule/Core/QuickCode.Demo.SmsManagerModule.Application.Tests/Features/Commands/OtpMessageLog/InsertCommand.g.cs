using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageLog;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageLog;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.OtpMessageLog
{
    public class InsertOtpMessageLogCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IOtpMessageLogRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertOtpMessageLogCommand.InsertOtpMessageLogHandler>> _loggerMock;
        public InsertOtpMessageLogCommandTests()
        {
            _repositoryMock = new Mock<IOtpMessageLogRepository>();
            _loggerMock = new Mock<ILogger<InsertOtpMessageLogCommand.InsertOtpMessageLogHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageLogDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessageLogDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessageLogDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertOtpMessageLogCommand.InsertOtpMessageLogHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertOtpMessageLogCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<OtpMessageLogDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageLogDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessageLogDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessageLogDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertOtpMessageLogCommand.InsertOtpMessageLogHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertOtpMessageLogCommand(fakeDto);
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