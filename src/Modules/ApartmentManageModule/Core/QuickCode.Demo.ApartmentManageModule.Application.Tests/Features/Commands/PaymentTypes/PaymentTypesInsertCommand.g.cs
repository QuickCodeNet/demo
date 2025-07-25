using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Features;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Features
{
    public class PaymentTypesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPaymentTypesRepository> _repositoryMock;
        private readonly Mock<ILogger<PaymentTypesInsertCommand.PaymentTypesInsertHandler>> _loggerMock;
        public PaymentTypesInsertCommandTests()
        {
            _repositoryMock = new Mock<IPaymentTypesRepository>();
            _loggerMock = new Mock<ILogger<PaymentTypesInsertCommand.PaymentTypesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentTypesDto>("tr");
            var fakeResponse = new RepoResponse<PaymentTypesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PaymentTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new PaymentTypesInsertCommand.PaymentTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PaymentTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PaymentTypesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentTypesDto>("tr");
            var fakeResponse = new RepoResponse<PaymentTypesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PaymentTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new PaymentTypesInsertCommand.PaymentTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PaymentTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
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