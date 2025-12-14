using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.OnlineShopModule.Application.Services.ProductGroup;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductGroup;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.OnlineShopModule.Application.Tests.Services.ProductGroup
{
    public class InsertProductGroupCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IProductGroupRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductGroupService>> _loggerMock;
        private readonly ProductGroupService _service;
        public InsertProductGroupCommandTests()
        {
            _repositoryMock = new Mock<IProductGroupRepository>();
            _loggerMock = new Mock<ILogger<ProductGroupService>>();
            _service = new ProductGroupService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductGroupDto>("tr");
            var fakeResponse = new RepoResponse<ProductGroupDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ProductGroupDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ProductGroupDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductGroupDto>("tr");
            var fakeResponse = new RepoResponse<ProductGroupDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ProductGroupDto>())).ReturnsAsync(fakeResponse);
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