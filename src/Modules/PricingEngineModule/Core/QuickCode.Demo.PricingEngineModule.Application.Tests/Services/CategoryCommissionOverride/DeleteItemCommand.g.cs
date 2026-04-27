using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.PricingEngineModule.Application.Services.CategoryCommissionOverride;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CategoryCommissionOverride;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.PricingEngineModule.Application.Tests.Services.CategoryCommissionOverride
{
    public class CategoryCommissionOverrideServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICategoryCommissionOverrideRepository> _repositoryMock;
        private readonly Mock<ILogger<CategoryCommissionOverrideService>> _loggerMock;
        private readonly CategoryCommissionOverrideService _service;
        public CategoryCommissionOverrideServiceDeleteTests()
        {
            _repositoryMock = new Mock<ICategoryCommissionOverrideRepository>();
            _loggerMock = new Mock<ILogger<CategoryCommissionOverrideService>>();
            _service = new CategoryCommissionOverrideService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CategoryCommissionOverrideDto>("tr");
            var fakeGetResponse = new RepoResponse<CategoryCommissionOverrideDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<CategoryCommissionOverrideDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Id);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<CategoryCommissionOverrideDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<CategoryCommissionOverrideDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<CategoryCommissionOverrideDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Id);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<CategoryCommissionOverrideDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}