using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.EmailManagerModule.Application.Services.MessageTemplate;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.EmailManagerModule.Application.Tests.Services.MessageTemplate
{
    public class MessageTemplateServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IMessageTemplateRepository> _repositoryMock;
        private readonly Mock<ILogger<MessageTemplateService>> _loggerMock;
        private readonly MessageTemplateService _service;
        public MessageTemplateServiceDeleteTests()
        {
            _repositoryMock = new Mock<IMessageTemplateRepository>();
            _loggerMock = new Mock<ILogger<MessageTemplateService>>();
            _service = new MessageTemplateService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<MessageTemplateDto>("tr");
            var fakeGetResponse = new RepoResponse<MessageTemplateDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Name)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<MessageTemplateDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Name);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Name), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<MessageTemplateDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<MessageTemplateDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<MessageTemplateDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Name)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Name);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Name), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<MessageTemplateDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}