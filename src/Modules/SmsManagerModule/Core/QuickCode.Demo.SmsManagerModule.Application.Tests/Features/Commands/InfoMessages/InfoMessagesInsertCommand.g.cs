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
    public class InfoMessagesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IInfoMessagesRepository> _repositoryMock;
        private readonly Mock<ILogger<InfoMessagesInsertCommand.InfoMessagesInsertHandler>> _loggerMock;
        public InfoMessagesInsertCommandTests()
        {
            _repositoryMock = new Mock<IInfoMessagesRepository>();
            _loggerMock = new Mock<ILogger<InfoMessagesInsertCommand.InfoMessagesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoMessagesDto>("tr");
            var fakeResponse = new RepoResponse<InfoMessagesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<InfoMessagesDto>())).ReturnsAsync(fakeResponse);
            var handler = new InfoMessagesInsertCommand.InfoMessagesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoMessagesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<InfoMessagesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoMessagesDto>("tr");
            var fakeResponse = new RepoResponse<InfoMessagesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<InfoMessagesDto>())).ReturnsAsync(fakeResponse);
            var handler = new InfoMessagesInsertCommand.InfoMessagesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoMessagesInsertCommand(fakeDto);
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