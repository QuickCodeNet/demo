using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ProductCatalogModule.Application.Services.ProductCategory;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductCategory;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ProductCatalogModule.Application.Tests.Services.ProductCategory
{
    public class ProductCategoryServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IProductCategoryRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductCategoryService>> _loggerMock;
        private readonly ProductCategoryService _service;
        public ProductCategoryServiceDeleteTests()
        {
            _repositoryMock = new Mock<IProductCategoryRepository>();
            _loggerMock = new Mock<ILogger<ProductCategoryService>>();
            _service = new ProductCategoryService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductCategoryDto>("tr");
            var fakeGetResponse = new RepoResponse<ProductCategoryDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.ProductId, fakeDto.CategoryId)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ProductCategoryDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.ProductId, fakeDto.CategoryId);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.ProductId, fakeDto.CategoryId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ProductCategoryDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<ProductCategoryDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<ProductCategoryDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.ProductId, fakeDto.CategoryId)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.ProductId, fakeDto.CategoryId);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.ProductId, fakeDto.CategoryId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ProductCategoryDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}