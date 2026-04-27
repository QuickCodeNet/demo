using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ProductCatalogModule.Application.Services.ProductVariantAttribute;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductVariantAttribute;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ProductCatalogModule.Application.Tests.Services.ProductVariantAttribute
{
    public class InsertProductVariantAttributeCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IProductVariantAttributeRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductVariantAttributeService>> _loggerMock;
        private readonly ProductVariantAttributeService _service;
        public InsertProductVariantAttributeCommandTests()
        {
            _repositoryMock = new Mock<IProductVariantAttributeRepository>();
            _loggerMock = new Mock<ILogger<ProductVariantAttributeService>>();
            _service = new ProductVariantAttributeService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            var fakeResponse = new RepoResponse<ProductVariantAttributeDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ProductVariantAttributeDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            var fakeResponse = new RepoResponse<ProductVariantAttributeDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(fakeResponse);
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