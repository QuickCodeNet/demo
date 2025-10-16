﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUser;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUser;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.AspNetUser
{
    public class InsertAspNetUserCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetUserRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertAspNetUserCommand.InsertAspNetUserHandler>> _loggerMock;
        public InsertAspNetUserCommandTests()
        {
            _repositoryMock = new Mock<IAspNetUserRepository>();
            _loggerMock = new Mock<ILogger<InsertAspNetUserCommand.InsertAspNetUserHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserDto>("tr");
            var fakeResponse = new RepoResponse<AspNetUserDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetUserDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetUserCommand.InsertAspNetUserHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetUserCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<AspNetUserDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserDto>("tr");
            var fakeResponse = new RepoResponse<AspNetUserDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetUserDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetUserCommand.InsertAspNetUserHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetUserCommand(fakeDto);
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