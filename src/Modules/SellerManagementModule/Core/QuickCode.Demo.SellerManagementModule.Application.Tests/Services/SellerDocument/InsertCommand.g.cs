using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SellerManagementModule.Application.Services.SellerDocument;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerDocument;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SellerManagementModule.Application.Tests.Services.SellerDocument
{
    public class InsertSellerDocumentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISellerDocumentRepository> _repositoryMock;
        private readonly Mock<ILogger<SellerDocumentService>> _loggerMock;
        private readonly SellerDocumentService _service;
        public InsertSellerDocumentCommandTests()
        {
            _repositoryMock = new Mock<ISellerDocumentRepository>();
            _loggerMock = new Mock<ILogger<SellerDocumentService>>();
            _service = new SellerDocumentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerDocumentDto>("tr");
            var fakeResponse = new RepoResponse<SellerDocumentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerDocumentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SellerDocumentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerDocumentDto>("tr");
            var fakeResponse = new RepoResponse<SellerDocumentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerDocumentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
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