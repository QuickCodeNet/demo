using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.OrderManagementModule.Application.Services.ReturnRequest;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.ReturnRequest;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.OrderManagementModule.Application.Tests.Services.ReturnRequest
{
    public class InsertReturnRequestCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IReturnRequestRepository> _repositoryMock;
        private readonly Mock<ILogger<ReturnRequestService>> _loggerMock;
        private readonly ReturnRequestService _service;
        public InsertReturnRequestCommandTests()
        {
            _repositoryMock = new Mock<IReturnRequestRepository>();
            _loggerMock = new Mock<ILogger<ReturnRequestService>>();
            _service = new ReturnRequestService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ReturnRequestDto>("tr");
            var fakeResponse = new RepoResponse<ReturnRequestDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ReturnRequestDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ReturnRequestDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ReturnRequestDto>("tr");
            var fakeResponse = new RepoResponse<ReturnRequestDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ReturnRequestDto>())).ReturnsAsync(fakeResponse);
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