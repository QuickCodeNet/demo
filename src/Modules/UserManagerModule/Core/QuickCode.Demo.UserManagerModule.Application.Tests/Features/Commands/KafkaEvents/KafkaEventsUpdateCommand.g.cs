using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features
{
    public class KafkaEventsUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IKafkaEventsRepository> _repositoryMock;
        private readonly Mock<ILogger<KafkaEventsUpdateCommand.KafkaEventsUpdateHandler>> _loggerMock;
        public KafkaEventsUpdateCommandTests()
        {
            _repositoryMock = new Mock<IKafkaEventsRepository>();
            _loggerMock = new Mock<ILogger<KafkaEventsUpdateCommand.KafkaEventsUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<KafkaEventsDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<KafkaEventsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TopicName)).ReturnsAsync(new RepoResponse<KafkaEventsDto>());
            var handler = new KafkaEventsUpdateCommand.KafkaEventsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new KafkaEventsUpdateCommand(fakeDto.TopicName, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<KafkaEventsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<KafkaEventsDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<KafkaEventsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TopicName)).ReturnsAsync(new RepoResponse<KafkaEventsDto>());
            var handler = new KafkaEventsUpdateCommand.KafkaEventsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new KafkaEventsUpdateCommand(fakeDto.TopicName, fakeDto);
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