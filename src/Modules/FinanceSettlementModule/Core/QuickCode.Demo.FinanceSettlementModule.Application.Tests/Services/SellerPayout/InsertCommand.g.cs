using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.FinanceSettlementModule.Application.Services.SellerPayout;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.SellerPayout;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Tests.Services.SellerPayout
{
    public class InsertSellerPayoutCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISellerPayoutRepository> _repositoryMock;
        private readonly Mock<ILogger<SellerPayoutService>> _loggerMock;
        private readonly SellerPayoutService _service;
        public InsertSellerPayoutCommandTests()
        {
            _repositoryMock = new Mock<ISellerPayoutRepository>();
            _loggerMock = new Mock<ILogger<SellerPayoutService>>();
            _service = new SellerPayoutService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerPayoutDto>("tr");
            var fakeResponse = new RepoResponse<SellerPayoutDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerPayoutDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SellerPayoutDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SellerPayoutDto>("tr");
            var fakeResponse = new RepoResponse<SellerPayoutDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SellerPayoutDto>())).ReturnsAsync(fakeResponse);
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