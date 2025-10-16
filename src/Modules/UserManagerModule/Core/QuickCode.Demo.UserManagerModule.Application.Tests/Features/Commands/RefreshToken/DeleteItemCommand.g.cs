﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.RefreshToken;
using QuickCode.Demo.UserManagerModule.Application.Dtos.RefreshToken;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.RefreshToken
{
    public class DeleteItemRefreshTokenCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IRefreshTokenRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemRefreshTokenCommand.DeleteItemRefreshTokenHandler>> _loggerMock;
        public DeleteItemRefreshTokenCommandTests()
        {
            _repositoryMock = new Mock<IRefreshTokenRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemRefreshTokenCommand.DeleteItemRefreshTokenHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<RefreshTokenDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<RefreshTokenDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<RefreshTokenDto>());
            var handler = new DeleteItemRefreshTokenCommand.DeleteItemRefreshTokenHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemRefreshTokenCommand(fakeDto.Id);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<RefreshTokenDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<RefreshTokenDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<RefreshTokenDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<RefreshTokenDto>());
            var handler = new DeleteItemRefreshTokenCommand.DeleteItemRefreshTokenHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemRefreshTokenCommand(fakeDto.Id);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}