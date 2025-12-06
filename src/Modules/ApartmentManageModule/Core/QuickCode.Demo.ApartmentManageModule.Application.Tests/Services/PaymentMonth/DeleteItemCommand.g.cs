using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentMonth;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentMonth;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.PaymentMonth
{
    public class PaymentMonthServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPaymentMonthRepository> _repositoryMock;
        private readonly Mock<ILogger<PaymentMonthService>> _loggerMock;
        private readonly PaymentMonthService _service;
        public PaymentMonthServiceDeleteTests()
        {
            _repositoryMock = new Mock<IPaymentMonthRepository>();
            _loggerMock = new Mock<ILogger<PaymentMonthService>>();
            _service = new PaymentMonthService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentMonthDto>("tr");
            var fakeGetResponse = new RepoResponse<PaymentMonthDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<PaymentMonthDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Id);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<PaymentMonthDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<PaymentMonthDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<PaymentMonthDto>
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
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<PaymentMonthDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}