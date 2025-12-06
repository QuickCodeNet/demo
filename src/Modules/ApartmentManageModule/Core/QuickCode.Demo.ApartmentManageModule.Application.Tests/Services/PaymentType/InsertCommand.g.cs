using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentType;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentType;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.PaymentType
{
    public class InsertPaymentTypeCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPaymentTypeRepository> _repositoryMock;
        private readonly Mock<ILogger<PaymentTypeService>> _loggerMock;
        private readonly PaymentTypeService _service;
        public InsertPaymentTypeCommandTests()
        {
            _repositoryMock = new Mock<IPaymentTypeRepository>();
            _loggerMock = new Mock<ILogger<PaymentTypeService>>();
            _service = new PaymentTypeService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentTypeDto>("tr");
            var fakeResponse = new RepoResponse<PaymentTypeDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PaymentTypeDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PaymentTypeDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentTypeDto>("tr");
            var fakeResponse = new RepoResponse<PaymentTypeDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PaymentTypeDto>())).ReturnsAsync(fakeResponse);
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