using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.OtpMessageQueue
{
    public class InsertOtpMessageQueueCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IOtpMessageQueueRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertOtpMessageQueueCommand.InsertOtpMessageQueueHandler>> _loggerMock;
        public InsertOtpMessageQueueCommandTests()
        {
            _repositoryMock = new Mock<IOtpMessageQueueRepository>();
            _loggerMock = new Mock<ILogger<InsertOtpMessageQueueCommand.InsertOtpMessageQueueHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageQueueDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessageQueueDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessageQueueDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertOtpMessageQueueCommand.InsertOtpMessageQueueHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertOtpMessageQueueCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<OtpMessageQueueDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageQueueDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessageQueueDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessageQueueDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertOtpMessageQueueCommand.InsertOtpMessageQueueHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertOtpMessageQueueCommand(fakeDto);
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