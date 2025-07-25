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
    public class FeeTypesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IFeeTypesRepository> _repositoryMock;
        private readonly Mock<ILogger<FeeTypesInsertCommand.FeeTypesInsertHandler>> _loggerMock;
        public FeeTypesInsertCommandTests()
        {
            _repositoryMock = new Mock<IFeeTypesRepository>();
            _loggerMock = new Mock<ILogger<FeeTypesInsertCommand.FeeTypesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FeeTypesDto>("tr");
            var fakeResponse = new RepoResponse<FeeTypesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FeeTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new FeeTypesInsertCommand.FeeTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new FeeTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<FeeTypesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<FeeTypesDto>("tr");
            var fakeResponse = new RepoResponse<FeeTypesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<FeeTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new FeeTypesInsertCommand.FeeTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new FeeTypesInsertCommand(fakeDto);
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