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
    public class PaymentMonthsInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPaymentMonthsRepository> _repositoryMock;
        private readonly Mock<ILogger<PaymentMonthsInsertCommand.PaymentMonthsInsertHandler>> _loggerMock;
        public PaymentMonthsInsertCommandTests()
        {
            _repositoryMock = new Mock<IPaymentMonthsRepository>();
            _loggerMock = new Mock<ILogger<PaymentMonthsInsertCommand.PaymentMonthsInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentMonthsDto>("tr");
            var fakeResponse = new RepoResponse<PaymentMonthsDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PaymentMonthsDto>())).ReturnsAsync(fakeResponse);
            var handler = new PaymentMonthsInsertCommand.PaymentMonthsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PaymentMonthsInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PaymentMonthsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PaymentMonthsDto>("tr");
            var fakeResponse = new RepoResponse<PaymentMonthsDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PaymentMonthsDto>())).ReturnsAsync(fakeResponse);
            var handler = new PaymentMonthsInsertCommand.PaymentMonthsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PaymentMonthsInsertCommand(fakeDto);
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