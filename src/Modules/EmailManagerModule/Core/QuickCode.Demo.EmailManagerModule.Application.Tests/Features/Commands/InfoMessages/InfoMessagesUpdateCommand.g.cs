using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.EmailManagerModule.Application.Features;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.EmailManagerModule.Application.Tests.Features
{
    public class InfoMessagesUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IInfoMessagesRepository> _repositoryMock;
        private readonly Mock<ILogger<InfoMessagesUpdateCommand.InfoMessagesUpdateHandler>> _loggerMock;
        public InfoMessagesUpdateCommandTests()
        {
            _repositoryMock = new Mock<IInfoMessagesRepository>();
            _loggerMock = new Mock<ILogger<InfoMessagesUpdateCommand.InfoMessagesUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoMessagesDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<InfoMessagesDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<InfoMessagesDto>());
            var handler = new InfoMessagesUpdateCommand.InfoMessagesUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoMessagesUpdateCommand(fakeDto.Id, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<InfoMessagesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoMessagesDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<InfoMessagesDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<InfoMessagesDto>());
            var handler = new InfoMessagesUpdateCommand.InfoMessagesUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoMessagesUpdateCommand(fakeDto.Id, fakeDto);
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