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
    public class InsertCategoryCommissionOverrideCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICategoryCommissionOverrideRepository> _repositoryMock;
        private readonly Mock<ILogger<CategoryCommissionOverrideService>> _loggerMock;
        private readonly CategoryCommissionOverrideService _service;
        public InsertCategoryCommissionOverrideCommandTests()
        {
            _repositoryMock = new Mock<ICategoryCommissionOverrideRepository>();
            _loggerMock = new Mock<ILogger<CategoryCommissionOverrideService>>();
            _service = new CategoryCommissionOverrideService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CategoryCommissionOverrideDto>("tr");
            var fakeResponse = new RepoResponse<CategoryCommissionOverrideDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CategoryCommissionOverrideDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<CategoryCommissionOverrideDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CategoryCommissionOverrideDto>("tr");
            var fakeResponse = new RepoResponse<CategoryCommissionOverrideDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CategoryCommissionOverrideDto>())).ReturnsAsync(fakeResponse);
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