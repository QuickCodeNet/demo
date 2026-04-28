using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.OrderManagementModule.Application.Services.OrderStatusManager;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.OrderStatusManager;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.OrderManagementModule.Application.Tests.Services.OrderStatusManager
{
    public class InsertOrderStatusManagerCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IOrderStatusManagerRepository> _repositoryMock;
        private readonly Mock<ILogger<OrderStatusManagerService>> _loggerMock;
        private readonly OrderStatusManagerService _service;
        public InsertOrderStatusManagerCommandTests()
        {
            _repositoryMock = new Mock<IOrderStatusManagerRepository>();
            _loggerMock = new Mock<ILogger<OrderStatusManagerService>>();
            _service = new OrderStatusManagerService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OrderStatusManagerDto>("tr");
            var fakeResponse = new RepoResponse<OrderStatusManagerDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OrderStatusManagerDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<OrderStatusManagerDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<OrderStatusManagerDto>("tr");
            var fakeResponse = new RepoResponse<OrderStatusManagerDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<OrderStatusManagerDto>())).ReturnsAsync(fakeResponse);
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