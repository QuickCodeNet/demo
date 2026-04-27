using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.FinanceSettlementModule.Application.Services.FinancialAdjustment;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.FinancialAdjustment;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Tests.Services.FinancialAdjustment
{
    public class FinancialAdjustmentServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFinancialAdjustmentRepository> _repositoryMock;
        private readonly Mock<ILogger<FinancialAdjustmentService>> _loggerMock;
        private readonly FinancialAdjustmentService _service;
        public FinancialAdjustmentServiceDeleteTests()
        {
            _repositoryMock = new Mock<IFinancialAdjustmentRepository>();
            _loggerMock = new Mock<ILogger<FinancialAdjustmentService>>();
            _service = new FinancialAdjustmentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FinancialAdjustmentDto>("tr");
            var fakeGetResponse = new RepoResponse<FinancialAdjustmentDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<FinancialAdjustmentDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Id);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<FinancialAdjustmentDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<FinancialAdjustmentDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<FinancialAdjustmentDto>
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
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<FinancialAdjustmentDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}