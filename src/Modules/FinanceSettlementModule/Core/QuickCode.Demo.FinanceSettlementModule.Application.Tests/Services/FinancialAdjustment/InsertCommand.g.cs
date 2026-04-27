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
    public class InsertFinancialAdjustmentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFinancialAdjustmentRepository> _repositoryMock;
        private readonly Mock<ILogger<FinancialAdjustmentService>> _loggerMock;
        private readonly FinancialAdjustmentService _service;
        public InsertFinancialAdjustmentCommandTests()
        {
            _repositoryMock = new Mock<IFinancialAdjustmentRepository>();
            _loggerMock = new Mock<ILogger<FinancialAdjustmentService>>();
            _service = new FinancialAdjustmentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FinancialAdjustmentDto>("tr");
            var fakeResponse = new RepoResponse<FinancialAdjustmentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FinancialAdjustmentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<FinancialAdjustmentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FinancialAdjustmentDto>("tr");
            var fakeResponse = new RepoResponse<FinancialAdjustmentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FinancialAdjustmentDto>())).ReturnsAsync(fakeResponse);
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