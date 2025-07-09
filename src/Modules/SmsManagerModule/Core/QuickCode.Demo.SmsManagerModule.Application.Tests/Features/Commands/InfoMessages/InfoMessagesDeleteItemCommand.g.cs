using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features
{
    public class InfoMessagesDeleteItemCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IInfoMessagesRepository> _repositoryMock;
        private readonly Mock<ILogger<InfoMessagesDeleteItemCommand.InfoMessagesDeleteItemHandler>> _loggerMock;
        public InfoMessagesDeleteItemCommandTests()
        {
            _repositoryMock = new Mock<IInfoMessagesRepository>();
            _loggerMock = new Mock<ILogger<InfoMessagesDeleteItemCommand.InfoMessagesDeleteItemHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoMessagesDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<InfoMessagesDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<InfoMessagesDto>());
            var handler = new InfoMessagesDeleteItemCommand.InfoMessagesDeleteItemHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoMessagesDeleteItemCommand(fakeDto.Id);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<InfoMessagesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoMessagesDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<InfoMessagesDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<InfoMessagesDto>());
            var handler = new InfoMessagesDeleteItemCommand.InfoMessagesDeleteItemHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoMessagesDeleteItemCommand(fakeDto.Id);
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