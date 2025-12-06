using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.FeeType;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FeeType;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.FeeType
{
    public class InsertFeeTypeCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFeeTypeRepository> _repositoryMock;
        private readonly Mock<ILogger<FeeTypeService>> _loggerMock;
        private readonly FeeTypeService _service;
        public InsertFeeTypeCommandTests()
        {
            _repositoryMock = new Mock<IFeeTypeRepository>();
            _loggerMock = new Mock<ILogger<FeeTypeService>>();
            _service = new FeeTypeService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FeeTypeDto>("tr");
            var fakeResponse = new RepoResponse<FeeTypeDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FeeTypeDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<FeeTypeDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FeeTypeDto>("tr");
            var fakeResponse = new RepoResponse<FeeTypeDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FeeTypeDto>())).ReturnsAsync(fakeResponse);
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