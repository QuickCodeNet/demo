using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.FlatPayment;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatPayment;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.FlatPayment
{
    public class UpdateFlatPaymentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFlatPaymentRepository> _repositoryMock;
        private readonly Mock<ILogger<FlatPaymentService>> _loggerMock;
        private readonly FlatPaymentService _service;
        public UpdateFlatPaymentCommandTests()
        {
            _repositoryMock = new Mock<IFlatPaymentRepository>();
            _loggerMock = new Mock<ILogger<FlatPaymentService>>();
            _service = new FlatPaymentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FlatPaymentDto>("tr");
            var fakeGetResponse = new RepoResponse<FlatPaymentDto>(fakeDto, "Success");
            var fakeUpdateResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<FlatPaymentDto>())).ReturnsAsync(fakeUpdateResponse);
            // Act
            var result = await _service.UpdateAsync(fakeDto.Id, fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<FlatPaymentDto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FlatPaymentDto>("tr");
            var fakeGetResponse = new RepoResponse<FlatPaymentDto>
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
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<FlatPaymentDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}