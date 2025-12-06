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
    public class UpdateApartmentFeePlanCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApartmentFeePlanRepository> _repositoryMock;
        private readonly Mock<ILogger<ApartmentFeePlanService>> _loggerMock;
        private readonly ApartmentFeePlanService _service;
        public UpdateApartmentFeePlanCommandTests()
        {
            _repositoryMock = new Mock<IApartmentFeePlanRepository>();
            _loggerMock = new Mock<ILogger<ApartmentFeePlanService>>();
            _service = new ApartmentFeePlanService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApartmentFeePlanDto>("tr");
            var fakeGetResponse = new RepoResponse<ApartmentFeePlanDto>(fakeDto, "Success");
            var fakeUpdateResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ApartmentFeePlanDto>())).ReturnsAsync(fakeUpdateResponse);
            // Act
            var result = await _service.UpdateAsync(fakeDto.Id, fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ApartmentFeePlanDto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApartmentFeePlanDto>("tr");
            var fakeGetResponse = new RepoResponse<ApartmentFeePlanDto>
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
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ApartmentFeePlanDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}