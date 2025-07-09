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
    public class OtpMessagesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IOtpMessagesRepository> _repositoryMock;
        private readonly Mock<ILogger<OtpMessagesInsertCommand.OtpMessagesInsertHandler>> _loggerMock;
        public OtpMessagesInsertCommandTests()
        {
            _repositoryMock = new Mock<IOtpMessagesRepository>();
            _loggerMock = new Mock<ILogger<OtpMessagesInsertCommand.OtpMessagesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessagesDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessagesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessagesDto>())).ReturnsAsync(fakeResponse);
            var handler = new OtpMessagesInsertCommand.OtpMessagesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new OtpMessagesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<OtpMessagesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OtpMessagesDto>("tr");
            var fakeResponse = new RepoResponse<OtpMessagesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OtpMessagesDto>())).ReturnsAsync(fakeResponse);
            var handler = new OtpMessagesInsertCommand.OtpMessagesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new OtpMessagesInsertCommand(fakeDto);
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