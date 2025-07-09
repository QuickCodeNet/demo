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
    public class EmailSendersInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IEmailSendersRepository> _repositoryMock;
        private readonly Mock<ILogger<EmailSendersInsertCommand.EmailSendersInsertHandler>> _loggerMock;
        public EmailSendersInsertCommandTests()
        {
            _repositoryMock = new Mock<IEmailSendersRepository>();
            _loggerMock = new Mock<ILogger<EmailSendersInsertCommand.EmailSendersInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<EmailSendersDto>("tr");
            var fakeResponse = new RepoResponse<EmailSendersDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<EmailSendersDto>())).ReturnsAsync(fakeResponse);
            var handler = new EmailSendersInsertCommand.EmailSendersInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new EmailSendersInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<EmailSendersDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<EmailSendersDto>("tr");
            var fakeResponse = new RepoResponse<EmailSendersDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<EmailSendersDto>())).ReturnsAsync(fakeResponse);
            var handler = new EmailSendersInsertCommand.EmailSendersInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new EmailSendersInsertCommand(fakeDto);
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