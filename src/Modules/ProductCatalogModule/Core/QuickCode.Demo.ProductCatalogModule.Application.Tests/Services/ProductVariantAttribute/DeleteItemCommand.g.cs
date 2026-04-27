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
    public class ProductVariantAttributeServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IProductVariantAttributeRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductVariantAttributeService>> _loggerMock;
        private readonly ProductVariantAttributeService _service;
        public ProductVariantAttributeServiceDeleteTests()
        {
            _repositoryMock = new Mock<IProductVariantAttributeRepository>();
            _loggerMock = new Mock<ILogger<ProductVariantAttributeService>>();
            _service = new ProductVariantAttributeService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            var fakeGetResponse = new RepoResponse<ProductVariantAttributeDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.VariantId, fakeDto.AttributeValueId)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.VariantId, fakeDto.AttributeValueId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ProductVariantAttributeDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<ProductVariantAttributeDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.VariantId, fakeDto.AttributeValueId)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.VariantId, fakeDto.AttributeValueId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ProductVariantAttributeDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}