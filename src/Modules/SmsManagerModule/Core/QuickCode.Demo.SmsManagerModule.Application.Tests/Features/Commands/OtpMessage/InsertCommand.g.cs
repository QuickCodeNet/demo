using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.OtpMessage
{
    public class InsertOtpMessageCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IOtpMessageRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertOtpMessageCommand.InsertOtpMessageHandler>> _loggerMock;
        public InsertOtpMessageCommandTests()
        {
            _repositoryMock = new Mock<IOtpMessageRepository>();
            _loggerMock = new Mock<ILogger<InsertOtpMessageCommand.InsertOtpMessageHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessageDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessageDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertOtpMessageCommand.InsertOtpMessageHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertOtpMessageCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<OtpMessageDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessageDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessageDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessageDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertOtpMessageCommand.InsertOtpMessageHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertOtpMessageCommand(fakeDto);
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