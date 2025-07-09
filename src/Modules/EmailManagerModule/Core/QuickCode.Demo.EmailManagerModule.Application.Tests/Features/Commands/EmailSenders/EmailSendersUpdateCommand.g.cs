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
    public class EmailSendersUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IEmailSendersRepository> _repositoryMock;
        private readonly Mock<ILogger<EmailSendersUpdateCommand.EmailSendersUpdateHandler>> _loggerMock;
        public EmailSendersUpdateCommandTests()
        {
            _repositoryMock = new Mock<IEmailSendersRepository>();
            _loggerMock = new Mock<ILogger<EmailSendersUpdateCommand.EmailSendersUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<EmailSendersDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<EmailSendersDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<EmailSendersDto>());
            var handler = new EmailSendersUpdateCommand.EmailSendersUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new EmailSendersUpdateCommand(fakeDto.Id, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<EmailSendersDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<EmailSendersDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<EmailSendersDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<EmailSendersDto>());
            var handler = new EmailSendersUpdateCommand.EmailSendersUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new EmailSendersUpdateCommand(fakeDto.Id, fakeDto);
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