﻿using System;
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
    public class InsertFlatPaymentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFlatPaymentRepository> _repositoryMock;
        private readonly Mock<ILogger<FlatPaymentService>> _loggerMock;
        private readonly FlatPaymentService _service;
        public InsertFlatPaymentCommandTests()
        {
            _repositoryMock = new Mock<IFlatPaymentRepository>();
            _loggerMock = new Mock<ILogger<FlatPaymentService>>();
            _service = new FlatPaymentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FlatPaymentDto>("tr");
            var fakeResponse = new RepoResponse<FlatPaymentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FlatPaymentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<FlatPaymentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FlatPaymentDto>("tr");
            var fakeResponse = new RepoResponse<FlatPaymentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FlatPaymentDto>())).ReturnsAsync(fakeResponse);
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