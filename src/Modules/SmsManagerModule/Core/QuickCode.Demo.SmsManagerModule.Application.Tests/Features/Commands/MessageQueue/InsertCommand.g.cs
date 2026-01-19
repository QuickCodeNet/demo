using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.MessageQueue
{
    public class InsertMessageQueueCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IMessageQueueRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertMessageQueueCommand.InsertMessageQueueHandler>> _loggerMock;
        public InsertMessageQueueCommandTests()
        {
            _repositoryMock = new Mock<IMessageQueueRepository>();
            _loggerMock = new Mock<ILogger<InsertMessageQueueCommand.InsertMessageQueueHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageQueueDto>("tr");
            var fakeResponse = new RepoResponse<MessageQueueDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<MessageQueueDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertMessageQueueCommand.InsertMessageQueueHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertMessageQueueCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<MessageQueueDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageQueueDto>("tr");
            var fakeResponse = new RepoResponse<MessageQueueDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<MessageQueueDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertMessageQueueCommand.InsertMessageQueueHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertMessageQueueCommand(fakeDto);
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