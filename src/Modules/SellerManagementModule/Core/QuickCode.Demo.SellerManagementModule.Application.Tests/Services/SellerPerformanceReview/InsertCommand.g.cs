using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SellerManagementModule.Application.Services.SellerPerformanceReview;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerPerformanceReview;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SellerManagementModule.Application.Tests.Services.SellerPerformanceReview
{
    public class InsertSellerPerformanceReviewCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISellerPerformanceReviewRepository> _repositoryMock;
        private readonly Mock<ILogger<SellerPerformanceReviewService>> _loggerMock;
        private readonly SellerPerformanceReviewService _service;
        public InsertSellerPerformanceReviewCommandTests()
        {
            _repositoryMock = new Mock<ISellerPerformanceReviewRepository>();
            _loggerMock = new Mock<ILogger<SellerPerformanceReviewService>>();
            _service = new SellerPerformanceReviewService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerPerformanceReviewDto>("tr");
            var fakeResponse = new RepoResponse<SellerPerformanceReviewDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerPerformanceReviewDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SellerPerformanceReviewDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerPerformanceReviewDto>("tr");
            var fakeResponse = new RepoResponse<SellerPerformanceReviewDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerPerformanceReviewDto>())).ReturnsAsync(fakeResponse);
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