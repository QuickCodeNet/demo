using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.Sender;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.Sender
{
    public class InsertSenderCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISenderRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertSenderCommand.InsertSenderHandler>> _loggerMock;
        public InsertSenderCommandTests()
        {
            _repositoryMock = new Mock<ISenderRepository>();
            _loggerMock = new Mock<ILogger<InsertSenderCommand.InsertSenderHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SenderDto>("tr");
            var fakeResponse = new RepoResponse<SenderDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SenderDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertSenderCommand.InsertSenderHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertSenderCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SenderDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SenderDto>("tr");
            var fakeResponse = new RepoResponse<SenderDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SenderDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertSenderCommand.InsertSenderHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertSenderCommand(fakeDto);
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