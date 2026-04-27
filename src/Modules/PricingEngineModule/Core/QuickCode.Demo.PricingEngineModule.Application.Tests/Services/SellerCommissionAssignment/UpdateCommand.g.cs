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
    public class UpdateSellerCommissionAssignmentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISellerCommissionAssignmentRepository> _repositoryMock;
        private readonly Mock<ILogger<SellerCommissionAssignmentService>> _loggerMock;
        private readonly SellerCommissionAssignmentService _service;
        public UpdateSellerCommissionAssignmentCommandTests()
        {
            _repositoryMock = new Mock<ISellerCommissionAssignmentRepository>();
            _loggerMock = new Mock<ILogger<SellerCommissionAssignmentService>>();
            _service = new SellerCommissionAssignmentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerCommissionAssignmentDto>("tr");
            var fakeGetResponse = new RepoResponse<SellerCommissionAssignmentDto>(fakeDto, "Success");
            var fakeUpdateResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.SellerId)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<SellerCommissionAssignmentDto>())).ReturnsAsync(fakeUpdateResponse);
            // Act
            var result = await _service.UpdateAsync(fakeDto.SellerId, fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.SellerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<SellerCommissionAssignmentDto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerCommissionAssignmentDto>("tr");
            var fakeGetResponse = new RepoResponse<SellerCommissionAssignmentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.SellerId)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.UpdateAsync(fakeDto.SellerId, fakeDto);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.SellerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<SellerCommissionAssignmentDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}