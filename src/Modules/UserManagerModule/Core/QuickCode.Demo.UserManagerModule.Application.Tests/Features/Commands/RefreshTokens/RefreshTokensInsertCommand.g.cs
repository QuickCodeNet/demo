using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features
{
    public class RefreshTokensInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IRefreshTokensRepository> _repositoryMock;
        private readonly Mock<ILogger<RefreshTokensInsertCommand.RefreshTokensInsertHandler>> _loggerMock;
        public RefreshTokensInsertCommandTests()
        {
            _repositoryMock = new Mock<IRefreshTokensRepository>();
            _loggerMock = new Mock<ILogger<RefreshTokensInsertCommand.RefreshTokensInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<RefreshTokensDto>("tr");
            var fakeResponse = new RepoResponse<RefreshTokensDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<RefreshTokensDto>())).ReturnsAsync(fakeResponse);
            var handler = new RefreshTokensInsertCommand.RefreshTokensInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new RefreshTokensInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<RefreshTokensDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<RefreshTokensDto>("tr");
            var fakeResponse = new RepoResponse<RefreshTokensDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<RefreshTokensDto>())).ReturnsAsync(fakeResponse);
            var handler = new RefreshTokensInsertCommand.RefreshTokensInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new RefreshTokensInsertCommand(fakeDto);
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