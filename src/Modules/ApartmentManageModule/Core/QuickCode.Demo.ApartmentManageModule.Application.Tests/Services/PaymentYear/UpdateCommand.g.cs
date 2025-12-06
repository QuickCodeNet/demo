using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentYear;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentYear;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.PaymentYear
{
    public class UpdatePaymentYearCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPaymentYearRepository> _repositoryMock;
        private readonly Mock<ILogger<PaymentYearService>> _loggerMock;
        private readonly PaymentYearService _service;
        public UpdatePaymentYearCommandTests()
        {
            _repositoryMock = new Mock<IPaymentYearRepository>();
            _loggerMock = new Mock<ILogger<PaymentYearService>>();
            _service = new PaymentYearService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentYearDto>("tr");
            var fakeGetResponse = new RepoResponse<PaymentYearDto>(fakeDto, "Success");
            var fakeUpdateResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PaymentYearDto>())).ReturnsAsync(fakeUpdateResponse);
            // Act
            var result = await _service.UpdateAsync(fakeDto.Id, fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<PaymentYearDto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentYearDto>("tr");
            var fakeGetResponse = new RepoResponse<PaymentYearDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.UpdateAsync(fakeDto.Id, fakeDto);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<PaymentYearDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}