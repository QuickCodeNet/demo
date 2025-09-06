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
    public class SmsSendersInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISmsSendersRepository> _repositoryMock;
        private readonly Mock<ILogger<SmsSendersInsertCommand.SmsSendersInsertHandler>> _loggerMock;
        public SmsSendersInsertCommandTests()
        {
            _repositoryMock = new Mock<ISmsSendersRepository>();
            _loggerMock = new Mock<ILogger<SmsSendersInsertCommand.SmsSendersInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SmsSendersDto>("tr");
            var fakeResponse = new RepoResponse<SmsSendersDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SmsSendersDto>())).ReturnsAsync(fakeResponse);
            var handler = new SmsSendersInsertCommand.SmsSendersInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new SmsSendersInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SmsSendersDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SmsSendersDto>("tr");
            var fakeResponse = new RepoResponse<SmsSendersDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SmsSendersDto>())).ReturnsAsync(fakeResponse);
            var handler = new SmsSendersInsertCommand.SmsSendersInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new SmsSendersInsertCommand(fakeDto);
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