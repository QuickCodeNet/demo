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
    public class UpdateOtpMessageLogCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IOtpMessageLogRepository> _repositoryMock;
        private readonly Mock<ILogger<UpdateOtpMessageLogCommand.UpdateOtpMessageLogHandler>> _loggerMock;
        public UpdateOtpMessageLogCommandTests()
        {
            _repositoryMock = new Mock<IOtpMessageLogRepository>();
            _loggerMock = new Mock<ILogger<UpdateOtpMessageLogCommand.UpdateOtpMessageLogHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageLogDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<OtpMessageLogDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<OtpMessageLogDto>());
            var handler = new UpdateOtpMessageLogCommand.UpdateOtpMessageLogHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdateOtpMessageLogCommand(fakeDto.Id, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<OtpMessageLogDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageLogDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<OtpMessageLogDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<OtpMessageLogDto>());
            var handler = new UpdateOtpMessageLogCommand.UpdateOtpMessageLogHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdateOtpMessageLogCommand(fakeDto.Id, fakeDto);
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