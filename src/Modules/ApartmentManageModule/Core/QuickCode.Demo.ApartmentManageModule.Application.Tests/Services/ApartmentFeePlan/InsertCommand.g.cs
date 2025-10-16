using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.ApartmentFeePlan;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ApartmentFeePlan;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.ApartmentFeePlan
{
    public class InsertApartmentFeePlanCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApartmentFeePlanRepository> _repositoryMock;
        private readonly Mock<ILogger<ApartmentFeePlanService>> _loggerMock;
        private readonly ApartmentFeePlanService _service;
        public InsertApartmentFeePlanCommandTests()
        {
            _repositoryMock = new Mock<IApartmentFeePlanRepository>();
            _loggerMock = new Mock<ILogger<ApartmentFeePlanService>>();
            _service = new ApartmentFeePlanService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApartmentFeePlanDto>("tr");
            var fakeResponse = new RepoResponse<ApartmentFeePlanDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApartmentFeePlanDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ApartmentFeePlanDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApartmentFeePlanDto>("tr");
            var fakeResponse = new RepoResponse<ApartmentFeePlanDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApartmentFeePlanDto>())).ReturnsAsync(fakeResponse);
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