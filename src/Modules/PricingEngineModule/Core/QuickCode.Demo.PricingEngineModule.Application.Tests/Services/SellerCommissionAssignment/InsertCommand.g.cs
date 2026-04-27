using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.PricingEngineModule.Application.Services.SellerCommissionAssignment;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.SellerCommissionAssignment;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.PricingEngineModule.Application.Tests.Services.SellerCommissionAssignment
{
    public class InsertSellerCommissionAssignmentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISellerCommissionAssignmentRepository> _repositoryMock;
        private readonly Mock<ILogger<SellerCommissionAssignmentService>> _loggerMock;
        private readonly SellerCommissionAssignmentService _service;
        public InsertSellerCommissionAssignmentCommandTests()
        {
            _repositoryMock = new Mock<ISellerCommissionAssignmentRepository>();
            _loggerMock = new Mock<ILogger<SellerCommissionAssignmentService>>();
            _service = new SellerCommissionAssignmentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerCommissionAssignmentDto>("tr");
            var fakeResponse = new RepoResponse<SellerCommissionAssignmentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerCommissionAssignmentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SellerCommissionAssignmentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerCommissionAssignmentDto>("tr");
            var fakeResponse = new RepoResponse<SellerCommissionAssignmentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerCommissionAssignmentDto>())).ReturnsAsync(fakeResponse);
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