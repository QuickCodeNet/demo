using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features.MessageTemplate
{
    public class DeleteItemMessageTemplateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IMessageTemplateRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemMessageTemplateCommand.DeleteItemMessageTemplateHandler>> _loggerMock;
        public DeleteItemMessageTemplateCommandTests()
        {
            _repositoryMock = new Mock<IMessageTemplateRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemMessageTemplateCommand.DeleteItemMessageTemplateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageTemplateDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<MessageTemplateDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Name)).ReturnsAsync(new RepoResponse<MessageTemplateDto>());
            var handler = new DeleteItemMessageTemplateCommand.DeleteItemMessageTemplateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemMessageTemplateCommand(fakeDto.Name);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<MessageTemplateDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageTemplateDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<MessageTemplateDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Name)).ReturnsAsync(new RepoResponse<MessageTemplateDto>());
            var handler = new DeleteItemMessageTemplateCommand.DeleteItemMessageTemplateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemMessageTemplateCommand(fakeDto.Name);
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