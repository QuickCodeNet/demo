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
    public class InsertMessageTemplateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IMessageTemplateRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertMessageTemplateCommand.InsertMessageTemplateHandler>> _loggerMock;
        public InsertMessageTemplateCommandTests()
        {
            _repositoryMock = new Mock<IMessageTemplateRepository>();
            _loggerMock = new Mock<ILogger<InsertMessageTemplateCommand.InsertMessageTemplateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageTemplateDto>("tr");
            var fakeResponse = new RepoResponse<MessageTemplateDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<MessageTemplateDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertMessageTemplateCommand.InsertMessageTemplateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertMessageTemplateCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<MessageTemplateDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageTemplateDto>("tr");
            var fakeResponse = new RepoResponse<MessageTemplateDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<MessageTemplateDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertMessageTemplateCommand.InsertMessageTemplateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertMessageTemplateCommand(fakeDto);
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