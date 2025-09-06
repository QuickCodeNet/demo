using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features
{
    public class InfoTypesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IInfoTypesRepository> _repositoryMock;
        private readonly Mock<ILogger<InfoTypesInsertCommand.InfoTypesInsertHandler>> _loggerMock;
        public InfoTypesInsertCommandTests()
        {
            _repositoryMock = new Mock<IInfoTypesRepository>();
            _loggerMock = new Mock<ILogger<InfoTypesInsertCommand.InfoTypesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoTypesDto>("tr");
            var fakeResponse = new RepoResponse<InfoTypesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<InfoTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new InfoTypesInsertCommand.InfoTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<InfoTypesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<InfoTypesDto>("tr");
            var fakeResponse = new RepoResponse<InfoTypesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<InfoTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new InfoTypesInsertCommand.InfoTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InfoTypesInsertCommand(fakeDto);
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