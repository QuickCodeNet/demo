﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetRole;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetRole;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.AspNetRole
{
    public class InsertAspNetRoleCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetRoleRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertAspNetRoleCommand.InsertAspNetRoleHandler>> _loggerMock;
        public InsertAspNetRoleCommandTests()
        {
            _repositoryMock = new Mock<IAspNetRoleRepository>();
            _loggerMock = new Mock<ILogger<InsertAspNetRoleCommand.InsertAspNetRoleHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetRoleDto>("tr");
            var fakeResponse = new RepoResponse<AspNetRoleDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetRoleDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetRoleCommand.InsertAspNetRoleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetRoleCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<AspNetRoleDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetRoleDto>("tr");
            var fakeResponse = new RepoResponse<AspNetRoleDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetRoleDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetRoleCommand.InsertAspNetRoleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetRoleCommand(fakeDto);
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