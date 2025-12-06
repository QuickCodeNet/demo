using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.FlatExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.FlatExpenseInstallment
{
    public class InsertFlatExpenseInstallmentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFlatExpenseInstallmentRepository> _repositoryMock;
        private readonly Mock<ILogger<FlatExpenseInstallmentService>> _loggerMock;
        private readonly FlatExpenseInstallmentService _service;
        public InsertFlatExpenseInstallmentCommandTests()
        {
            _repositoryMock = new Mock<IFlatExpenseInstallmentRepository>();
            _loggerMock = new Mock<ILogger<FlatExpenseInstallmentService>>();
            _service = new FlatExpenseInstallmentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FlatExpenseInstallmentDto>("tr");
            var fakeResponse = new RepoResponse<FlatExpenseInstallmentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FlatExpenseInstallmentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<FlatExpenseInstallmentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FlatExpenseInstallmentDto>("tr");
            var fakeResponse = new RepoResponse<FlatExpenseInstallmentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FlatExpenseInstallmentDto>())).ReturnsAsync(fakeResponse);
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