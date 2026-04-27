using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.FinanceSettlementModule.Application.Services.PayoutLineItem;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.PayoutLineItem;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Tests.Services.PayoutLineItem
{
    public class InsertPayoutLineItemCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPayoutLineItemRepository> _repositoryMock;
        private readonly Mock<ILogger<PayoutLineItemService>> _loggerMock;
        private readonly PayoutLineItemService _service;
        public InsertPayoutLineItemCommandTests()
        {
            _repositoryMock = new Mock<IPayoutLineItemRepository>();
            _loggerMock = new Mock<ILogger<PayoutLineItemService>>();
            _service = new PayoutLineItemService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PayoutLineItemDto>("tr");
            var fakeResponse = new RepoResponse<PayoutLineItemDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PayoutLineItemDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PayoutLineItemDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PayoutLineItemDto>("tr");
            var fakeResponse = new RepoResponse<PayoutLineItemDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PayoutLineItemDto>())).ReturnsAsync(fakeResponse);
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