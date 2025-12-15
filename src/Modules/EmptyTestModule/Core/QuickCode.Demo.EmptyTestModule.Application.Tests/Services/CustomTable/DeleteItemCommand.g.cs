using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.EmptyTestModule.Application.Services.CustomTable;
using QuickCode.Demo.EmptyTestModule.Application.Dtos.CustomTable;
using QuickCode.Demo.EmptyTestModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.EmptyTestModule.Application.Tests.Services.CustomTable
{
    public class CustomTableServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICustomTableRepository> _repositoryMock;
        private readonly Mock<ILogger<CustomTableService>> _loggerMock;
        private readonly CustomTableService _service;
        public CustomTableServiceDeleteTests()
        {
            _repositoryMock = new Mock<ICustomTableRepository>();
            _loggerMock = new Mock<ILogger<CustomTableService>>();
            _service = new CustomTableService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CustomTableDto>("tr");
            var fakeGetResponse = new RepoResponse<CustomTableDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<CustomTableDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.Id);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.Id), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<CustomTableDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<CustomTableDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<CustomTableDto>
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
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<CustomTableDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}