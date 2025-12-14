using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.KafkaEvent;
using QuickCode.Demo.UserManagerModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.KafkaEvent
{
    public class UpdateKafkaEventCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IKafkaEventRepository> _repositoryMock;
        private readonly Mock<ILogger<UpdateKafkaEventCommand.UpdateKafkaEventHandler>> _loggerMock;
        public UpdateKafkaEventCommandTests()
        {
            _repositoryMock = new Mock<IKafkaEventRepository>();
            _loggerMock = new Mock<ILogger<UpdateKafkaEventCommand.UpdateKafkaEventHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<KafkaEventDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<KafkaEventDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TopicName)).ReturnsAsync(new RepoResponse<KafkaEventDto>());
            var handler = new UpdateKafkaEventCommand.UpdateKafkaEventHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdateKafkaEventCommand(fakeDto.TopicName, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<KafkaEventDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<KafkaEventDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<KafkaEventDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TopicName)).ReturnsAsync(new RepoResponse<KafkaEventDto>());
            var handler = new UpdateKafkaEventCommand.UpdateKafkaEventHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdateKafkaEventCommand(fakeDto.TopicName, fakeDto);
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