using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Services.ExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Services.ExpenseInstallment
{
    public class InsertExpenseInstallmentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IExpenseInstallmentRepository> _repositoryMock;
        private readonly Mock<ILogger<ExpenseInstallmentService>> _loggerMock;
        private readonly ExpenseInstallmentService _service;
        public InsertExpenseInstallmentCommandTests()
        {
            _repositoryMock = new Mock<IExpenseInstallmentRepository>();
            _loggerMock = new Mock<ILogger<ExpenseInstallmentService>>();
            _service = new ExpenseInstallmentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ExpenseInstallmentDto>("tr");
            var fakeResponse = new RepoResponse<ExpenseInstallmentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ExpenseInstallmentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ExpenseInstallmentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ExpenseInstallmentDto>("tr");
            var fakeResponse = new RepoResponse<ExpenseInstallmentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ExpenseInstallmentDto>())).ReturnsAsync(fakeResponse);
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